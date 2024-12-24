Shader "Custom/TriplanarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tiling ("Tiling", Float) = 1
        _BlendSharpness ("Blend Sharpness", Float) = 2
        _Tint ("Tint Color", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0, 1)) = 0.0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float _Tiling;
        float _BlendSharpness;
        fixed4 _Tint;
        float _Metallic;
        float _Smoothness;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 worldPos = IN.worldPos * _Tiling;
            float3 normal = abs(IN.worldNormal);

            // Blend weights
            float3 blend = pow(normal, _BlendSharpness);
            blend /= (blend.x + blend.y + blend.z);

            // Triplanar sampling
            float4 xTex = tex2D(_MainTex, worldPos.yz);
            float4 yTex = tex2D(_MainTex, worldPos.zx);
            float4 zTex = tex2D(_MainTex, worldPos.xy);

            // Blended color
            float4 finalColor = xTex * blend.x + yTex * blend.y + zTex * blend.z;

            // Apply tint
            finalColor.rgb *= _Tint.rgb;

            // Set outputs
            o.Albedo = finalColor.rgb;
            o.Alpha = finalColor.a;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }

        ENDCG
    }
    FallBack "Diffuse"
}