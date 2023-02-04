using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Prefabs")]
    public GameObject Root;

    [HideInInspector]
    public bool isMain = false;
    [HideInInspector]
    public float fakeRotate = 0;
    public bool flipControls = false;
    public float rotationSpeed;
    public Rigidbody2D rig2d;
    PlayerMovement playerMovement;

    // Fake rotate
    float fakeRotateTime = 0;
    float nextFakeRotateStart;
    float nextFakeRotateEnd;
    float nextFakeRotateAmount;

    private void Awake() {
        rig2d = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (isMain)
            return;
        fakeRotate = 0;
        fakeRotateTime += Time.deltaTime;
        if (fakeRotateTime > nextFakeRotateStart) {
            fakeRotate = nextFakeRotateAmount;
        }
        if (fakeRotateTime > nextFakeRotateEnd) {
            fakeRotateTime = 0;
            QueueNewFakeRotate();
        }
        Debug.Log(fakeRotateTime + "; " + nextFakeRotateStart);
    }

    public void Setup(bool isMain, PlayerMovement playerMovement) {
        this.isMain = isMain;
        this.playerMovement = playerMovement;
        if (!isMain) {
            QueueNewFakeRotate();
            rotationSpeed = Random.Range(0.8f, 1.3f);
        }
    }

    public void Split() {
        playerMovement.Split(this);
        SpawnSmallRoots();
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
