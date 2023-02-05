using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelManager : MonoBehaviour {
    public static int currentLevel = 0;
    public static int currentEnergyLevel { get; private set; }
    public static GameModes? nextGameMode = null;
    public static Color? color = null;

    [HideInInspector]
    public static NextLevelManager instance;
    private void Awake() {
        instance = this;
    }

    private void Start() {
        currentLevel++;
        LeanTween.value(gameObject, (value) => {
            Camera.main.orthographicSize = value;
        }, 3, 5, 1.5f).setOnComplete(() => {
            nextGameMode = GameModes.NextPlanet;
            SceneManager.LoadScene("GameScene");
        });
    }

    public static void GainEnergy(int amount = 1) {
        currentEnergyLevel += amount;
    }
}
