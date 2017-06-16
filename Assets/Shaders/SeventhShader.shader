Shader"MyShaders/SpecularFragmentShader"
{
	Properties
	{
		_Diffuse("Diffuse Color",Color) = (1,1,1,1)
		_Specular("Specular Color",Color) = (1,1,1,1)
		_Gloss("Gloss",Range(1,200)) = 10			//_Gloss 光泽度：值越大，亮圈越小
	}
		SubShader
	{
		Pass
		{
		Tags{"LightMode" = "ForwardBase"}
		CGPROGRAM
#include "Lighting.cginc"
#pragma	vertex vert
#pragma fragment frag
		fixed4 _Diffuse;
		half _Gloss;
		fixed4 _Specular;
		struct a2v
		{
			float4 myVertex : POSITION;
			float3 myNormal : NORMAL;
		};
		struct v2f
		{
			float4 myPosition : SV_POSITION;
			float3 worldNormalDir : TEXCOORD0;
			float3 worldVertex:TEXCOORD1;
		};
		v2f vert(a2v _v)
		{
			v2f tfrag;
			tfrag.myPosition = mul(UNITY_MATRIX_MVP, _v.myVertex);
			tfrag.worldNormalDir = mul(_v.myNormal, (float3x3)unity_WorldToObject);
			tfrag.worldVertex = mul(_v.myVertex,(float3x3)unity_WorldToObject).xyz;
			return tfrag;
		}
		fixed4 frag(v2f _f) :SV_Target
		{
			fixed3 tNormalDirNor = normalize(_f.worldNormalDir);		//获得片元的法线方向
			fixed3 tLightDirNor = normalize(_WorldSpaceLightPos0.xyz);	//获得光线入射方向
			fixed3 tDiffuseColor = _LightColor0.rgb*max(0, dot(tNormalDirNor, tLightDirNor));
			_f.worldNormalDir = tDiffuseColor*_Diffuse.rbg;			//漫反射
			//fixed3 tAmbient = UNITY_LIGHTMODEL_AMBIENT.rgb;
			fixed3 tReflectDirNor = normalize(reflect(-tLightDirNor, tNormalDirNor));		//获取反射光线的方向=reflect(入射光线方向，法线方向)
			fixed3 tCameraDirNor = normalize(_WorldSpaceCameraPos.xyz - _f.worldVertex);		//摄像机视野方向
			fixed3 tSpecularColor = _LightColor0.rgb*_Specular.rgb*pow(max(0, dot(tReflectDirNor, tCameraDirNor)), _Gloss);//高光反射，使用
			//整体的平行光或者是自定义的颜色作为光源
			_f.worldNormalDir = _f.worldNormalDir + tSpecularColor;
			return fixed4(_f.worldNormalDir, 1);
		}
		ENDCG
		}
	}
}