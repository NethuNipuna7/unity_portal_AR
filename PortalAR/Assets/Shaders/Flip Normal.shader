Shader "Custom/Flip Normals" {
  Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
     _ZoomFactor ("Zoom Factor", Range(0.1, 10)) = 1.0
		
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 10000
		Cull front

		Pass
		{


Stencil{
	Ref 1
	Comp[_StencilComp]
}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normal: NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 normal: TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float _ZoomFactor;
			
			v2f vert (appdata v)
			{
				v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                // Calculate the center of the UV coordinates
                float2 uvCenter = 0.5;
                
                // Apply zoom-out effect around the center
                o.uv = uvCenter + (_ZoomFactor - 1) * (o.uv - uvCenter);
                
                o.normal = normalize(mul(v.normal, unity_WorldToObject));
                return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				 half4 col = tex2D(_MainTex, i.uv);
                return col;
			}
			ENDCG
		}
	}
}