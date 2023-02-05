using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject playerPrefab;

    public float speed { get => 2.3f + NextLevelManager.currentLevel * 0.1f; }
    public float rotationSpeed { get => 3 + NextLevelManager.currentLevel * 0.01f; }
    [Header("Settings")]
    public float minRotation = 270 - 70;
    public float maxRotation = 270 + 70;

    [Header("Components")]
    public Transform playerSpawnPoint;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    float _horizontalInput;
    //float _verticalInput;
    [HideInInspector]
    public List<Player> players = new List<Player>();

    public Player SpawnFirstRoot() {
        GameObject newGo = Instantiate(playerPrefab, playerSpawnPoint);
        Player player = newGo.GetComponent<Player>();
        player.Setup(true, this);
        players.Add(player);
        virtualCamera.Follow = newGo.transform;
        return player;
    }

    void Update() {
        GetPlayerInput();
    }

    private void FixedUpdate() {
        foreach (var player in players) {
            float sp = player.isMain ? speed : speed * 0.8f;
            player.rig2d.velocity = player.transform.right * sp;
            RotatePlayer(player);
        }
    }

    void GetPlayerInput() {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        //_verticalInput = Input.GetAxisRaw("Vertical");
    }

    void RotatePlayer(Player player) {
        float rotation = -_horizontalInput * rotationSpeed;
        if (player.flipControls)
            rotation *= -1;
        if (!player.isMain) {
            rotation *= player.rotationSpeed;
            rotation += player.fakeRotate;
        }
        player.transform.Rotate(Vector3.forward * rotation);
        Vector3 rotationFinal = player.transform.eulerAngles;
        rotationFinal.z = Mathf.Clamp(rotationFinal.z, minRotation, maxRotation);
        player.transform.eulerAngles = rotationFinal;
    }

    public void Split(Player oldPlayer) {
        float playerRotation = oldPlayer.transform.eulerAngles.z - 270;
        Debug.Log("Split!");
        GameObject newGo = Instantiate(playerPrefab);
        newGo.transform.position = oldPlayer.transform.position;
        Player player = newGo.GetComponent<Player>();
        player.flipControls = true;
        player.Setup(false, this);
        players.Add(player);
        newGo.transform.eulerAngles = new Vector3(0, 0, 270 - playerRotation);
    }
}