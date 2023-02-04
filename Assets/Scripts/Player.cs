using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Prefabs")]
    public GameObject Root;

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

    // Fake rotate
    float fakeRotateTime = 0;
    float nextFakeRotateStart;
    float nextFakeRotateEnd;
    float nextFakeRotateAmount;

    private void Awake() {
        rig2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (isMain) {
            if(Random.Range(0,1f) < smallRootsSpawnChancePerSecond * Time.deltaTime) {
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
        if (isMain) {
            HandlePickupPickedUp(collision.gameObject);
        }
    }

    public void HandlePickupPickedUp(GameObject pickup, bool withDestroy = true) {
        if (pickup.CompareTag("splitPowerup")) {
            if (withDestroy)
                PickupManager.instance.DestroyPickup(pickup);
            Split();
        } else if(pickup.CompareTag("rock")) {
            Debug.Log("YOU ARE DEAD");
        }


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
