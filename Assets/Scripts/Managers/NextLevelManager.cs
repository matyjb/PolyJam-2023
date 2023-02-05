using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelManager : MonoBehaviour
{
    public static int currentLevel = 0;
    public static GameModes? nextGameMode = null;

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
