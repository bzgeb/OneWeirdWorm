using UnityEngine;
using System.Collections;

public class TestMove : MonoBehaviour 
{
    bool jigRight;
    bool jigLeft;

    void Update() {
        if ( jigLeft ) {
            JigLeft();
        }

        if ( jigRight ) {
            JigRight();
        }

        if ( Input.GetKeyDown( KeyCode.UpArrow ) ) {
            JigUp();
        }
    }

    void SetJigRight() {
        jigRight = true;
    }

    void SetJigLeft() {
        jigLeft = true;
    }

    void JigRight() {
        Debug.Log("Right");
        rigidbody.AddForce( 15f, 0, 0, ForceMode.Impulse );
        jigRight = false;
    }

    void JigLeft() {
        Debug.Log("Left");
        rigidbody.AddForce( -15f, 0, 0, ForceMode.Impulse );
        jigLeft = false;
    }

    void JigUp() {
        rigidbody.AddForce( 0, 15f, 0, ForceMode.Impulse );
    }
}
