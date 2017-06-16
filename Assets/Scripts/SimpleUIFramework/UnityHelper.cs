using UnityEngine;
using System.Collections;

public class UnityHelper : MonoBehaviour        //提供一些常用的功能方法，方便快速开发
{
	public static GameObject FindChildNode(GameObject _parentGo, string _childName)      //查找子节点
	{
		Transform tResultTrans = _parentGo.transform.Find(_childName);
		if(null == tResultTrans)
		{
			//for (int i = 0; i < _parentGo.transform.childCount; i++)
			//{

			//}
			foreach(Transform trans in _parentGo.transform)
			{
				FindChildNode(trans.gameObject, _childName);
			}
		}
		if(null != tResultTrans)
		{
			return tResultTrans.gameObject;
		}
		return null;
	}

	public static T GetChildNodeComponentScript<T>(GameObject _parentGo, string _childName) where T : Component//查找子节点脚本
	{
		return (FindChildNode(_parentGo, _childName) == null ? null : FindChildNode(_parentGo, _childName).GetComponent<T>());
	}
	public static T AddChildNodeComponent<T>(GameObject _parentGo, string _childName) where T : Component           //给节点添加脚本
	{
		if(FindChildNode(_parentGo, _childName) == null)
		{
			return null;
		}
		return GetChildNodeComponentScript<T>(_parentGo, _childName) == null
			? FindChildNode(_parentGo, _childName).AddComponent<T>()
			: FindChildNode(_parentGo, _childName).GetComponent<T>();
	}
	public static void AddParentNode(GameObject _parentGo, GameObject _childGo)     //给自物体添加父物体 默认为局部坐标false
	{
		_childGo.transform.SetParent(_parentGo.transform, false);
		_childGo.transform.localPosition = new Vector3(0, 0, 0);
		_childGo.transform.localScale = new Vector3(1, 1, 1);
		_childGo.transform.localEulerAngles = new Vector3(0, 0, 0);
	}






}
