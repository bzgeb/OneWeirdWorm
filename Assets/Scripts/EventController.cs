using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour 
{
    public void SendEvent( Events ev ) {
        EventManager.Push( ev.ToString() );
    }

    IEnumerator SendEventOnTouch( Events ev ) {
        bool done = false;

        while ( !done ) {
            if ( Input.GetMouseButtonDown( 0 ) ) {
                EventManager.Push( ev.ToString() );
                done = true;
            }

            yield return null;
        }
    }

    void CancelEventOnTouch() {
        StopAllCoroutines();
    }
}
