Shader"MyShaders/HalfLambertFragDiffuse"			//逐片元光照
{
	Properties
	{
		_Diffuse("Diffuse Color",Color) = (1,1,1,1)		//声明一个可以调控的漫反射颜色
	}
		SubShader
	{
		Pass
	{
		Tags{ "LightMode" = "ForwardBase" }
		CGPROGRAM
#include "Lighting.cginc"
#pragma	vertex vert
#pragma	fragment frag
		fixed4 _Diffuse;
	struct a2v
	{
		float4 myVertex:POSITION;
		float3 myNormal:NORMAL;
	};
	struct v2f
	{
		float4 myPosition:SV_POSITION;
		//fixed3 myDiffuseColor : COLOR;
		fixed3 worldNormalDir : COLOR0;
	};
	v2f vert(a2v _v)
	{
		v2f tfrag;
		tfrag.myPosition = mul(UNITY_MATRIX_MVP, _v.myVertex);
		/*fixed3 tAmbientColor = UNITY_LIGHTMODEL_AMBIENT.rgb;*/
		tfrag.worldNormalDir = mul(_v.myNormal,(float3x3)unity_WorldToObject);
		return tfrag;
	}
	fixed4 frag(v2f _f) :SV_Target
	{
		fixed3 tNormalDirNor = normalize(_f.worldNormalDir);			//获取法线方向
	fixed3 tLightDirNor = normalize(_WorldSpaceLightPos0.xyz);		//获取光照方向
	fixed3 tDiffuseColor = _LightColor0.rgb*(dot(tNormalDirNor, tLightDirNor)*0.5+0.5);		//使用半兰伯特替换原来的光照模型
	//tDiffuseColor += UNITY_LIGHTMODEL_AMBIENT.rgb;

	tDiffuseColor *= _Diffuse.rgb;
	return fixed4(tDiffuseColor, 1);
	}
		ENDCG
	}
	}
		Fallback"Diffuse"
}