using UnityEngine;
using System.Collections;

public class Track : MonoBehaviour 
{
    public int menuSceneIndex;
    public float startGroundParticles;
    public float startBackgroundParticles;

    void Awake() {
        Invoke( "StartTrack", 1.0f );
    }

    void StartTrack() {
        EventManager.Push( Events.BeginRecording.ToString() );
        Invoke( "SignalEndOfSong", audio.clip.length );

        audio.Play();
        Invoke( "StartGroundParticles", startGroundParticles );
        Invoke( "StartBackgroundParticles", startBackgroundParticles );
    }

    void StartGroundParticles() {
        EventManager.Push( Events.StartGroundParticles.ToString() );
    }

    void StartBackgroundParticles() {
        EventManager.Push( Events.StartBackgroundParticles.ToString() );
    }

    void SignalEndOfSong() {
        EventManager.Push( Events.EndRecording.ToString() );
        Invoke( "LoadMenu", 3.0f );
    }

    void LoadMenu() {
        Application.LoadLevel( menuSceneIndex );
    }
}
