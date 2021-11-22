Shader "Custom/EdgeBlender"
{
    Properties
    {
        _BlendMargin ("Blend margin", float) = 0.0675 //2px for a 32x32 tile
    
        [PerRendererData]_MainTex ("Center texture", 2D) = "white" {}
        
        [PerRendererData]_BlendLeft ("Blend left texture", int) = 0
        [PerRendererData]_LeftTex ("Left texture", 2D) = "white" {}
        
        [PerRendererData]_BlendRight ("Blend right texture", int) = 0
        [PerRendererData]_RightTex ("Right texture", 2D) = "white" {}
        
        [PerRendererData]_BlendTop ("Blend top texture", int) = 0
        [PerRendererData]_TopTex ("Top texture", 2D) = "white" {}
        
        [PerRendererData]_BlendBottom ("Blend bottom texture", int) = 0
        [PerRendererData]_BottomTex ("Bottom texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        CGPROGRAM
        #pragma surface surf Standard
        #pragma target 3.5
        
        float _BlendMargin;

        sampler2D _MainTex;
        
        int _BlendLeft;
        sampler2D _LeftTex;
        
        int _BlendRight;
        sampler2D _RightTex;
        
        int _BlendTop;
        sampler2D _TopTex;
        
        int _BlendBottom;
        sampler2D _BottomTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_LeftTex;
            float2 uv_RightTex;
            float2 uv_TopTex;
            float2 uv_BottomTex;
        };
        
        // In a domain of [0; 1]:
        // Scale from -1 to 0 on interval [0; margin]
        // 0 on interval [margin; 1-margin]
        // Scale from 0 to 1 on inteval [1-margin; 1]
        float marginCalc(float position, float margin)
        {
            return sign(position - 0.5) * clamp(abs(position - 0.5) - (0.5 - margin), 0, margin) / margin;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 main = tex2D(_MainTex, IN.uv_MainTex);
            float2 pos = IN.uv_MainTex.xy;
            
            fixed4 right = tex2D(_RightTex, IN.uv_RightTex);
            fixed4 left = tex2D(_LeftTex, IN.uv_LeftTex);
            fixed4 top = tex2D(_TopTex, IN.uv_TopTex);
            fixed4 bottom = tex2D(_BottomTex, IN.uv_BottomTex);
            
            // How much into the margins are we?
            // Absolute magnitude is from 0 (inside or margin begins) to 1 (edge of tile)
            // Sign signifies direction (postitive for right and top, negative for left and bottom)
            float marginX = marginCalc(pos.x, _BlendMargin);
            float marginY = marginCalc(pos.y, _BlendMargin);
            
            // Blend power tells us how much of foreign tiles will be mixed in
            // Goes from 0 inside and at inner margin edges, up to 0.5 on tile edges
            float blendPower = max(abs(marginX), abs(marginY)) / 2.0;
            
            // Which adjacent tiles will even play a role in the mix?
            bool leftContributes = (_BlendLeft == 1) && (marginX < 0);
            bool rightContributes = (_BlendRight == 1) && (marginX > 0);
            bool topContributes = (_BlendTop == 1) && (marginY > 0);
            bool bottomContributes = (_BlendBottom == 1) && (marginY < 0);
            
            // Mix ratio between two adjacent textures within the corner
            float cornerMixRatio = abs(marginY) / (abs(marginX) + abs(marginY));
            
            fixed4 result;
            
            if (leftContributes && topContributes) {
                fixed4 mixin = lerp(
                    left,
                    top,
                    cornerMixRatio);
                result = lerp(main, mixin, blendPower);
            } else if (leftContributes && bottomContributes) {
                fixed4 mixin = lerp(
                    left,
                    bottom,
                    cornerMixRatio);
                result = lerp(main, mixin, blendPower);
            } else if (rightContributes && topContributes) {
                fixed4 mixin = lerp(
                    right,
                    top,
                    cornerMixRatio);
                result = lerp(main, mixin, blendPower);
            } else if (rightContributes && bottomContributes) {
                fixed4 mixin = lerp(
                    right,
                    bottom,
                    cornerMixRatio);
                result = lerp(main, mixin, blendPower);
            } else if (leftContributes) {
                result = lerp(main, left, blendPower);
            } else if (rightContributes) {
                result = lerp(main, right, blendPower);
            } else if (topContributes) {
                result = lerp(main, top, blendPower);
            } else if (bottomContributes) {
                result = lerp(main, bottom, blendPower);
            } else {
                result = main;
            }
            
            o.Albedo = result;
        }
        ENDCG
    }
}