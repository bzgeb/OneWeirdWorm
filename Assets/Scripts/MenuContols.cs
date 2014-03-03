using UnityEngine;
using System.Collections;

public class MenuContols : MonoBehaviour 
{
    void Update() {
        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            Application.LoadLevel( 1 );
        }
    }
}
