Shader "Unlit/Outline3"
{
  Properties
    {
     _MainTex ("Texture", 2D) = "white" {}
        _MainTex2 ("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1,1,1,1)
    }
    SubShader
    {
		Tags
		{
			"RenderType" = "Transparent"
		}
 
		Blend SrcAlpha OneMinusSrcAlpha
 
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
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex2;
            float4 _MainTex2_ST;
            float4 _MainTex2_TexelSize;
 
			fixed4 _OutlineColor;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex2);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex2, i.uv);
 
				fixed leftPixel = tex2D(_MainTex2, i.uv + float2(-_MainTex2_TexelSize.x, 0)).a;
				fixed upPixel = tex2D(_MainTex2, i.uv + float2(0, _MainTex2_TexelSize.y)).a;
				fixed rightPixel = tex2D(_MainTex2, i.uv + float2(_MainTex2_TexelSize.x, 0)).a;
				fixed bottomPixel = tex2D(_MainTex2, i.uv + float2(0, -_MainTex2_TexelSize.y)).a;
 
				fixed outline = (1 - leftPixel * upPixel * rightPixel * bottomPixel) * col.a;
               
                return lerp(col, _OutlineColor, outline);
               
            }
            ENDCG
        }
    }
}