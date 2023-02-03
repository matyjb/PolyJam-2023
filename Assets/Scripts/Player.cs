using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject Root;

    bool isMain = false;
    public bool flipControls = false;
    public Rigidbody2D rig2d;
    PlayerMovement playerMovement;

    private void Awake() {
        rig2d = GetComponent<Rigidbody2D>();
    }

    public void Setup(bool isMain, PlayerMovement playerMovement) {
        this.isMain = isMain;
        this.playerMovement = playerMovement;
    }

    public void Split() {
        playerMovement.Split(this);
        SpawnSmallRoots();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("splitPowerup")) {
            Destroy(collision.gameObject);
            Split();
        }
    }

    void SpawnSmallRoots() {
        GameObject newGo = Instantiate(Root, playerMovement.transform);
        newGo.transform.position = transform.position;
        newGo.transform.eulerAngles = new Vector3(0, 0, 180);
    }
}
