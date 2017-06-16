// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader"MyShaders/DiffuseSpecular"			//使用贴图纹理
{
	Properties
	{
		_Diffuse("Diffuse Color",Color) = (1,1,1,1)
		_Specular("Specular Color",Color) = (1,1,1,1)
		_Gloss("Gloss",Range(10,200)) = 10
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
			fixed4 _Diffuse;
			fixed4 _Specular;
			half _Gloss;
			struct a2v
			{
				float4 myVertex:POSITION;
				float3 myNormal:NORMAL;
				float4 myWorldVertex:TEXCOORD1;
			};
			struct v2f
			{
				float4 myPosition:SV_POSITION;
				float3 myWorldNormal:TEXCOORD0;
				float4 myWorldVertex:TEXCOORD1;
			};
			v2f vert(a2v _v)
			{
				v2f tfrag;
				tfrag.myPosition = mul(UNITY_MATRIX_MVP,_v.myVertex);
				tfrag.myWorldNormal = UnityObjectToWorldNormal(_v.myNormal);
				tfrag.myWorldVertex = mul(_v.myVertex,unity_WorldToObject);
				return tfrag;
			}
			fixed4 frag(v2f _f) :SV_Target
			{
				fixed4 tFinalColor;
				fixed3 tNormalDir = normalize(_f.myWorldNormal);
				fixed3 tLightDir = normalize(WorldSpaceLightDir(_f.myWorldVertex));
				fixed3 tDiffuse = _LightColor0.rgb*_Diffuse.rgb*max(dot(tNormalDir,tLightDir),0);//不需要透明度所以是fixed3
				fixed3 tViewDir = normalize(UnityWorldSpaceViewDir(_f.myWorldVertex));
				fixed3 tHalfDir = normalize(tLightDir + tViewDir);
				fixed3 tSpecular = _LightColor0.rgb*_Specular.rgb*pow(max(dot(tNormalDir,tHalfDir), 0), _Gloss); 
				fixed3 tTempColor = tDiffuse + tSpecular + UNITY_LIGHTMODEL_AMBIENT.rgb;
				tFinalColor = fixed4(tTempColor, 1);
				return tFinalColor;
			}

			ENDCG
		}
	}
		Fallback "Specular"
}