using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [Header("Spawn positions")]
    public Transform planetStartGamePosition;

    [Header("Components")]
    public Transform planet;
    public Transform spaceMushroom;

    public static bool firstTime = true;

    void Start()
    {
        if (firstTime) {
            AudioManager.instance.PlayIntroMusic();
        }
    }
}
