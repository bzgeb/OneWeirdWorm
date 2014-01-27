using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour 
{
    void Awake() {
        Invoke( "StartTrack", 1.0f );
    }

    void StartTrack() {
        EventManager.Push( Events.BeginRecording.ToString() );
        Invoke( "SignalEndOfSong", audio.clip.length );
        // Invoke( "SignalEndOfSong", 20f );

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

    void SignalEndOfSong() {
        EventManager.Push( Events.EndRecording.ToString() );
    }
}
