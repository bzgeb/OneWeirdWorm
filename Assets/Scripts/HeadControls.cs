using UnityEngine;
using System.Collections;

public class HeadControls : MonoBehaviour 
{
    void Update() {
        if ( Input.GetKeyDown( KeyCode.S ) ) {
            HeadBang();
        }
    }

    void HeadBang() {
        rigidbody.AddForce( 0, 0, -25f, ForceMode.Impulse );
    }
}
