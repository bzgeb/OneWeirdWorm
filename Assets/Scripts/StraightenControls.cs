using UnityEngine;
using System.Collections;

public class StraightenControls : MonoBehaviour 
{
    Vector3 initialPosition;
    bool straighten;
    public Button[] straightenButtons;

    void Awake() {
        initialPosition = transform.position;
    }

    void FixedUpdate() {
        rigidbody.isKinematic = false;
        straighten = false;
        foreach ( Button button in straightenButtons ) {
            if ( button.buttonIsDown ) {
                straighten = true;
                break;
            }
        }
        
        if ( straighten || Input.GetKey( KeyCode.W ) ) {
            Straighten();
        }
    }

    void SetStraighten() {
        straighten = true;
    }

    void Straighten() {
        rigidbody.position = initialPosition;
        rigidbody.isKinematic = true;
        straighten = false;
    }
}
