using UnityEngine;
using System.Collections;

public class ButtonFeedback : MonoBehaviour 
{
    public Button button;
    public Vector3 pressedScale;
    float elapsedTime;
    public float pressTime;
    Vector3 initialScale;

    void Awake() {
        initialScale = transform.localScale;
    }

    void Update() {
        bool resetScale = false;

        if ( button.buttonIsDown ) {
            ModifyScale( pressedScale );

            if ( !button.holdButton ) {
                elapsedTime += Time.deltaTime;
                if ( elapsedTime > pressTime ) {
                    resetScale = true;
                }
            }
        } else {
            elapsedTime = 0;
            resetScale = true;
        }

        if ( resetScale ) {
            ModifyScale( initialScale );
        }
    }

    void ModifyScale( Vector3 newScale ) {
        transform.localScale = newScale;
    }
}
