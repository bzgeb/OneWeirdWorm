using UnityEngine;
using System.Collections;

public class MenuText : MonoBehaviour 
{
    public Color fontColor;
    public GUIText text;

    float hue;
    float saturation;

    void Start() {
        // StartCoroutine( UpdateFontSize() );
        InvokeRepeating( "ChangeFontSize", 0.000001f, 0.27272727272727f );
    }

    void Update() {
        UpdateFontColor();
    }

    void UpdateFontColor() {
        hue = hue + Random.Range( 0.5f, 4f );
        saturation = saturation + Random.Range( 0.0035f, 0.005f );

        hue = hue % 256;
        saturation = Mathf.Max((saturation % 1.0f), 0.3f);

        fontColor = ColorExt.ColorFromHSV( hue, saturation, 0.35f, 1 );

        text.color = fontColor;
    }

    void ChangeFontSize() {
        text.fontSize = Random.Range( 25, 45 );
    }

    IEnumerator UpdateFontSize() {
        float elapsed = 0;
        float changeTime = 0.25f;

        while ( true ) {
            if ( elapsed > changeTime ) {
                elapsed = 0;
                changeTime = Random.Range( 0.12f, 0.30f );
                // ChangeFontSize();
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
