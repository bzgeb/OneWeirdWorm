using UnityEngine;
using System.Collections;

public class Flap : MonoBehaviour 
{
    public float lowerLimit;
    public float upperLimit;
    public bool goingDown;

    void Update() {
        if ( goingDown ) {
            RotateDown();
        } else {
            RotateUp();
        }
    }

    void RotateDown() {
        if ( transform.rotation.eulerAngles.z < lowerLimit ) {
            goingDown = false;
            return;
        }

        transform.Rotate( new Vector3(0, 0, -1), 350.0f * Time.deltaTime );
    }

    void RotateUp() {
        if ( transform.rotation.eulerAngles.z > upperLimit ) {
            goingDown = true;
            return;
        }

        transform.Rotate( new Vector3(0, 0, 1), 350.0f * Time.deltaTime );
    }
}
