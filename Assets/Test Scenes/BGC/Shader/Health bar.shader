Shader "Unlit/Health bar"
{
    Properties
    {
        _CaseTex("HP", 2D) = "white"{}
        _HpEffect("Hpeffect",2D)="white"{} 
        _MainTex ("Texture", 2D) = "white" {}
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
                float2 uv2 : TEXCOORD1;
                float2 uv3 : TEXCOORD2;
            };
            //
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD2;
                float2 uv3 : TEXCOORD3;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _CaseTex;
            sampler2D _HpEffect;
            float4 _MainTex_ST;
            float4 _HpEffect_ST;
            float4 _CaseTex_ST;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv2, _HpEffect);
                o.uv3 = TRANSFORM_TEX(v.uv3, _CaseTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col3 = tex2D(_CaseTex,i.uv3);
                fixed4 col2 = tex2D(_HpEffect,i.uv2);

                col.a *= col3.r;
                //col 는 렌더텍스쳐로해야될거같음 아마도
                //col2 는 위 HP 데코레이션이미지
                //col3 는 테두리이미지

                return col;
            }
            ENDCG
        }
    }
}
