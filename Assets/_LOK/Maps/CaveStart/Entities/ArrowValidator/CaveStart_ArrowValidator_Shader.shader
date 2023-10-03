Shader "Unlit/CaveStart_ArrowValidator_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BorderColor("BorderColor", Color) = (1, 1, 1, 1)
        _EmptyColor("EmptyColor", Color) = (0, 0, 0, 1)
        _FillRatio ("FillRatio", Range(0.0, 1.0)) = 0.0
        _FillColor("FillColor", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            ZTest Always
    	    Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                uint id : SV_VertexID;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _BorderColor;
            float4 _EmptyColor;

            float _FillRatio;
            float _FillDir;
            float4 _FillColor;

            float3 ReplaceColor(float3 In, float3 From, float3 To, float Range);

            float2 GenerateUVCoord(int index)
            {
                if (index == 0) return float2(0, 1);
                if (index == 1) return float2(1, 1);
                if (index == 2) return float2(0, 0);
                return float2(1, 0);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = GenerateUVCoord(v.id);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                float3 col1 = col.rgb;
                float3 col2 = col.rgb;
                float3 fillColor = col.rgb;
                float3 whiteColor = float3(1, 1, 1);
                float3 blackColor = float3(0, 0, 0);

                col1 = ReplaceColor(col1, whiteColor, _BorderColor, 0.1f);
                col2 = ReplaceColor(col2, blackColor, _EmptyColor, 0.1f);
                col2 = ReplaceColor(col2, whiteColor, blackColor, 0.1f);

                fillColor = ReplaceColor(fillColor, blackColor, _FillColor, 0.1f);
                fillColor = ReplaceColor(fillColor, whiteColor, blackColor, 0.1f);

                //return fixed4((i.uv2), 0, 1);
                if (i.uv2.x > _FillRatio) return fixed4((col1 + col2).rgb, col.a);
                return fixed4((col1 + fillColor).rgb, col.a);
            }

            float3 ReplaceColor(float3 In, float3 From, float3 To, float Range)
            {
                float Distance = distance(From, In);
                return lerp(To, In, saturate((Distance - Range)));
            }
            ENDCG
        }
    }
}