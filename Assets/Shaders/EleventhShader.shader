// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'


Shader"MyShaders/RockTexture"			//岩石shader
{
	Properties
	{
		_Color("Color",Color) = (1,1,1,1)
		_MainTex("Main Tex",2D) = "white"{}
	}
		SubShader
	{
		Pass
		{
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM
#include "Lighting.cginc"
#pragma vertex vert
#pragma fragment frag
			float4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			struct a2v
			{
				float4 myVertex:POSITION;
				float3 myNormal:NORMAL;
				float4 myTexcoord:TEXCOORD0;
			};
			struct v2f
			{
				float4 svPos:SV_POSITION;
				float3 worldNormal:TEXCOORD0;
				float4 worldVertex:TEXCOORD1;
				float2 uv:TEXCOORD2;
			};
			v2f vert(a2v _v)
			{
				v2f tfrag;
				tfrag.svPos = mul(UNITY_MATRIX_MVP, _v.myVertex);
				tfrag.worldNormal = UnityObjectToWorldNormal(_v.myNormal);
				tfrag.worldVertex = mul(_v.myVertex, unity_WorldToObject);
				tfrag.uv = _v.myTexcoord.xy*_MainTex_ST.xy + _MainTex_ST.zw;
				return tfrag;
			}
			fixed4 frag(v2f _f):SV_Target
			{
				fixed3 tNormalDir = normalize(_f.worldNormal);
				fixed3 tLightDir = normalize(WorldSpaceLightDir(_f.worldVertex));
				fixed3 tTexColor = tex2D(_MainTex, _f.uv.xy)*_Color.rgb;
				fixed3 tDiffuse = _LightColor0.rgb*tTexColor*max(dot(tNormalDir, tLightDir), 0);
				fixed3 tTempColor = tDiffuse + UNITY_LIGHTMODEL_AMBIENT.rgb *tTexColor;
				return fixed4(tTempColor, 1);
			}
			ENDCG
		}
	}
		Fallback "Specular"
}