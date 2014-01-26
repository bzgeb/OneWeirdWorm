using UnityEngine;
using UnityEditor;

[CustomEditor( typeof(AudioEventContainer) )]
public class AudioEventContainerGUI : Editor {
    SerializedProperty events;
    SerializedProperty eventTimes;
    SerializedProperty audioClip;

    void OnEnable() {
        audioClip = serializedObject.FindProperty( "clip" );
        events = serializedObject.FindProperty( "events" );
        eventTimes = serializedObject.FindProperty( "eventTimes" );
    }

    override public void OnInspectorGUI() {
        serializedObject.Update();

        audioClip.objectReferenceValue = EditorGUILayout.ObjectField( audioClip.objectReferenceValue, typeof( AudioClip ) );
        EditorGUILayout.LabelField( "Events" );
        for ( int i = 0; i < eventTimes.arraySize; ++i ) {
            SerializedProperty currentEvent = events.GetArrayElementAtIndex( i );
            SerializedProperty currentTime = eventTimes.GetArrayElementAtIndex( i );
            GUILayout.BeginHorizontal();
                currentEvent.stringValue = EditorGUILayout.TextField( currentEvent.stringValue, new GUILayoutOption[] { GUILayout.MaxWidth( 100 ) } );
                currentTime.floatValue = EditorGUILayout.FloatField( currentTime.floatValue );
                if ( GUILayout.Button( "Remove" ) ) {
                    eventTimes.DeleteArrayElementAtIndex( i );
                }
            GUILayout.EndHorizontal();
        }

        if ( GUILayout.Button( "Clear" ) ) {
            events.ClearArray();
            eventTimes.ClearArray();
        }

        serializedObject.ApplyModifiedProperties();
    }
}