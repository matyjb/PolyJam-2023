using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helpers {
    public static Vector2 RadianToCartesianCoords(float distance, float degrees) {
        float radian = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);
        return new Vector2(x, y) * distance;
    }

    public static T ChooseObjectWithChances<T>(List<T> objects, List<int> chances) {
        if (objects.Count != chances.Count) {
            throw new System.Exception("Both lists must be equal length");
        }

        int chancesSum = chances.Sum();
        int winningChanceIndex = Random.Range(0, chancesSum);

        int s = 0;
        for (int i = 0; i < objects.Count; i++) {
            s += chances[i];
            if (winningChanceIndex < s) {
                return objects[i];
            }
        }
        return objects.Last();
    }
}
