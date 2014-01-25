using UnityEngine;
using System.Collections;

public class ToolControls : MonoBehaviour 
{
    public float speed;
    void Update() {
        Vector3 currentPosition = transform.position;
        if ( Input.GetKey( KeyCode.A ) ) {
            currentPosition.x -= speed * Time.deltaTime;
        }

        if ( Input.GetKey( KeyCode.D ) ) {
            currentPosition.x += speed * Time.deltaTime;
        }

        if ( Input.GetKey( KeyCode.W ) ) {
            currentPosition.z += speed * Time.deltaTime;
        }

        if ( Input.GetKey( KeyCode.S ) ) {
            currentPosition.z -= speed * Time.deltaTime;
        }

        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            currentPosition.y += speed * Time.deltaTime;
        }

        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            currentPosition.y -= speed * Time.deltaTime;
        }

        transform.position = currentPosition;
    }
}
