using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateAudioEventContainer : MonoBehaviour
{
    [MenuItem("Assets/Create/Audio Event Container")]
    public static void CreateAsset() {
       ScriptableObjectUtility.CreateAsset<AudioEventContainer>();
    }
}
