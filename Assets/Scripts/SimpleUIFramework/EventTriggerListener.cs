using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger           //事件触发监听类
{
	public delegate void VoidDelegate(GameObject _go);

	public VoidDelegate onClick;
	public VoidDelegate onDown;
	public VoidDelegate onEnter;
	public VoidDelegate onExit;
	public VoidDelegate onUp;
	public VoidDelegate onSelect;
	public VoidDelegate onUpdateSelect;

	public static EventTriggerListener GetListener(GameObject _go)                  //用于实现对任何对象的监听处理
	{
		EventTriggerListener tListener = _go.GetComponent<EventTriggerListener>();
		if(null == tListener)
		{
			tListener = _go.AddComponent<EventTriggerListener>();
		}
		return tListener;
	}
	public override void OnPointerClick(PointerEventData eventData)
	{
		if(null != onClick)
		{
			onClick(gameObject);
		}
	}
}
