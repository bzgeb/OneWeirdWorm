using UnityEngine;
using System.Collections;

public class RotateForwardControls : MonoBehaviour 
{
    bool rotateForward;
    bool rotateBackward;
    public Button[] rotateForwardButtons;
    public Button[] rotateBackwardButtons;

    void FixedUpdate() {
        foreach ( Button button in rotateForwardButtons ) {
            if ( button.buttonIsDown ) {
                rotateForward = true;
                break;
            }
        }

        foreach ( Button button in rotateBackwardButtons ) {
            if ( button.buttonIsDown ) {
                rotateBackward = true;
                break;
            }
        }

        if ( rotateForward || Input.GetKey( KeyCode.E) ) {
            RotateForward();
        }

        if ( rotateBackward || Input.GetKey( KeyCode.Q) ) {
            RotateBackward();
        }
    }

    void SetRotateForward() {
        rotateForward = true;
    }

    void SetRotateBackward() {
        rotateBackward = true;
    }

    void RotateForward() {
        rigidbody.AddTorque( -300, 0, 0, ForceMode.Impulse );
        rotateForward = false;
    }

    void RotateBackward() {
        rigidbody.AddTorque( 300, 0, 0, ForceMode.Impulse );
        rotateBackward = false;
    }
}
