Shader "Custom/TrueLastTest1"
{
    Properties
    {
        
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _HoloColor("HoloColor",Color)=(0,0,0,0)
        _HoloColor2("HoloColor2",Color)=(0,0,0,0)
        _HoloSpeed1("HoloSpeed1",Float)=0
        _HoloSpeed2("HoloSpeed2",Float)=0
        _HoloLine1("HoloLine1",Float)=0
        _HoloLine2("HoloLine2",Float)=0
        _RimColor("RimColor",Color) = (1,1,1,1)
        _RimPower("RimPower", Range(0, 10)) = 3
        _OutLinePower("OutLinePower", Range(0.0, 1.0)) =0
    }
    SubShader
    {
        Tags { "RenderType"="Transparant""Queue"="Transparent" }
        LOD 200
        
         CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert noambient vertex:vert alpha:fade 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
       float _OutLinePower;
       float4 _RimColor;
       float4 _RimPower;
        struct Input
        {
            float2 uv_MainTex;
             float3 viewDir;
            float3 worldPos;
        };

     
        void vert(inout appdata_full v)
        {
          v.vertex.xyz += v.normal*_OutLinePower;
        }
        void surf (Input IN, inout SurfaceOutput o)
        {
  
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);    
            float4 rim = saturate(dot(o.Normal, IN.viewDir));           
            o.Emission = pow(1-rim,4)*_RimColor;
            o.Alpha=0.6;       
        }
        ENDCG
            CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert noambient vertex:vert alpha:fade 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
     
       float4 _RimColor;
       float4 _RimPower;
        struct Input
        {
            float2 uv_MainTex;
             float3 viewDir;
            float3 worldPos;
        };

     
        void vert(inout appdata_full v)
        {
         // v.vertex.xyz += v.normal*0.01;
        }
        void surf (Input IN, inout SurfaceOutput o)
        {
  
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);    
            float4 rim = saturate(dot(o.Normal, IN.viewDir));           
            o.Emission = pow(1-rim,4)*_RimColor;
            o.Alpha=0.1;       
        }
        ENDCG
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert noambient alpha:fade 

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
         float4 _HoloColor;
        float4 _HoloColor2;
         sampler2D _BumpMap;
           float _RimPower;
           float _HoloSpeed1;
           float _HoloSpeed2;
           float _HoloLine2;
           float _HoloLine1;
          float4 _RimColor;
        struct Input
        {
            float2 uv_MainTex;
             float3 viewDir;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
  
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) ;
            float4 test1 = frac(IN.worldPos.y*_HoloLine1+_Time.y*_HoloSpeed1);
            float4 test2 = frac(IN.worldPos.y*_HoloLine2-_Time.y*_HoloSpeed2);
            float4 rim = dot(o.Normal, IN.viewDir);
            //How to culling?? ƒ√∏µ∏∏«œ∏Èµ…≈Ÿµ• »Ï..
            float4 test3 = round(test1);

            o.Emission = pow(1-rim,2)*_RimColor + test3*_HoloColor + test2*_HoloColor2;
            if(test3.r==0)
            {
              o.Alpha=0.5;
            }
            else
            {
              o.Alpha=0.0;
            }
          
         
          
        }
        ENDCG
    }
    FallBack "Diffuse"
}
