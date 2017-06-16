using UnityEngine;
using System.Collections;

public class MainCityPanelController : BaseUIForm
{


	void Awake()
	{
		init();
	}
	private void init()
	{
		MyUIType.MyUIFormShowMode = UIFormShowMode.Normal;
		MyUIType.MyUIFormType = UIFormType.FullScreen;
		MyUIType.MyUIFormTransparency = UIFormTransparency.Lucency;

		//showHeroInfoForm();

		RegisterBtnEvent("BtnReturn", onBtnReturnClick);
	}
	private void onBtnReturnClick(GameObject _go)
	{
		UIManager.m_pIns.CloseUIForm(PanelName.MainCityPanel.ToString());
	}
	private void showHeroInfoForm()                                             //显示英雄信息面板
	{
		UIManager.m_pIns.ShowUIForm(PanelName.HeroInfoForm.ToString());
	}
}

