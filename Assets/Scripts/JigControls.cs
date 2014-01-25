using UnityEngine;
using System.Collections;

public class JigControls : MonoBehaviour 
{
    bool jigRight;
    bool jigLeft;
    bool jigUp;
    bool jigDown;
    bool jigForward;
    bool jigBackward;

    void Update() {
        if ( jigLeft || Input.GetKeyDown( KeyCode.LeftArrow ) ) {
            JigLeft();
        }

        if ( jigRight || Input.GetKeyDown( KeyCode.RightArrow ) ) {
            JigRight();
        }

        if ( jigUp || Input.GetKeyDown( KeyCode.UpArrow ) ) {
            JigUp();
        }

        if ( jigDown ) {
            JigDown();
        }

        if ( jigForward ) {
            JigForward();
        }

        if ( jigBackward ) {
            JigBackward();
        }
    }

    void SetJigRight() {
        jigRight = true;
    }

    void SetJigLeft() {
        jigLeft = true;
    }

    void SetJigUp() {
        jigUp = true;
    }

    void SetJigDown() {
        jigDown = true;
    }

    void SetJigForward() {
        jigForward = true;
    }

    void SetJigBackward() {
        jigBackward = true;
    }

    void JigRight() {
        rigidbody.AddForce( 15f, 0, 0, ForceMode.Impulse );
        jigRight = false;
    }

    void JigLeft() {
        rigidbody.AddForce( -15f, 0, 0, ForceMode.Impulse );
        jigLeft = false;
    }

    void JigUp() {
        rigidbody.AddForce( 0, 15f, 0, ForceMode.Impulse );
    }

    void JigDown() {
        rigidbody.AddForce( 0, -15f, 0, ForceMode.Impulse );
    }

    void JigBackward() {
        rigidbody.AddForce( 0, 0, 15f, ForceMode.Impulse );
    }

    void JigForward() {
        rigidbody.AddForce( 0, 0, -15f, ForceMode.Impulse );
    }
}
