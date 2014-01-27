using UnityEngine;
using System.Collections;

public class VideoRecorderController : MonoBehaviour 
{
    public iVidCapPro vr;
    void Start() {
        vr.RegisterSessionCompleteDelegate(HandleSessionComplete);
    }

    void BeginRecording() {
        vr.BeginRecordingSession( "Worm", 1024, 768, 30, iVidCapPro.CaptureAudio.Audio, iVidCapPro.CaptureFramerateLock.Unlocked );
    }

    void EndRecording() {
        int framesRecorded;
        vr.EndRecordingSession( iVidCapPro.VideoDisposition.Save_Video_To_Album, out framesRecorded );
    }

    void HandleSessionComplete() {
        Debug.Log("Session complete");
    }
}
