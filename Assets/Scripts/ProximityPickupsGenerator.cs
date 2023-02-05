using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class ProximityPickupsGenerator : MonoBehaviour {
    public bool isFakePlayer { get => !GetComponent<Player>().isMain; } // if false spawnuje kulki z pickupumi else fakuj zbieranie pickupow
    public float fakePlayerPickupChancePerSecond = 0.1f;
    public float minRadius = 5;
    public float maxRadius = 10;
    public float minAngle = 0;
    public float maxAngle = 360;
    public List<GameObject> pickups = new List<GameObject>();
    public List<int> pickupChances = new List<int>();
    public int maxPickupsInProximity = 10;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, minRadius);
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }

    // Start is called before the first frame update
    void Start() {
        if (!isFakePlayer) {
            // generate in whole area only once
            for (int i = 0; i < maxPickupsInProximity; i++) {
                GameObject pickupType = Helpers.ChooseObjectWithChances(pickups, pickupChances);
                SpawnPickup(pickupType, true);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (!isFakePlayer) {
            for (int i = 0; i < maxPickupsInProximity - GetPickupsFurther().Count; i++) {
                GameObject pickupType = Helpers.ChooseObjectWithChances(pickups, pickupChances);
                SpawnPickup(pickupType);
            }
        } else {
            // imagine picking up pickups
            if (Random.Range(0, 1f) < fakePlayerPickupChancePerSecond * Time.deltaTime) {
                GameObject pickupType = Helpers.ChooseObjectWithChances(pickups, pickupChances);
                GetComponent<Player>().HandlePickupPickedUp(pickupType, false);
            }
        }
    }

    List<GameObject> GetPickupsFurther() {
        return PickupManager.instance.pickupsOnScene.Where(element => {
            float distance = Vector3.Distance(element.transform.position, transform.position);
            return distance <= maxRadius && distance >= minRadius
            && transform.position.y > element.gameObject.transform.position.y;
        }).ToList();
    }

    private void SpawnPickup(GameObject pickup, bool ignoreMinRadius = false) {
        float distance = Random.Range(ignoreMinRadius ? 3f : minRadius, maxRadius);
        float angle = Random.Range(minAngle, maxAngle);

        Vector3 spawnPoint = Helpers.RadianToCartesianCoords(distance, angle);
        spawnPoint = transform.position + spawnPoint;

        if (spawnPoint.y > -43) {
            var p = Instantiate(pickup);
            PickupManager.instance.pickupsOnScene.Add(p);
            p.transform.position = spawnPoint;
        }
    }
}
