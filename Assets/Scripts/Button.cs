using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public Events onClickEvent;
    public Camera uiCamera;
    public bool holdButton;
    public bool buttonIsDown;

    void Update() {
        buttonIsDown = false;
        if ( GetMouseDown() ) {
            foreach ( Touch touch in Input.touches ) {
                if ( CheckForHit( touch ) ) {
                    OnHit();
                }
            }
        }
    }

    bool CheckForHit( Touch touch ) {
        Ray ray = uiCamera.ScreenPointToRay( touch.position );
        RaycastHit hit;
        if ( Physics.Raycast( ray, out hit, 100.0f ) ) {
            if ( hit.collider == collider ) {
                return true;
            }
        }

        return false;
    }

    void OnHit() {
        EventManager.Push( onClickEvent.ToString() );
        buttonIsDown = true;
    }

    bool GetMouseDown() {
        if ( holdButton ) {
            foreach ( Touch touch in Input.touches ) {
                if ( touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled ) {
                    return true;
                }
            }
        } else {
            foreach ( Touch touch in Input.touches ) {
                if ( touch.phase == TouchPhase.Began ) {
                    return true;
                }
            }
        }

        return false;
    }
}
