using UnityEngine;
using System.Collections;

public class BaseUIForm : MonoBehaviour             //所有UI窗体的父类，有四个生命周期
{
	private UIType myUIType;

	public UIType MyUIType                          //当前UI窗体的属性数据
	{
		get
		{
			if(null == myUIType)
			{
				MyUIType = new UIType();
			}
			return myUIType;
		}
		set
		{
			myUIType = value;
		}
	}

	//1.Display			显示状态
	//2.Hiding			隐藏状态
	//3.ReDisplay		再显示状态
	//4.Freeze			冻结状态
	public virtual void Display()                   //显示状态
	{
		gameObject.SetActive(true);
		if(MyUIType.MyUIFormType == UIFormType.PopUp)
		{
			MaskManager.m_pIns.SetMaskStatus(gameObject, MyUIType.MyUIFormTransparency);
		}
	}
	public virtual void Hiding()                    //隐藏状态
	{
		gameObject.SetActive(false);
		if(MyUIType.MyUIFormType == UIFormType.PopUp)
		{
			MaskManager.m_pIns.CancelMaskStatus();
		}
	}
	public virtual void ReDisplay()                 //再显示状态
	{
		gameObject.SetActive(true);
	}
	public virtual void Freeze()                    //冻结状态
	{
		gameObject.SetActive(true);
	}
	protected void RegisterBtnEvent(string _btnName, EventTriggerListener.VoidDelegate _delHandler)                //注册按钮事件
	{
		GameObject tBtnLogIn = UnityHelper.FindChildNode(gameObject, _btnName);
		EventTriggerListener.GetListener(tBtnLogIn).onClick += _delHandler;
	}
	protected void OpenUIForm(string _uiName)
	{
		UIManager.m_pIns.ShowUIForm(_uiName);
	}
	protected void SendMessage(string _messageType, string _messageName, object _msgContent)                //请求执行回调
	{
		KeyValuesUpdate tkv = new KeyValuesUpdate(_messageName, _msgContent);
		MessageCenter.m_p_Ins.SendMessage(_messageType, tkv);
	}
	protected void ReceiveMessage(string _messageType, MessageCenter.DelMessageDelivery _del)               //接受消息
	{
		MessageCenter.m_p_Ins.AddListener(_messageType, _del);
	}

}
