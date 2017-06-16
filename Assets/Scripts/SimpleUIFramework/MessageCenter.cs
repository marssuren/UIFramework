using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageCenter : MonoBehaviour                      //消息传递中心，用于处理所有UI窗体之间的数据传值
{
	private static MessageCenter _m_pIns;
	public static MessageCenter m_p_Ins
	{
		get
		{
			if(null == _m_pIns)
			{
				_m_pIns = FindObjectOfType<MessageCenter>();
			}
			return _m_pIns;
		}
	}
	public delegate void DelMessageDelivery(KeyValuesUpdate _kv);
	public Dictionary<string, DelMessageDelivery> MessageDic = new Dictionary<string, DelMessageDelivery>();  //同名委托里面可以增加多个内容
	public void AddListener(string _messageName, DelMessageDelivery _del)                   //增加消息监听
	{
		if(!MessageDic.ContainsKey(_messageName))
		{
			MessageDic.Add(_messageName, null);
		}
		MessageDic[_messageName] += _del;
	}
	public void RemoveListener(string _messageName, DelMessageDelivery _del)                    //移除消息监听
	{
		if(MessageDic.ContainsKey(_messageName))
		{
			MessageDic[_messageName] -= _del;
			if(MessageDic[_messageName] == null)
			{
				MessageDic.Remove(_messageName);
			}
		}
	}
	public void RemoveAllMessageListener()          //取消所有指定消息的监听
	{
		if(null != MessageDic)
		{
			MessageDic.Clear();
		}
	}
	public void SendMessage(string _messageName, KeyValuesUpdate _kv)                   //发送消息(调用委托)
	{
		DelMessageDelivery tDel;
		if(MessageDic.TryGetValue(_messageName, out tDel))
		{
			if(null != tDel)
			{
				tDel(_kv);
			}
		}
	}
}
public class KeyValuesUpdate
{
	private string key;
	private object objectValue;
	public string Key
	{
		get
		{
			return key;
		}
	}
	public object ObjectValue
	{
		get
		{
			return objectValue;
		}
	}
	public KeyValuesUpdate(string _key, object _obj)
	{
		key = _key;
		objectValue = _obj;
	}
}
