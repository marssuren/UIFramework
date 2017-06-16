using System;
using UnityEngine;
using System.Collections;

public class JsonAnalyseException : Exception           //捕获Json解析错误
{
	public JsonAnalyseException() : base()
	{

	}
	public JsonAnalyseException(string _exceptionMessage) : base(_exceptionMessage)
	{

	}
}
