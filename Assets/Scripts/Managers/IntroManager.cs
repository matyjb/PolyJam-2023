using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Transform rootBall;

    public static bool firstTime = true;
    bool waitingForPlayerStart = false;

    AsyncOperation ap;

    void Start()
    {
        rootBall.gameObject.SetActive(false);
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
        ap = SceneManager.LoadSceneAsync("GameScene");
        ap.allowSceneActivation = false;
    }

    private void Update() {
        if (waitingForPlayerStart && Input.GetKeyDown(KeyCode.Space)) {
            waitingForPlayerStart = false;
            StartIntroSequence();
        }
    }

    void StartIntroSequence() {
        AudioManager.instance.PlayIntroMusic();
        AudioManager.instance.PlaySound(0);
        float time = 10.2f;
        logo.LeanMoveY(10, 0.6f).setEaseInBack();
        rootBall.gameObject.SetActive(true);
        planet.GetComponent<Planet>().StopMoveAnimation();
        planet.LeanMove(planetPreGamePosition.position, time).setEaseInSine();
        planet.LeanScale(Vector3.one * 1.8f, time).setEaseInSine();
        planet.GetChild(0).GetChild(0).LeanScale(Vector3.one * 1.15f, time * 0.7f).setEaseInOutSine();
        spaceFrontChild.LeanScale(Vector3.one * 1.8f, time).setEaseInSine();
        rootBall.LeanMove(new Vector3(-1.45f, 1.4f, 0), time - 4f).delay = 4;
        rootBall.LeanScale(Vector3.one * 0.4f, time - 4f).delay = 4;
        LeanTween.delayedCall(time, () => {
            NextLevelManager.nextGameMode = GameModes.FirstPlanet;
            ap.allowSceneActivation = true;
        });
    }
}
