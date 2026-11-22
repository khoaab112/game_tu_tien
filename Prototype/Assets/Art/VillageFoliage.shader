Shader "Tu Tien/Village Foliage"
{
    Properties
    {
        _MainTex ("Source sheet", 2D) = "white" {}
        _Frame ("Frame UV (left, bottom, width, height)", Vector) = (0,0,1,1)
        _Wind ("Wind (amplitude, rate, phase)", Vector) = (0.035,1.4,0,0)
        _Tint ("Foliage tint", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent-20" "RenderType"="Transparent" }
        Cull Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment Fragment
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Frame;
            float4 _Wind;
            fixed4 _Tint;

            fixed4 Fragment(v2f_img input) : SV_Target
            {
                const float horizontalPadding = 1.16;
                float2 uv = input.uv;
                float aboveRoots = saturate((uv.y - 0.06) / 0.94);
                float phase = _Time.y * _Wind.y + _Wind.z;
                float gust = sin(phase) + 0.24 * sin(2.1 * phase - 0.7);
                uv.x = (uv.x - 0.5) * horizontalPadding + 0.5
                    - gust * _Wind.x * aboveRoots * aboveRoots;
                clip(min(uv.x, 1.0 - uv.x));

                fixed3 color = tex2D(_MainTex, _Frame.xy + uv * _Frame.zw).rgb;
                // Remove the white JPEG background without changing the source sheets.
                half white = min(color.r, min(color.g, color.b));
                half alpha = 1.0 - smoothstep(0.48, 0.94, white);
                clip(alpha - 0.008);
                fixed3 premultiplied = clamp(color - (1.0 - alpha), 0.0, alpha);
                return fixed4(premultiplied * _Tint.rgb, alpha);
            }
            ENDCG
        }
    }
}
