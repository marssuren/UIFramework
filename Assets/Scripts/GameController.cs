using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	void Start()
	{
		UIManager.m_pIns.ShowUIForm("LogInPanel");     //加载登录窗体
	}
}
