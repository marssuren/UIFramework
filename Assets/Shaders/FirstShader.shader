Shader "MyShaders/FirstShader"				//指定Shader的名字
{
	Properties		//属性
	{
		_Color("myColor",Color) = (1,1,1,1)				//""中为在暴露在面板中的名字
		_Vector("myVector",Vector)=(1,2,3,4)			//Vector 四维向量
		_Int("myInt",Int)=32454
		_Float("myFloat",Float)=3.2
		_Range("myRange",Range(0,500))=5
		_2D("myTexture",2D)="white"{}				//此处的white用于不指定任何图片时显示的颜色
		_Cube("myCube",Cube) = "white"{}				//立方体贴图
		_3D("Texture",3D) = "black"
	}
		SubShader		//SubShader是不同的分支，为了在不同的显卡上实现不同的效果，显卡会从上至下寻找，以第一个能实现的SubShader为基准
		{
			Pass
			{
				CGPROGRAM
				float4 _Color;		//在使用属性前需要重新定义，但不需要重新赋值	定义属性不需要分号，但是CG代码需要
				float4 _Vector;
				float _int;
				float _Float;
				float _Range;
				sampler2D _2D;
				samplerCube _Cube;
				sampler3D _3D;
				//float		32位	来存储
				//half		16		-6万到+6万
				//fixed		11位	-2到+2
				ENDCG		
			}
	}
		Fallback"Diffuse"			//指定一个已经存在的Shader作为所有SubShader都不支持的最后方案
}
