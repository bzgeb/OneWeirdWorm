using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;

public class AudioEventWindow : EditorWindow {
    static AudioEventContainer activeAudioEventContainer;
    static AudioClip activeAudioClip;
    static float[] activeAudioClipData;
    static float[] activeClipChannelOneData;
    static float[] activeClipChannelTwoData;
    static float scrollPosition;
    static Texture2D waveform1;
    static Texture2D waveform2;
    static Rect waveform1Rect;
    static Rect waveform2Rect;
    static bool dirty;
    static int selectedIndex;
    static float selectionPosition;
    static float selectedTime;

    [MenuItem ("Window/Audio Event")]
    static void Load() {
        AudioEventWindow window = AudioEventWindow.GetWindowWithRect<AudioEventWindow>( new Rect(100, 100, 400, 800 ) );
    }

    void Update() {
        AudioEventContainer audioEventContainer = Selection.activeObject as AudioEventContainer;
        if ( audioEventContainer == null ) {
            return;
        }
        activeAudioEventContainer = audioEventContainer;

        AudioClip obj = audioEventContainer.clip;
        if ( obj != null && obj != activeAudioClip ) {
            activeAudioClip = obj;
            activeAudioClipData = new float[activeAudioClip.samples * activeAudioClip.channels];
            activeClipChannelOneData = new float[activeAudioClip.samples];
            if ( activeAudioClip.channels == 2 ) {
                activeClipChannelTwoData = new float[activeAudioClip.samples];
            }

            activeAudioClip.GetData( activeAudioClipData, 0 );

            int c = 0;
            for ( int i = 0; i < activeAudioClipData.Length; i += 2 ) {
                activeClipChannelOneData[c] = activeAudioClipData[i];
                ++c;
            }

            if ( activeAudioClip.channels == 2 ) {
                c = 0;
                for ( int j = 1; j < activeAudioClipData.Length; j += 2 ) {
                    activeClipChannelTwoData[c] = activeAudioClipData[j];
                    ++c;
                }
            }
            dirty = true;

            selectionPosition = 0;
            selectedIndex = 0;
            selectedTime = 0;
        }

        if ( activeAudioClip != null ) {
            float percent = selectedIndex / (float)activeClipChannelOneData.Length;
            selectedTime = percent * activeAudioClip.length;
        }
    }

    void DrawWaveforms() {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        if ( dirty ) {
            String path = AssetDatabase.GetAssetPath( activeAudioClip );
            AudioImporter importer = AssetImporter.GetAtPath( path ) as AudioImporter;

            MethodInfo GetWaveform = audioUtilClass.GetMethod( "GetWaveForm", 
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static,
                null,
                new System.Type[] {
                    typeof( AudioClip ),
                    typeof( UnityEditor.AudioImporter ),
                    typeof( System.Int32 ),
                    typeof( System.Single ),
                    typeof( System.Single )
                },
                null
            );

            if ( GetWaveform != null ) {
                waveform1 = GetWaveform.Invoke( null, new System.Object[] { activeAudioClip, importer, 0, position.width, 200 } ) as Texture2D;
                if ( activeAudioClip.channels == 2 ) {
                    waveform2 = GetWaveform.Invoke( null, new System.Object[] { activeAudioClip, importer, 1, position.width, 200 } ) as Texture2D;
                } else {
                    waveform2 = null;
                }
            }
            dirty = false;
        }
    }

    void OnGUI() {
        DrawWaveforms();

        if ( activeAudioClip != null ) {
            ShowActiveGUI();
        } else {
            ShowInactiveGUI();
        }

        // if ( GUILayout.Button( "Some Button" ) ) {
        //     Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        //     Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        //     MethodInfo[] methods = audioUtilClass.GetMethods( BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static );
        //     foreach( MethodInfo m in methods ) {
        //         ParameterInfo[] p = m.GetParameters();
        //         Debug.Log( "Method: " + m.Name );
        //         foreach( ParameterInfo pinfo in p ) {
        //             Debug.Log( string.Format( "Param Name:{0} Type:{1}", pinfo.Name, pinfo.ParameterType ) );
        //         }
        //         Debug.Log( "Returns: " + m.ReturnType );
        //         Debug.Log( "*********" );
        //     }
        // }

        Event e = Event.current;
        if ( (e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0 ) {
            OnMouseClick();
        }

        if ( e.type == EventType.KeyDown ) {
            if ( e.keyCode == KeyCode.LeftArrow ) {
                DecrementIndex();
                e.Use();
            } else if ( e.keyCode == KeyCode.RightArrow ) {
                IncrementIndex();
                e.Use();
            } else if ( e.keyCode == KeyCode.C ) {
                CopyTimeToClipboard();
                e.Use();
            } else if ( e.keyCode == KeyCode.A ) {
                AddEventTime();
                e.Use();
            }

            float percent = selectedIndex / (float)activeClipChannelOneData.Length;
            selectionPosition = ( percent * waveform1Rect.width ) + waveform1Rect.x;
        }
    }

    void DecrementIndex() {
        selectedIndex -= (activeClipChannelOneData.Length / 1500);
        if ( selectedIndex < 0 ) {
            selectedIndex = 0;
        }
    }

    void IncrementIndex() {
        selectedIndex += (activeClipChannelOneData.Length / 1500);
        if ( selectedIndex > activeClipChannelOneData.Length ) {
            selectedIndex = activeClipChannelOneData.Length;
        }
    }

    void CopyTimeToClipboard() {
        EditorGUIUtility.systemCopyBuffer = ((selectedIndex / (float)activeClipChannelOneData.Length) * activeAudioClip.length).ToString();
    }

    void AddEventTime() {
        List<float> times = new List<float>( activeAudioEventContainer.eventTimes );
        times.Add( selectedTime );
        times.Sort();
        activeAudioEventContainer.eventTimes = times.ToArray();

        List<string> eventsList = new List<string>( activeAudioEventContainer.events );
        eventsList.Add( "None" );
        activeAudioEventContainer.events = eventsList.ToArray();;
    }

    // void RemoveEventTime( int index ) {
    //     List<float> times = new List<float>( activeAudioEventContainer.eventTimes );
    //     times.Remove( index );
    //     times.Sort();
    //     activeAudioEventContainer.eventTimes = times.ToArray();
    // }

    void OnMouseClick() {
        Event e = Event.current;
        bool isContained = false;

        isContained = waveform1Rect.Contains( e.mousePosition );
        if ( waveform2 != null && !isContained ) {
            isContained = waveform2Rect.Contains( e.mousePosition );
        }

        if ( isContained ) {
            float percent = (e.mousePosition.x - waveform1Rect.x) / waveform1Rect.width;
            selectedIndex = (int)( activeClipChannelOneData.Length * percent );
            selectionPosition = ( percent * waveform1Rect.width ) + waveform1Rect.x;
        }
        e.Use();
    }

    void ShowActiveGUI() {
        GUILayout.Label( activeAudioClip.name );
        if ( waveform1 != null ) {
            waveform1Rect = new Rect( 10, 20, position.width - 20, 200 );
            GUI.Box( waveform1Rect, "Channel 1" );
            GUI.DrawTexture( waveform1Rect, waveform1 );
            GUILayout.Space( 200 );
        }
        if ( waveform2 != null ) {
            waveform2Rect = new Rect( 10, 220, position.width - 20, 200 );
            GUI.Box( waveform2Rect, "Channel 2" );
            GUI.DrawTexture( waveform2Rect, waveform2 );
            GUILayout.Space( 210 );
        }

        if ( selectionPosition != 0 ) {
            if ( waveform2 != null ) {
                GUIHelper.DrawLine( new Vector2( selectionPosition, waveform1Rect.y ), new Vector2( selectionPosition, waveform2Rect.y + waveform2Rect.height ), Color.white );
            } else {
                GUIHelper.DrawLine( new Vector2( selectionPosition, waveform1Rect.y ), new Vector2( selectionPosition, waveform1Rect.y + waveform1Rect.height ), Color.white );
            }
        }

        foreach ( float time in activeAudioEventContainer.eventTimes ) {
            float pc = time / activeAudioClip.length;
            float pos = ( pc * waveform1Rect.width ) + waveform1Rect.x;
            if ( waveform2 != null ) {
                GUIHelper.DrawLine( new Vector2( pos, waveform1Rect.y ), new Vector2( pos, waveform2Rect.y + waveform2Rect.height ), Color.green );
            } else {
                GUIHelper.DrawLine( new Vector2( pos, waveform1Rect.y ), new Vector2( pos, waveform1Rect.y + waveform1Rect.height ), Color.green );
            }
        }

        GUILayout.Label( "Time: " + selectedTime );
        
        int sampleIndex = selectedIndex;
        if ( waveform2 != null ) {
            sampleIndex *= 2;
        }
        GUILayout.Label( "Sample: " + sampleIndex );
        GUILayout.Space( 10 );
        GUILayout.Label( "Use C key to copy time to clipboard" );
    }

    // void DrawWave( float[] data, int xOffset, int yOffset, float scrollPosition ) {
    //     int xPos = xOffset - ( (int)scrollPosition );
    //     int dataLength = data.Length;
    //     int offset = dataLength / 1000;

    //     for ( int i = 0; i < dataLength; i += offset ) {
    //         if ( i < dataLength && (i + offset) < dataLength ) {
    //             // Drawing.DrawLine( new Vector2( xPos, ( data[i] * 100 ) + yOffset ), 
    //             //     new Vector2( xPos + 1, ( data[i + offset] * 100 ) + yOffset ), 
    //             //     Color.white, 1, false );

    //             GUIHelper.DrawLine( new Vector2( xPos, ( data[i] * 100 ) + yOffset ), 
    //                 new Vector2( xPos + 1, ( data[i + offset] * 100 ) + yOffset ), 
    //                 Color.white );
    //             ++xPos;
    //         } else {
    //             // Drawing.DrawLine( new Vector2( xPos, ( data[i] * 100 ) + yOffset ), 
    //             //     new Vector2( xPos + 1, ( data[dataLength - 1] * 100 ) + yOffset ), 
    //             //     Color.white, 1, false );

    //             GUIHelper.DrawLine( new Vector2( xPos, ( data[i] * 100 ) + yOffset ), 
    //                 new Vector2( xPos + 1, ( data[dataLength - 1] * 100 ) + yOffset ), 
    //                 Color.white );
    //         }
    //     }
    // }

    void ShowInactiveGUI() {
        EditorGUILayout.LabelField( string.Format( "Selected Clip: None" ) );
    }
}
