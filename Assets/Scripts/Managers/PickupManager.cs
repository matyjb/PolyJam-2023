using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {

    public static PickupManager instance;
    private void Awake() {
        instance = this;
    }

    public List<GameObject> pickupsOnScene = new List<GameObject>();

    public void DestroyPickup(GameObject pickup) {
        pickupsOnScene.Remove(pickup);
        Destroy(pickup);
    }
}
