Shader"MyShaders/SpecularVertexShader"
{
	Properties
	{
		_Diffuse("Diffuse Color",Color) = (1,1,1,1)
		_Specular("Specular Color",Color)=(1,1,1,1)
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
			fixed3 worldNormalDir : COLOR0;
		};
		v2f vert(a2v _v)
		{
			v2f tfrag;
			tfrag.myPosition = mul(UNITY_MATRIX_MVP, _v.myVertex);
			tfrag.worldNormalDir = mul(_v.myNormal, (float3x3)unity_WorldToObject);
			fixed3 tNormalDirNor = normalize(tfrag.worldNormalDir);
			fixed3 tLightDirNor = normalize(_WorldSpaceLightPos0.xyz);
			fixed3 tDiffuseColor = _LightColor0.rgb*max(0, dot(tNormalDirNor, tLightDirNor));		

			tfrag.worldNormalDir = tDiffuseColor*_Diffuse.rbg;			//漫反射
			//fixed3 tAmbient = UNITY_LIGHTMODEL_AMBIENT.rgb;
			fixed3 tReflectDirNor = normalize(reflect(-tLightDirNor, tNormalDirNor));		//获取反射光线的方向=reflect(入射光线方向，法线方向)
			fixed3 tCameraDirNor = normalize(_WorldSpaceCameraPos.xyz - mul(_v.myVertex, unity_WorldToObject));		//摄像机视野方向
			fixed3 tSpecularColor = _LightColor0.rgb*_Specular.rgb*pow(max(0, dot(tReflectDirNor, tCameraDirNor)), _Gloss);		//高光反射，使用
			//整体的平行光或者是自定义的颜色作为光源
			tfrag.worldNormalDir =    tDiffuseColor+ tSpecularColor;
			return tfrag;
		}
		fixed4 frag(v2f _f) :SV_Target
		{
			return fixed4(_f.worldNormalDir, 1);
		}
		ENDCG
		}
	}
}