using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    [Header("Settings")]
    public Color color;
    public Color grassColor;
    public bool isEvil = false;
    public float darkerColorAmount = 10;

    [Header("Components")]
    public SpriteRenderer trees1;
    public SpriteRenderer trees2;
    public SpriteRenderer grass;
    public GameObject evilGo;

    private void Start() {
        trees1.color = color;
        Color darkerColor = new Color(color.r - darkerColorAmount, color.g - darkerColorAmount, color.b - darkerColorAmount);
        trees2.color = darkerColor;
        grass.color = (grassColor + color) / 2;
        evilGo.SetActive(isEvil);
    }
}
