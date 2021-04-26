Shader "Custom/Caustics"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
		// Caustics Texture 1
        _CausticsTex1("Caustics 1 (RGB)", 2D) = "white" {}
        _Caustics_ST1("Caustics 1 ST", Vector) = (1,1,0,0) // Tiling X, Tiling Y, Offset X, Offset Y
        _CausticsSpeed1("Caustics 1 Animation", Vector) = (1, 1, 0, 0) // Animation Vector
		
		// Caustics Texture 2
		_CausticsTex2("Caustics 2 (RGB)", 2D) = "white" {}
        _Caustics_ST2("Caustics 2 ST", Vector) = (1,1,0,0) // Tiling X, Tiling Y, Offset X, Offset Y
        _CausticsSpeed2("Caustics 2 Animation", Vector) = (1, 1, 0, 0) // Animation Vector
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

        sampler2D _CausticsTex1;
        float4 _Caustics_ST1;
        float2 _CausticsSpeed1;

		sampler2D _CausticsTex2;
        float4 _Caustics_ST2;
        float2 _CausticsSpeed2;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // Caustics sampling
            fixed2 uv1 = IN.uv_MainTex * _Caustics_ST1.xy + _Caustics_ST1.zw;
            uv1 += _CausticsSpeed1 * _Time.y * 0.1;

			fixed2 uv2 = IN.uv_MainTex * _Caustics_ST2.xy + _Caustics_ST2.zw;
            uv2 += _CausticsSpeed2 * _Time.y * 0.1;

            fixed3 caustics = min(tex2D(_CausticsTex1, uv1).rgb, tex2D(_CausticsTex2, uv2).rgb);

            // Add
            o.Albedo.rgb += caustics * 10;
 
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

        }ENDCG
    }
    FallBack "Diffuse"
}
