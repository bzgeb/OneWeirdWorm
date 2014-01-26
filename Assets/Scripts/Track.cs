using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour 
{
    void Awake() {
        Invoke( "StartTrack", 1.0f );
    }

    void StartTrack() {
        audio.Play();
        Invoke( "StartGroundParticles", 31f );
        Invoke( "StartBackgroundParticles", 154.5f );
    }

    void StartGroundParticles() {
        EventManager.Push( Events.StartGroundParticles.ToString() );
    }

    void StartBackgroundParticles() {
        EventManager.Push( Events.StartBackgroundParticles.ToString() );
    }
}
