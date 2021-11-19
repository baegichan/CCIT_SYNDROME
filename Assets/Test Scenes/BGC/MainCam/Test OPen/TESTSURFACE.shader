Shader "Custom/TESTSURFACE"
{
    Properties
    {
       
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2("Image1", 2D) = "white" {}
        _MainTex3("Image2",2D) = "white"{}
       
        _MoveSpeed("MoveSpeed",Range(0,1))=0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MainTex2;
        sampler2D _MainTex3;
        struct Input
        {
            float4 color :COLOR;
            float2 uv_MainTex;
            float2 uv_MainTex2;
              float2 uv_MainTex3;
        };

    
      
        float _MoveSpeed;
       
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
        
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex );
            fixed4 d = tex2D(_MainTex2, IN.uv_MainTex2);
            fixed4 e = tex2D(_MainTex3, IN.uv_MainTex3);
            o.Emission = lerp(e.rgb, d.rgb, c.a);

            //o.Alpha = c.r;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
