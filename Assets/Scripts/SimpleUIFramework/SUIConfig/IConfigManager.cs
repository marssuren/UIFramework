using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IConfigManagable//通用配置管理器，基于键值对的配置文件的统一解析
{
	Dictionary<string, string> AppSettingDic        //应用设置，用于得到键值对集合的数据
	{
		get;
	}
	int GetAPPSettingMaxNum();          //得到配置文件的最大数量
}
[Serializable]
public class KeyValueInfo
{
	public List<KeyValueNode> ConfigInfoLst;        //配置信息
}
[Serializable]
public class KeyValueNode
{
	public string Key = null;                           //键
	public string Value = null;                         //值

}

