using UnityEngine;
using System.Collections;

public class StartParticlesOnEvent : MonoBehaviour 
{
    public Events startEvent;
    public Events stopEvent;
    public bool playAudio;

    void OnEnable() {
        EventManager.Register( startEvent.ToString(), StartEvent );
        EventManager.Register( stopEvent.ToString(), StopEvent );
    }

    void OnDisable() {
        EventManager.Deregister( startEvent.ToString(), StartEvent );
        EventManager.Deregister( stopEvent.ToString(), StopEvent );
    }

    void StartEvent( params object[] args ) {
        particleSystem.Play();
        if ( playAudio ) {
            audio.Play();
        }
    }

    void StopEvent( params object[] args ) {
        particleSystem.Stop();
    }
}
