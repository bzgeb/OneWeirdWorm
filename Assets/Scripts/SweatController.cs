using UnityEngine;
using System.Collections;

public class SweatController : MonoBehaviour 
{
    public float sweatTime;
    float elapsedTime;

    void Update() {
        elapsedTime += Time.deltaTime;

        if ( elapsedTime > sweatTime ) {
            particleSystem.Emit( 5 );
            elapsedTime = 0;
        }
    }
}
