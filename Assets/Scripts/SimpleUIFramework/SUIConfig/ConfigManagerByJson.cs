using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ConfigManagerByJson : IConfigManagable
{
	public ConfigManagerByJson(string _jsonPath)                //得到json路径并解析
	{
		appSettingDic = new Dictionary<string, string>();          //初始化解析Json数据，加载到集合中
		initAndAnalyseJson(_jsonPath);
	}
	private Dictionary<string, string> appSettingDic;              //保存键值对应用设置集合

	public Dictionary<string, string> AppSettingDic
	{
		get
		{
			return appSettingDic;
		}
	}
	public int GetAPPSettingMaxNum()                                //获取APPSetting元素的数量
	{
		return AppSettingDic.Count >= 1 ? AppSettingDic.Count : 0;
	}
	private void initAndAnalyseJson(string _jsonPath)               //初始化和解析json数据，加载进集合
	{
		TextAsset tAsset = Resources.Load<TextAsset>(_jsonPath);
		KeyValueInfo tInfo = JsonUtility.FromJson<KeyValueInfo>(tAsset.text);
		foreach(KeyValueNode item in tInfo.ConfigInfoLst)
		{
			AppSettingDic.Add(item.Key, item.Value);
		}
	}
}
