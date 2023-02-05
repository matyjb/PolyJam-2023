using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController instance;
    private void Awake() {
        instance = this;
    }

    [Header("Settings")]
    public GameModes gameMode;
    public float energyDepletionPerSec = 1;
    public float maxEnergyLevel = 20;

    [Header("Managers")]
    public PlayerMovement playerMovement;
    public Cinemachine.CinemachineVirtualCamera virtualCamera;
    public LandManager landManager;

    [Header("Components")]
    public Transform playerStartPosition;
    public Transform startScenePosition;
    public Transform rootBall;
    public SpriteRenderer whiteBlink;
    public TextMeshProUGUI energyText;
    public CanvasGroup winieta;

    [HideInInspector]
    public Player mainPlayer;

    private void Start() {
        winieta.alpha = 0;
        NextLevelManager.currentEnergyLevel = maxEnergyLevel;
        if (NextLevelManager.nextGameMode.HasValue) {
            gameMode = NextLevelManager.nextGameMode.Value;
        }
        switch (gameMode) {
            case GameModes.Debug:
                mainPlayer = playerMovement.SpawnFirstRoot();
                break;
            case GameModes.FirstPlanet:
                float timeToHit = 6f;
                virtualCamera.Follow = startScenePosition;
                whiteBlink.color = new Color(0, 0, 0, 0);
                whiteBlink.gameObject.SetActive(true);
                startScenePosition.LeanMoveLocalY(5.6f, timeToHit - 0.4f).setOnComplete(() => {
                    LeanTween.delayedCall(1f, () => {
                        mainPlayer = playerMovement.SpawnFirstRoot();
                    });

                });
                // Root ball
                rootBall.LeanMove(new Vector3(0, 1.35f, 0), 0.4f).setEaseInCirc().setDelay(timeToHit - 0.4f).setOnComplete(() => {
                    rootBall.gameObject.SetActive(false);
                });

                // White blink
                LeanTween.value(whiteBlink.gameObject, new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.35f), 1.2f).setDelay(timeToHit - 1.2f).setOnComplete(() => {
                    AudioManager.instance.PlayGamePlayMusic();
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

    private void Update() {
        if (mainPlayer != null) {
            NextLevelManager.currentEnergyLevel -= energyDepletionPerSec * Time.deltaTime;
            NextLevelManager.currentEnergyLevel = Mathf.Max(Mathf.Min(NextLevelManager.currentEnergyLevel, maxEnergyLevel), 0);
            if (NextLevelManager.currentEnergyLevel >= 10) {
                winieta.alpha = 0;
            } else {
                winieta.alpha = 1 - NextLevelManager.currentEnergyLevel / 10f;
            }
            energyText.text = "Energy: " + NextLevelManager.currentEnergyLevel.ToString("N2");
        }
    }

    public void GainEnergy(int amount = 1) {
        NextLevelManager.currentEnergyLevel += amount;
    }
}

public enum GameModes {
    Debug,
    FirstPlanet,
    NextPlanet
}
