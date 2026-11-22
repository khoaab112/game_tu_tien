Shader "Tu Tien/Character Contact Shadow"
{
    SubShader
    {
        Tags { "Queue"="Transparent-5" "RenderType"="Transparent" }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment Fragment
            #include "UnityCG.cginc"

            fixed4 Fragment(v2f_img input) : SV_Target
            {
                float2 offset = input.uv * 2.0 - 1.0;
                float edge = smoothstep(0.23, 1.0, dot(offset, offset));
                return fixed4(0.08, 0.06, 0.04, 0.35 * (1.0 - edge));
            }
            ENDCG
        }
    }
}
