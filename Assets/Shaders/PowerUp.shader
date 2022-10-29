Shader "Unlit/PowerUp"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PlasmaTex ("Plasma Texture", 2D) = "white" {}
        _PowerColor ("Power Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 plas : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 plas : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _PlasmaTex;
            float4 _MainTex_ST;
            float4 _PlasmaTex_ST;
            float4 _PowerColor;

            v2f vert (appdata v)
            {
                v2f o;
                // powerup grows/shrinks over time based on sin function between 1 and 1.4 times the model size
                // makes the powerup more visible/appealing to the player
                float growthFactor = (1+(0.4*abs(sin(_Time.y))));
                v.vertex.xyz *= growthFactor;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.plas = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Combines normal texture with a plasma effect
                // plasma intensity is determined by secondary noise texture that moves over time
                // PowerupColor script sets plasma color
                fixed4 plasmaColor = _PowerColor*(0.6+(0.3*abs(sin(_Time.y))));
                fixed4 plasmaIntensity = tex2D(_PlasmaTex, i.plas + _Time.xy);
                fixed4 col = tex2D(_MainTex, i.uv) + plasmaColor*plasmaIntensity;
                return col;
            }
            ENDCG
        }
    }
}
