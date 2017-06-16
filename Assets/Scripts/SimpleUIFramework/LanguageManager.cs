using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LanguageManager
{
	public static LanguageManager Instance;

	public Dictionary<string ,string > LanguageDic;
	private LanguageManager ()
	{
		LanguageDic = new Dictionary<string, string> ();
	}

	public static LanguageManager GetInstance()				//获取实例
	{
		if (null==Instance)
		{
			Instance = new LanguageManager ();
		}
		return Instance;
	}
	private void init()
	{
		//IConfigable tConfig = new ConfigByJson ();
	}
} 