using UnityEngine;

public class AutoRotate : MonoBehaviour {
    public float rateX = 1;
    public float rateY = 1;
    public float rateZ = 1;
    private Transform _transform;

    void Start() {
        _transform = transform;
    }

    void Update() {
        Quaternion localRotation = _transform.localRotation;
        localRotation = localRotation * Quaternion.Euler( rateX * Time.deltaTime, rateY * Time.deltaTime, rateZ * Time.deltaTime );

        _transform.localRotation = localRotation;
    } 
}