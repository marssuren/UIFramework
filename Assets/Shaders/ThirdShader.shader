// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
Shader"MyShaders/DiffuseVertex"				//基于逐顶点光照的漫反射
{
	Properties
	{
		_Diffuse("Diffuse Color",Color) = (1,1,1,1)		//声明一个可以调控的漫反射颜色
	}
		SubShader
	{
		Pass
		{
			Tags{"LightMode" = "ForwardBase"}
			CGPROGRAM
#include "Lighting.cginc"					//引入内置的CG库来获得光照相关变量
#pragma		vertex	vert
#pragma		fragment	frag
			fixed4 _Diffuse;				//如果要使用属性中声明的变量，需要在CGPROGRAM中再次声明
			struct a2v
			{
				float4 myVertex:POSITION;
				float3 myNormal:NORMAL;
			};
			struct v2f
			{
				float4 myPosition:SV_POSITION;
				fixed3 myDiffuseColor : COLOR;
			};
			v2f vert(a2v _v)
			{
				v2f tfrag;
				tfrag.myPosition = mul(UNITY_MATRIX_MVP, _v.myVertex);
				fixed3 tambientColor = UNITY_LIGHTMODEL_AMBIENT.rgb;			//获取内置的环境光的颜色
				fixed3 tNormalDirNor = normalize(mul(_v.myNormal,(float3x3)unity_WorldToObject));//_World2Object	用来把一个方向从世界空间转换到模型空间,此处需要
				//的过程是相反的，因此只需要互换两个变量的位置。此外_World2Object是一个4x4的矩阵，因此需要先将其转换为3x3的矩阵再使用
				//归一化得到的世界空间下的法线

				fixed3 tLightDirNor = normalize(_WorldSpaceLightPos0.xyz);			//取得第一个直射光的位置(对于每一个顶点来说，光的位置
																			//就是光的方向，因为光是平行的)	_WorldSpaceLightPos0	
				//fixed3 tLightDirNor = normalize(tLightDirection);
				fixed3 tDiffuseColor = _LightColor0.rgb*max(dot(tNormalDirNor, tLightDirNor),0);	//_LightColor0取得第一个直射光的颜色,乘以0和
				//平行光与法线的夹角的余弦	得到漫反射的颜色

				tfrag.myDiffuseColor = tDiffuseColor*_Diffuse.rgb; //如果需要混合自定义颜色，只需要将两个颜色相乘
				tfrag.myDiffuseColor /= tambientColor;				//叠加环境光，直接相加
				return tfrag;
			}
			fixed4 frag(v2f _f) :SV_Target
			{
				return fixed4(_f.myDiffuseColor,1);
			}
			ENDCG
		}
	}
		Fallback	"Diffuse"
}