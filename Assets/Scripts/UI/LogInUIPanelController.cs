using System;
using UnityEngine;
using System.Collections;

public class LogInUIPanelController : BaseUIForm
{
	void Awake()
	{
		init();
	}

	void Start()
	{

	}


	void Update()
	{

	}
	private void init()                 //初始化UI窗体状态
	{
		MyUIType.MyUIFormType = UIFormType.FullScreen;
		MyUIType.MyUIFormShowMode = UIFormShowMode.Normal;
		MyUIType.MyUIFormTransparency = UIFormTransparency.Lucency;
		//Transform tLogInPanelTransform = GameObject.FindGameObjectWithTag("LogInPanel").transform;
		//GameObject tBtnLogIn = tLogInPanelTransform.FindChild("BtnLogIn").gameObject;
		RegisterBtnEvent("BtnLogIn", VerifyInputInfo);
	}
	public void VerifyInputInfo(GameObject _go)           //用于校验用户输入的账户和密码
	{
		OpenUIForm(PanelName.MainCityPanel.ToString());
	}
}
