using UnityEngine;
using System.Collections;

public class SinColor : MonoBehaviour 
{
    SpriteRenderer sprite;
    Color currentColor;

    void Awake() {
        sprite = GetComponent<SpriteRenderer>();
        currentColor = sprite.color;
    }

    void Update() {
        float alpha = ( Mathf.Sin( Time.timeSinceLevelLoad * 6 ) + 1 ) * 0.5f;
        currentColor.a = alpha;

        sprite.color = currentColor;
    }
}
