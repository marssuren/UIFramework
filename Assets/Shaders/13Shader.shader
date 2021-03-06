﻿Shader"MyShaders/NormalMapShader"			//支持法线贴图的shader
{
	Properties
	{
		_Color("Color",Color) = (1,1,1,1)
		_MainTex("Main Tex",2D) = "white"{}
		_NormalMap("NormalMap",2D) = "bump"{}			//bump不是一个颜色值，意思是当没有指定法线贴图时，就会使用这个模型自带的顶点法线进行插值
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
			sampler2D _NormalMap;
			float4 _NormalMap_ST;
			struct a2v
			{
				float4 myVertex:POSITION;
				float3 normal:NORMAL;		//不再需要模型自带法线进行计算，仍然需要法线，因为切线是由法线计算出来的，切线是垂直于法线的，切线空间是通过法线和切线进行计算的
				float4 tangent:TANGENT;			//因为要取到第四个分量 MyTangent.w	用来确定切线坐标空间中坐标轴的方向
				float4 myTexcoord:TEXCOORD0;
			};
			struct v2f
			{
				float4 svPos:SV_POSITION;
				//float3 worldNormal:TEXCOORD0;	//不再需要
				float3 lightDir:TEXCOORD0;		//切线空间下平行光的方向
				float4 worldVertex:TEXCOORD1;
				float4 uv:TEXCOORD2;			//xy用来存储MainTex的纹理坐标  zw用来存储法线贴图NormalMap的纹理坐标
			};
			v2f vert(a2v v)
			{
				v2f tfrag;
				tfrag.svPos = mul(UNITY_MATRIX_MVP, v.myVertex);
				//tfrag.worldNormal = UnityObjectToWorldNormal(_v.myNormal);
				tfrag.worldVertex = mul(v.myVertex, unity_WorldToObject);
				TANGENT_SPACE_ROTATION;			//调用这个宏后，会得到一个矩阵，用来把模型空间下的方向转换为切线空间下
				//ObjSpaceLightDir(v.myVertex)	//得到模型空间下的平行光方向
				tfrag.lightDir = mul(rotation, ObjSpaceLightDir(v.myVertex));
				//转换模型空间下的平行光方向为切线空间下的平行光方向
				tfrag.uv.xy = v.myTexcoord.xy*_MainTex_ST.xy + _MainTex_ST.zw;
				tfrag.uv.zw = v.myTexcoord.xy*_NormalMap_ST.xy + _NormalMap_ST.zw;



				return tfrag;
			}
			//把所有跟法线方向有关的运算都放在切线空间下
			//从法线贴图里面取得的法线方向是在切线空间下的
			fixed4 frag(v2f _f) :SV_Target
			{
				//fixed3 tNormalDir = normalize(_f.worldNormal);
				fixed4 normalColor = tex2D(_NormalMap,_f.uv.zw);			//取出法线贴图上每个片元的颜色
				//fixed3 tangentNormal = normalize(normalColor.xyz * 2 - 1);	//转为切线空间下的法线
				fixed3 tangentNormal = UnpackNormal(normalColor);	
				fixed3 tLightDir = normalize(_f.lightDir);
				fixed3 tTexColor = tex2D(_MainTex, _f.uv.xy)*_Color.rgb;
				fixed3 tDiffuse = _LightColor0.rgb*tTexColor*max(dot(tangentNormal, tLightDir), 0);
				fixed3 tTempColor = tDiffuse + UNITY_LIGHTMODEL_AMBIENT.rgb *tTexColor;
				return fixed4(tTempColor, 1);
			}
			ENDCG
		}
	}
		//Fallback "Specular"
}