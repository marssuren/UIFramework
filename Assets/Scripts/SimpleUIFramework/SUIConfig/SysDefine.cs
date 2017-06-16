using UnityEngine;
using System.Collections;

public class SysDefine : MonoBehaviour
{
	public const string CanvasPath = "Prefabs/UI/Canvas";
	public const string UIFormsPath = "UIFormsConfigInfo";
}
public enum UIFormType                  //UI窗体的位置类型
{
	FullScreen,         //全屏窗体
	Fixed,          //固定窗体
	PopUp           //弹出窗体
}
public enum UIFormShowMode              //UI窗体的显示类型
{
	Normal,         //普通
	ReverseChange,  //反向切换(本窗体显示时，冻结父窗体)
	HideOther       //隐藏其他

}
public enum UIFormTransparency          //UI窗体的透明度类型
{
	Lucency,        //完全透明，不能穿透
	Translucency,   //半透明，不能穿透
	Impenetrable,   //低透明度，不能穿透
	Penetrate       //可以穿透
}
