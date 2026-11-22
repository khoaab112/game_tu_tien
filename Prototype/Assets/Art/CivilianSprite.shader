Shader "Tu Tien/Civilian Sprite"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [HideInInspector] _Color ("Tint", Color) = (1,1,1,1)
        [HideInInspector] _RendererColor ("Renderer Color", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "CanUseSpriteAtlas"="False" }
        Cull Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex SpriteVert
            #pragma fragment Fragment
            #include "UnitySprites.cginc"

            fixed4 Fragment(v2f input) : SV_Target
            {
                fixed4 color = SampleSpriteTexture(input.texcoord);
                // The source sheet is opaque. Remove its white matte at render time.
                half white = min(color.r, min(color.g, color.b));
                color.a *= 1 - smoothstep(0.87, 0.98, white);
                color *= input.color;
                color.rgb *= color.a;
                return color;
            }
            ENDCG
        }
    }
}
