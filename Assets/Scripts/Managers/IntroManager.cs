using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Start colors")]
    public Color[] startColors;

    [Header("Spawn positions")]
    public Transform planetStartGamePosition;
    public Transform planetPreGamePosition;

    [Header("Components")]
    public Transform planet;
    public Transform spaceMushroom;
    public Transform logo;
    public Transform spaceBack;
    public Transform spaceFront;
    public Transform spaceFrontChild;

    public static bool firstTime = true;
    bool waitingForPlayerStart = false;

    void Start()
    {
        spaceBack.LeanMove(new Vector3(-3, 0, 0), 35f).setLoopPingPong();
        spaceFront.LeanMove(new Vector3(-3, 0, 0), 20f).setLoopPingPong();
        if (firstTime) {
            firstTime = false;
            waitingForPlayerStart = true;
            planet.transform.position = planetStartGamePosition.position;
            planet.transform.localScale = Vector3.one * 1.2f;
            Color startColor = startColors[Random.Range(0, startColors.Length)];
            NextLevelManager.color = startColor;
        } else {
            planet.transform.position = planetPreGamePosition.position;
        }
        planet.GetComponent<Planet>().ChangeColor(NextLevelManager.color.Value);
    }

    private void Update() {
        if (waitingForPlayerStart && Input.GetKeyDown(KeyCode.Space)) {
            waitingForPlayerStart = false;
            StartIntroSequence();
        }
    }

    void StartIntroSequence() {
        AudioManager.instance.PlayIntroMusic();
        planet.GetComponent<Planet>().StopMoveAnimation();
        planet.LeanMove(planetPreGamePosition.position, 12f).setEaseInSine();
        planet.LeanScale(Vector3.one * 1.8f, 12f).setEaseInSine();
        planet.GetChild(0).GetChild(0).LeanScale(Vector3.one * 1.15f, 6f);
        spaceFrontChild.LeanScale(Vector3.one * 1.8f, 10f).setEaseInSine();
    }
}
