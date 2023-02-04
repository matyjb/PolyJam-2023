using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Components")]
    public SpriteRenderer inner;
    public SpriteRenderer outer;

    private void Start() {
        outer.transform.eulerAngles = new Vector3(0, 0, 7);
        StartRotateAnimation();
        inner.transform.localPosition = new Vector3(0.16f, 0.15f, 0);
        StartMoveAnimation();
    }

    void StartRotateAnimation() {
        outer.transform.LeanRotateZ(-4, 5f).setEaseInOutQuint().setOnComplete(() => {
            outer.transform.LeanRotateZ(4, 5f).setEaseInOutQuint().setOnComplete(() => StartRotateAnimation());
        });
    }

    void StartMoveAnimation() {
        inner.transform.LeanMoveLocalY(0.5f, 7f).setEaseInOutQuint().setOnComplete(() => {
            inner.transform.LeanMoveLocalY(0.16f, 7f).setEaseInOutQuint().setOnComplete(() => StartMoveAnimation());
        });
    }
}
