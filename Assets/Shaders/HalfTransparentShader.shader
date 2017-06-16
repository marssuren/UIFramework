Shader"MyShaders/HalfTransparentShader"
{
	Properties
	{
		_MainTex("Base(RGB)",2D) = "white"{}
		_TransVal("TransparencyValue",Range(0,1)) = 0.5
	}
		SubShader
		{
			Tags{"RenderType" = "Opaque""Queue" = "Transparent"}
			LOD 200
			CGPROGRAM
			#pragma surface surf Lambert alpha
			sampler2D _MainTex;
			float _TransVal;
			struct Input
			{
				float2 uv_MainTex;
			};
			void surf(Input IN, inout SurfaceOutput o)//void surf(Input in, inout SurfaceOutput o)
			{
			half4 tC = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = tC.rgb;
			o.Alpha = tC.b*_TransVal;
			}
			ENDCG

		}
			FallBack "Diffuse"
}