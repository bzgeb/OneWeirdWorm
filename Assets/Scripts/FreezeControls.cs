using UnityEngine;
using System.Collections;

public class FreezeControls : MonoBehaviour 
{
    public bool freeze;
    public bool wasFrozen;
    public Button[] freezeButtons;

    void Update() {
        freeze = Input.GetKey( KeyCode.S );
        foreach ( Button button in freezeButtons ) {
            if ( button.buttonIsDown ) {
                freeze = true;
                break;
            }
        }

        if ( wasFrozen && !freeze ) {
            UnFreeze();
        }

        if ( !wasFrozen && freeze ) {
            Freeze();
        }

        wasFrozen = freeze;
    }

    void Freeze() {
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    void UnFreeze() {
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
    }
}
