using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public Events onClickEvent;
    public Camera uiCamera;
    public bool holdButton;

    void Update() {
        if ( GetMouseDown() ) {
            Ray ray = uiCamera.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
                if ( hit.collider == collider ) {
                    EventManager.Push( onClickEvent.ToString() );
                }
            }
        }
    }

    bool GetMouseDown() {
        if ( holdButton ) {
            if ( Input.GetMouseButton( 0 ) ) {
                return true;
            }
        } else {
            if ( Input.GetMouseButtonDown( 0 ) ) {
                return true;
            }
        }

        return false;
    }
}
