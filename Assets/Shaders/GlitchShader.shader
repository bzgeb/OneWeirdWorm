Shader "Custom/GlitchShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            sampler2D _MainTex;

            float rand(float2 co){
                return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
            }

            struct vertexInput {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
                float4 color : COLOR;
            };
             
            struct vertexOutput {
                float4 pos : SV_POSITION;
                float4 scrPos : TEXCOORD1;
            };

            vertexOutput vert(vertexInput input)  {
                vertexOutput output;

                output.pos = mul( UNITY_MATRIX_MVP, input.vertex );
                output.scrPos = ComputeScreenPos( output.pos );

                return output;
            }

            float4 frag(vertexOutput input) : COLOR {
                float2 wcoord = (input.scrPos.xy / input.scrPos.w);
                wcoord.x /= 4;
                wcoord.y /= 100;
                wcoord.x += tex2D(_MainTex, wcoord.xy).z + _Time[2];
                wcoord.x = sin( wcoord.x * 200 ) + _SinTime[2] * 3;
                wcoord.y += tex2D(_MainTex, wcoord.yx).x + _Time[0];
                wcoord.y = sin( wcoord.y * 200 ) + _CosTime[3] * 2;

                return fixed4( wcoord.x, sin(wcoord.y) + cos(wcoord.y), cos(wcoord.y), 1.0 );
            }


            ENDCG 
        }
    }
}
