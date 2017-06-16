using UnityEngine;
using System.Collections;

public class UIType
{
	public UIFormType MyUIFormType = UIFormType.FullScreen;                     //默认窗体位置类型为普通类型
	public UIFormShowMode MyUIFormShowMode = UIFormShowMode.Normal;         //默认窗体显示类型为普通
	public UIFormTransparency MyUIFormTransparency = UIFormTransparency.Lucency;        //默认窗体透明度为全透明

	public bool IsClearStack = false;                           //是否在显示后要清空栈的其他数据
}
