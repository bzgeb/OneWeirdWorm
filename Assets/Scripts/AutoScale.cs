using UnityEngine;
using System.Collections;

public class AutoScale : MonoBehaviour 
{
    public Vector3 minScale;
    public Vector3 maxScale;
    Vector3 currentScale;
    float lerpValue;
    Transform _transform;
    float startSeed;

    void Awake() {
        _transform = transform;
        startSeed = Random.Range( 0, 100 );
    }

    void Update() {
        lerpValue = (Mathf.Sin( startSeed + Time.timeSinceLevelLoad ) + 1) * 0.5f;

        currentScale = Vector3.Lerp( minScale, maxScale, lerpValue );

        _transform.localScale = currentScale;
    }
}
