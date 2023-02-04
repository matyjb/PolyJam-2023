using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Spawn positions")]
    public Transform planetStartGamePosition;
    public Transform planetPreGamePosition;

    [Header("Components")]
    public Transform planet;
    public Transform spaceMushroom;
    public Transform logo;

    public static bool firstTime = true;
    bool waitingForPlayerStart = false;

    void Start()
    {
        if (firstTime) {
            firstTime = false;
            waitingForPlayerStart = true;
            planet.transform.position = planetStartGamePosition.position;
            planet.transform.localScale = Vector3.one * 1.2f;
        } else {
            planet.transform.position = planetPreGamePosition.position;
        }
    }

    private void Update() {
        if (waitingForPlayerStart && Input.GetKeyDown(KeyCode.Space)) {
            waitingForPlayerStart = false;
            StartIntroSequence();
        }
    }

    void StartIntroSequence() {
        AudioManager.instance.PlayIntroMusic();
        planet.LeanMove(planetPreGamePosition.position, 2f).setEaseOutCubic();
        planet.LeanScale(Vector3.one, 2f).setEaseOutCubic();
    }
}
