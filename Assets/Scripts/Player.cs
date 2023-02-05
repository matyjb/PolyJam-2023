using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [Header("Prefabs")]
    public GameObject Root;
    public GameObject coreRoot;

    [HideInInspector]
    public bool isMain = false;
    public float smallRootsSpawnChancePerSecond = 1f;
    [HideInInspector]
    public float fakeRotate = 0;
    public bool flipControls = false;
    public float rotationSpeed;
    public Rigidbody2D rig2d;
    PlayerMovement playerMovement;

    [Header("Trails")]
    public TrailRenderer upperTrail;
    public TrailRenderer belowTrail;
    public GameObject tunnelTrail;

    [Header("Settings")]
    public Material upperTrailMain;
    public Material belowTrailMain;
    public Material upperTrailNonMain;
    public Material belowTrailNonMain;

    [Header("Variable trails")]
    public Material upperTrailMax;
    public Material upperTrailMin;
    public Material upperTrailDead;

    // Fake rotate
    float fakeRotateTime = 0;
    float nextFakeRotateStart;
    float nextFakeRotateEnd;
    float nextFakeRotateAmount;

    private void Awake() {
        rig2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (rig2d != null) {
            if (isMain) {
                if (Random.Range(0, 1f) < smallRootsSpawnChancePerSecond * Time.deltaTime) {
                    SpawnSmallRoots();
                }
                return;
            }

            // Fake rotate
            fakeRotate = 0;
            fakeRotateTime += Time.deltaTime;
            if (fakeRotateTime > nextFakeRotateStart) {
                fakeRotate = nextFakeRotateAmount;
            }
            if (fakeRotateTime > nextFakeRotateEnd) {
                fakeRotateTime = 0;
                QueueNewFakeRotate();
            }
        }
    }

    public void Setup(bool isMain, PlayerMovement playerMovement) {
        this.isMain = isMain;
        this.playerMovement = playerMovement;
        if (!isMain) {
            QueueNewFakeRotate();
            rotationSpeed = Random.Range(0.8f, 1.3f);
            upperTrail.material = upperTrailNonMain;
            upperTrail.sortingOrder = -2;
            belowTrail.material = belowTrailNonMain;
            belowTrail.sortingOrder = -3;
            tunnelTrail.SetActive(true);
            upperTrail.widthMultiplier *= 0.5f;
            belowTrail.widthMultiplier *= 0.5f;
            tunnelTrail.GetComponent<TrailRenderer>().widthMultiplier *= 0.5f;
        }
    }

    public void Split() {
        playerMovement.Split(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        HandlePickupPickedUp(collision.gameObject);
    }

    public void HandlePickupPickedUp(GameObject pickup, bool withDestroy = true) {
        if (isMain) {
            if (pickup.CompareTag("splitPowerup")) {
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySound(3);
                if (withDestroy)
                    PickupManager.instance.DestroyPickup(pickup);
                Split();
            } else if (pickup.CompareTag("core")) {
                if (AudioManager.instance != null) {
                    AudioManager.instance.PlaySound(1);
                }
                GameController.instance.playerMovement.players.Remove(this);
                if (withDestroy)
                    Destroy(rig2d);

                // spawn small roots around
                int smallRootsCount = 8;
                float distance = -0.3f;
                float angle = 360f / (smallRootsCount - 1);
                for (int i = 0; i < smallRootsCount; i++) {
                    var root = Instantiate(coreRoot);
                    root.transform.position = pickup.transform.position + (Vector3)Helpers.RadianToCartesianCoords(distance, angle * i);
                    root.transform.rotation = Quaternion.Euler(0, 0, angle * i - 90);
                }

                // add delay to this
                StartCoroutine(LoadSceneCompletePlanetCorutine());
            }
        }



        if (pickup.CompareTag("rock")) {
            GameController.instance.playerMovement.players.Remove(this);
            if (withDestroy)
                Destroy(rig2d);
            if(isMain) {
                NextLevelManager.currentEnergyLevel = 0;
                 //GameController.instance.GameOver(); <- there is already gameover if energy <= 0;

            }
        } else if (pickup.CompareTag("energy")) {
            // TODO: picked up energy, what now?
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySound(2);
            if (withDestroy)
                PickupManager.instance.DestroyPickup(pickup);
            GameController.instance.GainEnergy();
        }


    }

    IEnumerator LoadSceneCompletePlanetCorutine() {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("CompletePlanet");
    }

    void SpawnSmallRoots() {
        GameObject newGo = Instantiate(Root, playerMovement.transform);
        newGo.transform.position = transform.position;
        newGo.transform.eulerAngles = new Vector3(0, 0, 180);
    }

    void QueueNewFakeRotate() {
        nextFakeRotateStart = Random.Range(0.3f, 1.1f);
        nextFakeRotateEnd = nextFakeRotateStart + Random.Range(0.8f, 1.3f);
        nextFakeRotateAmount = Random.Range(-3.2f, 3.2f);
    }
}
