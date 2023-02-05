using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    [Header("Components")]
    public SpriteRenderer inner;
    public SpriteRenderer outer;

    [Header("Settings")]
    public Color multiplyColor;

    // Temp
    int moveAnimationId = -1;

    private void Start() {
        outer.transform.eulerAngles = new Vector3(0, 0, 7);
        StartRotateAnimation();
        inner.transform.localPosition = new Vector3(0.16f, 0.15f, 0);
        StartMoveAnimation();
    }

    public void ChangeColor(Color color) {
        inner.color = color;

        //outer.color = NegativeColor(color);
    }

    void StartRotateAnimation() {
        outer.transform.LeanRotateZ(-4, 5f).setEaseInOutSine().setOnComplete(() => {
            outer.transform.LeanRotateZ(4, 5f).setEaseInOutSine().setOnComplete(() => StartRotateAnimation());
        });
    }

    void StartMoveAnimation() {
        moveAnimationId = inner.transform.LeanMoveLocalY(0.5f, 7f).setEaseInOutSine().setOnComplete(() => {
            moveAnimationId = inner.transform.LeanMoveLocalY(0.16f, 7f).setEaseInOutSine().setOnComplete(() => StartMoveAnimation()).id;
        }).id;
    }

    Color DarkenColor(Color color) {
        Color output = (color * multiplyColor);
        output.a = 1;
        return output;
    }

    Color NegativeColor(Color color) {
        Color.RGBToHSV(color, out float H, out float S, out float V);
        float negativeH = (H + 0.5f) % 1f;
        Color negativeColor = Color.HSVToRGB(negativeH, S, V);
        return negativeColor;
    }

    public void StopMoveAnimation() {
        LeanTween.cancel(moveAnimationId);
    }
}
