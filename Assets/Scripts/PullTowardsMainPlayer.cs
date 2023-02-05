using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PullTowardsMainPlayer : MonoBehaviour {
    Player mainPlayer { get => GameController.instance.mainPlayer; }
    public float minDistance = 2;
    public AnimationCurve positionCurve;
    public AnimationCurve scaleCurve;
    float t = 0;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (mainPlayer != null && Vector3.Distance(transform.position, mainPlayer.transform.position) < minDistance) {
            t += Time.deltaTime;
            float duration = 1;
            float animTime = Mathf.Min(duration, t);
            transform.position = Vector3.Lerp(transform.position, mainPlayer.transform.position,  positionCurve.Evaluate(animTime));
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 0.05f,  scaleCurve.Evaluate(animTime));
        }
    }
}
