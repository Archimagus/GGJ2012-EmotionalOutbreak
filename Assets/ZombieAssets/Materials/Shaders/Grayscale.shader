Shader "GrayscaleLolTransparent" {
    Properties 
	{
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }

    SubShader 
	{
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha

            sampler2D _MainTex;

            struct Input 
			{
                float2 uv_MainTex;
            };

            void surf (Input IN, inout SurfaceOutput o) 
			{
                o.Alpha = 0;
            }

        ENDCG
    }

    Fallback "Transparent/VertexLit"
}