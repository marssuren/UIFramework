Shader"MyShaders/SecondShader"
{
	Properties{
	}
	SubShader{
		Pass{
		CGPROGRAM
#pragma vertex	vert			//#pragma:固定写法	vertex:声明为顶点函数 ver：声明顶点函数名		基本作用：完成顶点坐标从
		//模型空间到裁剪空间(从建模环境到相机屏幕上)的转换
#pragma fragment	frag		//pragma：声明片元函数名	fragment:声明为片元函数	frag:声明片元函数名			基本作用：返回模型对应屏幕
								//上的每一个像素颜色值
		struct a2v {						//定义结构体：application to vertex		用于存储输入的结构体
		float4 myVertex:POSITION;			//告诉unity把模型空间下的顶点坐标填充给myVertex
		float4 myNormal:NORMAL;			//告诉unity把模型空间下的法线方向填充给MyNormal
		float4 myTexcoord:TEXCOORD0;		//告诉unity把第一套纹理坐标填充给myTexcoord
};
		struct v2f {							//vertex to fragment
			float4 position:SV_POSITION;		//如果去掉SV_POSITION，代表不返回它在屏幕空间中的位置，从而无法显示
			float3 temp:COLOR0;					//COLOR0相当于一个中间量
		};										//和POSITION用法并无不同。唯一区别是 SV_POSTION一旦被作为vertex shader的输出语义，
												//那么这个最终的顶点位置就被固定了(不能tensellate，不能再被后续改变它的空间位置？)，
												//直接进入光栅化处理，如果作为fragment shader的输入语义那么和POSITION是一样的，
												//代表着每个像素点在屏幕上的位置（这个说法其实并不准确，事实是fragment 在 view space
												//空间中的位置，但直观的感受是如括号之前所述一般）
		v2f vert(a2v v)
	{												
		v2f f;
		f.position = mul(UNITY_MATRIX_MVP, v.myVertex);
		f.temp = v.myNormal;								
		return f;										//mul:内置函数，用于完成矩阵和Position的乘法运算
	}
		float4 frag(v2f f) :SV_Target						//使用SV_TARGET声明描述，以表示将用于目标的呈现格式，表示返回值的类型也是float4
	{

		return float4(f.temp,1);
	}
		ENDCG
}
	}
		Fallback"VertexLit"
}
