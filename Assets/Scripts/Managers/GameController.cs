using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    private void Awake() {
        instance = this;
    }

    [Header("Settings")]
    public GameModes gameMode;

    [Header("Managers")]
    public PlayerMovement playerMovement;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public LandManager landManager;

    [Header("Components")]
    public Transform playerStartPosition;
    public Transform startScenePosition;
    public Transform rootBall;
    public SpriteRenderer whiteBlink;

    [HideInInspector]
    public Player mainPlayer;

    private void Start() {
        if (NextLevelManager.nextGameMode.HasValue) {
            gameMode = NextLevelManager.nextGameMode.Value;
        }
        switch (gameMode) {
            case GameModes.Debug:
                mainPlayer = playerMovement.SpawnFirstRoot();
                break;
            case GameModes.FirstPlanet:
                virtualCamera.Follow = startScenePosition;
                whiteBlink.color = new Color(0, 0, 0, 0);
                whiteBlink.gameObject.SetActive(true);
                startScenePosition.LeanMoveLocalY(5.6f, 3f).setOnComplete(() => {
                    LeanTween.delayedCall(1.9f, () => {
                        mainPlayer = playerMovement.SpawnFirstRoot();
                    });
                    
                });
                rootBall.LeanMove(new Vector3(0, 1.35f, 0), 0.4f).setEaseInCirc().setDelay(2.8f).setOnComplete(() => {
                    rootBall.gameObject.SetActive(false);
                });
                LeanTween.value(whiteBlink.gameObject, new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.15f), 1f).setDelay(2f).setOnComplete(() => {
                    whiteBlink.color = Color.white;
                    landManager.ChangeEvil(true);
                    LeanTween.value(whiteBlink.gameObject, Color.white, new Color(255, 255, 255, 0), 2f).delay = 0.5f;
                });
                break;
            case GameModes.NextPlanet:
                virtualCamera.Follow = startScenePosition;
                landManager.ChangeEvil(true);
                mainPlayer = playerMovement.SpawnFirstRoot();
                break;
        }
    }
}

public enum GameModes {
    Debug,
    FirstPlanet,
    NextPlanet
}
