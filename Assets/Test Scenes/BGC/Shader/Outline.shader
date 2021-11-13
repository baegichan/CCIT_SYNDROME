Shader "Custom/Outline"
{
    Properties
    {
       //_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Texture", 2D) = "white" {}
       _TestVal ("Test",float)=0
       _MulCol ("MulCol",Color)=(1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        //sampler2D _MainTex;
        sampler2D _MainTex2;
        float _TestVal;
        float4 _MulCol;
        struct Input
        {
           // float2 uv_MainTex;
            float2 uv_MainTex2;
        };
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 Maincha = tex2D (_MainTex2, IN.uv_MainTex2);
            fixed4 A1 = tex2D (_MainTex2, float2(IN.uv_MainTex2.x+_TestVal, IN.uv_MainTex2.y ));
            fixed4 A2 = tex2D (_MainTex2, float2(IN.uv_MainTex2.x-_TestVal, IN.uv_MainTex2.y ));
            fixed4 B1 = tex2D (_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y+_TestVal ));
            fixed4 B2 = tex2D (_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y-_TestVal ));
           
            fixed4 A12 = A1.a+A2.a;
            fixed4 B12 = B1.a+B2.a;

            fixed4 WhiteAB = clamp(A12+B12,0,1);
            fixed4 LineAB = WhiteAB.a - Maincha.a;
            o.Emission = Maincha.rgba + (LineAB*_MulCol);
            //o.Alpha =0;
     

            



        }
        ENDCG
    }
    FallBack "Diffuse"
}
