using UnityEngine;
using System.Collections;

public class SongSelectControls : MonoBehaviour 
{
    public int selectedSong;
    public GameObject[] songs;

    void Awake() {
        foreach ( GameObject song in songs ) {
            song.audio.time = 15.0f;
        }
    }

    void Update() {
        selectedSong = Mathf.Clamp( selectedSong, 0, songs.Length - 1 );

        // Hide the unselected songs and lower audio
        for ( int i = 0; i < songs.Length; ++i ) {
            if ( i == selectedSong ) {
                continue;
            }

            foreach( GUIText t in songs[i].GetComponentsInChildren<GUIText>() ) {
                t.color = Color.white;
            }

            songs[i].audio.volume = 0;
        }

        // Show the selected song and play audio
        foreach ( GUIText t in songs[selectedSong].GetComponentsInChildren<GUIText>() ) {
            t.color = new Color( 1, 0.6274509804f, 0.02352941176f, 1 );
        }
        songs[selectedSong].audio.volume = 1;


        if ( Input.GetKeyDown( KeyCode.DownArrow ) || Input.GetKeyDown( KeyCode.RightArrow ) ) {
            ++selectedSong;
        }

        if ( Input.GetKeyDown( KeyCode.UpArrow ) || Input.GetKeyDown( KeyCode.LeftArrow ) ) {
            --selectedSong;
        }

        if ( Input.GetKeyDown( KeyCode.Return ) ) {
            Application.LoadLevel( songs[selectedSong].GetComponent<SceneIndex>().sceneIndex );
        }
    }
}
