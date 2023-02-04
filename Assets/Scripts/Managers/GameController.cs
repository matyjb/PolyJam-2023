using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private void Awake() {
        instance = this;
    }

    [Header("Settings")]
    public GameModes gameMode;

    [Header("Managers")]
    public PlayerMovement playerMovement;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    [Header("Components")]
    public Transform playerStartPosition;
    public Transform startScenePosition;
    public Transform rootBall;

    private void Start() {
        switch (gameMode) {
            case GameModes.Debug:
                playerMovement.SpawnFirstRoot();
                break;
            case GameModes.FirstPlanet:
                virtualCamera.Follow = startScenePosition;
                startScenePosition.LeanMoveLocalY(6.4f, 3f).setOnComplete(() => {
                    playerMovement.SpawnFirstRoot();
                });
                rootBall.LeanMove(new Vector3(0, 1.35f, 0), 0.4f).setEaseInCirc().delay = 2.8f;
                break;
        }
    }
}

public enum GameModes {
    Debug,
    FirstPlanet,
    NextPlanet
}
