using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour 
{
    public float speed;
    void Update() {
        AlterDirection();
        Move();
    }

    void AlterDirection() {
        Vector3 direction;
        direction = transform.forward;
        direction = Quaternion.AngleAxis(-2, Vector3.up) * direction;
        transform.forward = direction;
    }

    void Move() {
        Vector3 currentPosition = transform.position;
        currentPosition += transform.forward * (speed * Time.deltaTime);
        transform.position = currentPosition;
    }
}
