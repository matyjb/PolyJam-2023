using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBackgroundsManagers : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject undergroundTilePrefab;

    public int firstOrder = -80;
    public float firstYmove = -4.5f;
    public float nextYMove = -8f;
    public Sprite randomBackgroundsFirst;
    public Sprite[] randomBackgroundNext2;
    public Sprite[] finish;

    private void Start() {
        SpawnBackgrounds();
    }

    void SpawnBackgrounds() {
        for (int i = 0; i < 6; i++) {
            GameObject newGo = Instantiate(undergroundTilePrefab, transform);
            newGo.transform.position = new Vector3(0, firstYmove + (i * nextYMove), 0);
            Sprite sprite;
            if (i == 0) {
                sprite = randomBackgroundsFirst;
            } else if (i <= 2) {
                sprite = randomBackgroundNext2[Random.Range(0, randomBackgroundNext2.Length)];
            } else {
                sprite = finish[i - 3];
            }
            SpriteRenderer spriteRenderer = newGo.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            float value = 1f;
            if (i <= 3)
                value = Random.Range(0.9f - i * (0.05f), 1f - i * (0.05f));
            spriteRenderer.color = new Color(value, value, value);
            spriteRenderer.sortingOrder = -80 - i;
        }
        
    }

}
