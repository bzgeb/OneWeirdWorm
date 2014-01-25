using UnityEngine;
using System.Collections;

public class HeadControls : MonoBehaviour 
{
    Vector3 initialPosition;

    void Awake() {
        initialPosition = transform.position;
    }

    void Update() {
        if ( Input.GetKey( KeyCode.W ) ) {
            Straighten();
        }

        if ( Input.GetKeyDown( KeyCode.S ) ) {
            HeadBang();
        }
    }

    void Straighten() {
        rigidbody.position = initialPosition;
    }

    void HeadBang() {
        rigidbody.AddForce( 0, 0, -25f, ForceMode.Impulse );
    }
}
