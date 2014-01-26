using UnityEngine;
using System.Collections;

public class AudioEventContainer : ScriptableObject
{
    public AudioClip clip;
    public string[] events;
    public float[] eventTimes;
    float[] eventSamples;
}
