// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader"MyShaders/SingleTexture"			//使用贴图纹理
{
	Properties
	{
		//_Diffuse("Diffuse Color",Color) = (1,1,1,1)
		_Color("Color",Color) = (1,1,1,1)
		_MainTex("MainTexture",2D) = "white"{}
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
			//fixed4 _Diffuse;
			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;		//S-Scale：缩放		  T-Translation：偏移
			fixed4 _Specular;
			half _Gloss;
			struct a2v
			{
				float4 myVertex:POSITION;
				float3 myNormal:NORMAL;
				float4 textCoord:TEXCOORD0;
			};
			struct v2f
			{
				float4 myPosition:SV_POSITION;
				float3 myWorldNormal:TEXCOORD0;
				float4 myWorldVertex:TEXCOORD1;
				float2 myUV:TEXCOORD2;
			};
			v2f vert(a2v _v)
			{
				v2f tfrag;
				tfrag.myPosition = mul(UNITY_MATRIX_MVP,_v.myVertex);
				tfrag.myWorldNormal = UnityObjectToWorldNormal(_v.myNormal);
				tfrag.myWorldVertex = mul(_v.myVertex,unity_WorldToObject);
				tfrag.myUV = _v.textCoord.xy*_MainTex_ST.xy + _MainTex_ST.zw;			//在顶点着色器里面取到横坐标和竖坐标
				return tfrag;
			}
			fixed4 frag(v2f _f) :SV_Target
			{
				fixed4 tFinalColor;
				fixed3 tNormalDir = normalize(_f.myWorldNormal);
				fixed3 tLightDir = normalize(WorldSpaceLightDir(_f.myWorldVertex));
				fixed3 tTexColor = tex2D(_MainTex, _f.myUV.xy)*_Color.rgb;		//根据传入的纹理坐标取得纹理颜色
				fixed3 tDiffuseColor = _LightColor0.rgb*tTexColor.rgb*max(dot(tNormalDir,tLightDir),0);//使用纹理颜色取代漫反射颜色
				fixed3 tViewDir = normalize(UnityWorldSpaceViewDir(_f.myWorldVertex));
				fixed3 tHalfDir = normalize(tLightDir + tViewDir);
				fixed3 tSpecular = _LightColor0.rgb*_Specular.rgb*pow(max(dot(tNormalDir,tHalfDir), 0), _Gloss);
				fixed3 tTempColor = tDiffuseColor + tSpecular + UNITY_LIGHTMODEL_AMBIENT.rgb*tTexColor;		//使环境光和纹理色融合
				tFinalColor = fixed4(tTempColor, 1);
				return tFinalColor;
			}

			ENDCG
		}
		}
			Fallback "Specular"
}