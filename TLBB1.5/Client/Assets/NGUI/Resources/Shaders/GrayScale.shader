Shader "GrayScale" {

    Properties {

        _Color ("Main Color", Color) = (1,1,1,1)

        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}

        _Grey ("Grey Value", Float) = 1

    }

    

    SubShader {

    

        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

            

        Pass

        {

            ZWrite Off

            Blend SrcAlpha OneMinusSrcAlpha

                        

            CGPROGRAM

            #pragma vertex vert_img

            #pragma fragment frag

            #pragma fragmentoption ARB_precision_hint_fastest 

 

            #include "UnityCG.cginc"

 

            sampler2D _MainTex;

            float4 _Color;

            float _Grey;

        

            float luminance(float3 c)

            {

                return dot( c, float3(0.3, 0.59, 0.11) );

            }

            

            float4 frag (v2f_img i) : COLOR

            {   

                float4 c = tex2D(_MainTex, i.uv) * _Color;

                float4 o = c;

                if (_Grey > 0.5) o.rgb = luminance(c.rgb);

                o.a = c.a;

                

                return o;

            }

 

            ENDCG

        }

    } 

    

    FallBack "Transparent/VertexLit"

}