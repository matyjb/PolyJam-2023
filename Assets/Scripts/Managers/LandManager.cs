using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManager : MonoBehaviour
{
    [Header("Settings")]
    public Color color;
    public Color multiplyColor;
    public Color grassColor;
    public float darkerColorAmount = 10;

    [Header("Components")]
    public SpriteRenderer trees1;
    public SpriteRenderer trees2;
    public SpriteRenderer grass;
    public SpriteRenderer bush;
    public Transform alien2;

    [Header("Evil")]
    public GameObject evilGo;
    public GameObject notEvilAlien1;
    public GameObject evilAlien1;
    public GameObject notEvilAlien2;
    public GameObject evilAlien2;

    private void Start() {
        if (NextLevelManager.color.HasValue) {
            color = NextLevelManager.color.Value;
        }
        trees1.color = color;
        Color darkerColor = DarkenColor(color);
        trees2.color = darkerColor;
        bush.color = darkerColor;
        grass.color = (grassColor + color) / 2;
        alien2.LeanMoveLocalX(-6.5f, 4f);
        ChangeEvil(false);
    }

    public void ChangeEvil(bool isEvil) {
        evilGo.SetActive(isEvil);
        evilAlien1.SetActive(isEvil);
        notEvilAlien1.SetActive(!isEvil);
        evilAlien2.SetActive(isEvil);
        notEvilAlien2.SetActive(!isEvil);
    }

    Color DarkenColor(Color color) {
        Color output = (color * multiplyColor);
        output.a = 1;
        return output;
        //float h, s, v;
        //Color.RGBToHSV(color, out h, out s, out v);
        //h -= 0.083f;
        //if (h < 0) {
        //    h += 1f;
        //}
        //v -= 0.25f;
        //if (v < 0) {
        //    v += 1f;
        //}
        //return Color.HSVToRGB(h, s, v);
    }
}
