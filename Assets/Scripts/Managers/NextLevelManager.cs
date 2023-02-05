using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelManager : MonoBehaviour {
    public static int currentLevel = 0;
    public static float currentEnergyLevel = 0;
    public static GameModes? nextGameMode = null;
    public static Color? color = null;

    private void Start() {
        currentLevel++;
        LeanTween.value(gameObject, (value) => {
            Camera.main.orthographicSize = value;
        }, 3, 5, 1.5f).setOnComplete(() => {
            nextGameMode = GameModes.NextPlanet;
            SceneManager.LoadScene("GameScene");
        });
    }
}
