using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class RockGen : MonoBehaviour {
    SpriteShapeController spriteShapeController;
    public float minCornerDistance = 2;
    public float maxCornerDistance = 10;
    // Start is called before the first frame update
    void Start() {
        spriteShapeController = GetComponent<SpriteShapeController>();
        GenRock();
    }

    // Update is called once per frame
    void Update() {

    }

    void GenRock() {
        int cornersCount = Random.Range(10, 16);
        List<float> distances = new List<float>();
        List<float> angles = new List<float>();

        for (int i = 0; i < cornersCount; i++) {
            distances.Add(Random.Range(minCornerDistance, maxCornerDistance));
            angles.Add(Random.Range(0, 360f));
        }
        distances.Sort();
        angles.Sort();

        var spline = spriteShapeController.spline;
        // remove current points
        spline.Clear(); // ?
        for (int i = cornersCount - 1; i >= 0; i--) {
            var insertPoint = Helpers.RadianToCartesianCoords(distances[i], angles[i]);
            spline.InsertPointAt(spline.GetPointCount(), insertPoint);
        }

        for (int i = 0; i < cornersCount; i++) {
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            spline.SetLeftTangent(i, spline.GetLeftTangent(i) / 2f);
            spline.SetRightTangent(i, spline.GetRightTangent(i) / 2f);
            //spline.SetHeight(newPointIndex, 1.0f);
        }

        spriteShapeController.BakeCollider();
    }
}
