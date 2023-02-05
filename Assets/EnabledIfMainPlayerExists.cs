using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnabledIfMainPlayerExists : MonoBehaviour {
    TextMeshProUGUI textMeshProUGUI;
    
    // Start is called before the first frame update
    void Start() {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        textMeshProUGUI.enabled = GameController.instance.mainPlayer != null;
    }
}
