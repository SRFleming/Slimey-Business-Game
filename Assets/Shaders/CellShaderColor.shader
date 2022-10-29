// code inspired by https://roystan.net/articles/toon-shader/, https://en.wikipedia.org/wiki/Blinn%E2%80%93Phong_reflection_model

Shader "Unlit/CellShaderColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Brightness("Brightness",  Range(0, 1)) = 1
        _AmbientColor ("Ambient Color", Color) = (0.3,0.3,0.3,1)
        _AmbientStrength("Ambient Strength",  Range(0, 1)) = 1
        _Glossiness ("Glossiness", Float) = 30 // The higher the glossiness, the more focused the specular highlights
        _RimAmount("Rim Amount", Range(0, 1)) = 0.7 // The higher the rim amount, the smaller the rim around edges
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1 
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { 
            "RenderType"="Opaque"
            }
        LOD 100

        Pass
        {
            Tags {
            "LightMode"="ForwardBase"
            "PassFlags"="OnlyDirectional"
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
			#include "AutoLight.cginc"

            uniform float4 _Color; 
            uniform float4 _SpecularColor;
            uniform float4 _AmbientColor;
            uniform float _AmbientStrength;
            uniform float _Brightness;
            uniform float _Glossiness;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;
            
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                TRANSFER_SHADOW(o) // for using unity's built in shadows
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.normal);
                float3 lightDirection = normalize(_WorldSpaceLightPos0);
                float shadow = SHADOW_ATTENUATION(i); // for using unity's built in shadows

                // diffuse component of lighting, restricted to two values for cel shading effect
                float NdotL = dot(lightDirection,normal);
                float rNdotL;
                if(NdotL < 0) {rNdotL = 0;}
                else {rNdotL = 1;}
                rNdotL = smoothstep(0,0.01,NdotL*shadow);

                // specular reflection component of the lighting using the blinn phong lighting model 
                float3 halfV = normalize(lightDirection + i.viewDir);
                float NdotH = dot(normal, halfV);
                float specular = pow(NdotH*rNdotL, _Glossiness*_Glossiness);
                specular = smoothstep(0.005,0.01,specular);

                // lighting on the rim around the edge of an object, gives 
                float4 rimDot = 1 - dot(i.viewDir, normal);
                rimDot = rimDot * pow(NdotL, _RimThreshold);
                rimDot = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);

                fixed4 col = _Color;
                return col*(_AmbientColor*_AmbientStrength + rNdotL*0.65  + specular*0.65 + rimDot)*_Brightness;
            }
            ENDCG
        }
        // Uses unity shadowcaster pass after cellshading effect is calculated
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
