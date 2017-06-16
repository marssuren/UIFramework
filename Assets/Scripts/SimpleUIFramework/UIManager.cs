using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UIManager : MonoBehaviour              //整个UI框架的核心
{
	private static UIManager _m_pIns;
	public static UIManager m_pIns                  //单例
	{
		get
		{
			if(null == _m_pIns)
			{
				_m_pIns = new GameObject("UIManager").AddComponent<UIManager>();

			}
			return _m_pIns;
		}
	}

	private Dictionary<string, string> uiFormsPathDic;              //UI窗体预设路径
	private Dictionary<string, BaseUIForm> loadedUIFormsDic;       //缓存已加载的所有UI窗体
	private Dictionary<string, BaseUIForm> currentUIFormsDic;      //缓存当前显示的UI窗体
	private Stack<BaseUIForm> currentShowUIStack;               //缓存需要使用栈来实现反向切换效果的UI窗体
	private Transform canvasTransform = null;                   //canvas的transform
	private Transform normalTransform = null;                   //普通窗口根节点的transform
	private Transform fixedTransform = null;                    //固定窗口根节点的transform
	private Transform popUpTransform = null;                    //弹出窗口根节点的transform
	private Transform uiScriptsTransform = null;                //用于存放UI脚本的节点的transfrom

	void Awake()
	{
		init();
		initRootCanvas();
	}
	private void init()                                 //初始化部分字段
	{
		loadedUIFormsDic = new Dictionary<string, BaseUIForm>();
		currentUIFormsDic = new Dictionary<string, BaseUIForm>();
		uiFormsPathDic = new Dictionary<string, string>();
		currentShowUIStack = new Stack<BaseUIForm>();
	}
	private void initRootCanvas()                       //初始化Canvas和各个根节点的transform
	{
		//GameObject tgo = Resources.Load<Canvas>("Prefabs/UI/Canvas").gameObject;
		//tgo.transform.SetParent(transform.parent);
		//Debug.LogError(null == tgo);
		Canvas tCanvas = Resources.Load<Canvas>(SysDefine.CanvasPath);
		canvasTransform = Instantiate(tCanvas).transform;
		//Debug.LogError(canvasTransform.localPosition);
		//normalTransform = canvasTransform.Find("Normal");
		//fixedTransform = canvasTransform.Find("Fixed");
		//popUpTransform = canvasTransform.Find("PopUp");
		//uiScriptsTransform = canvasTransform.Find("ScriptsManager");
		normalTransform = UnityHelper.FindChildNode(canvasTransform.gameObject, "Normal").transform;
		fixedTransform = UnityHelper.FindChildNode(canvasTransform.gameObject, "Fixed").transform;
		popUpTransform = UnityHelper.FindChildNode(canvasTransform.gameObject, "PopUp").transform;
		uiScriptsTransform = UnityHelper.FindChildNode(canvasTransform.gameObject, "ScriptsManager").transform;
		transform.SetParent(uiScriptsTransform);
		DontDestroyOnLoad(canvasTransform);
		//DontDestroyOnLoad(canvasTransform);
		//if(null != uiFormsPathDic)
		//{
		//	uiFormsPathDic.Add("LogInPanel", "Prefabs/UI/UIPanel/LogInPanel");
		//	uiFormsPathDic.Add("MainCityPanel", "Prefabs/UI/UIPanel/MainCityPanel");
		//}
		initUIFormsData();
	}
	public void ShowUIForm(string _uiName)         //打开UI窗体
	{
		BaseUIForm tBaseUiForm = loadUIForm(_uiName);
		if(tBaseUiForm.MyUIType.IsClearStack)
		{
			clearStack();
		}
		tBaseUiForm.gameObject.SetActive(false);
		RectTransform tRectTrans = tBaseUiForm.gameObject.GetComponent<RectTransform>();
		//Debug.LogError("最小偏移" + tRectTrans.offsetMin + "最大偏移" + tRectTrans.offsetMax);
		//Debug.LogError(tBaseUiForm.transform.localPosition + "！" + tBaseUiForm.transform.localScale);
		switch(tBaseUiForm.MyUIType.MyUIFormType)
		{
			case UIFormType.FullScreen:                 //普通窗体节点
			tBaseUiForm.gameObject.transform.SetParent(normalTransform);
			break;
			case UIFormType.Fixed:                  //固定窗体节点
			tBaseUiForm.gameObject.transform.SetParent(fixedTransform);
			break;
			case UIFormType.PopUp:                  //弹出窗体节点
			tBaseUiForm.gameObject.transform.SetParent(popUpTransform);
			break;
		}
		switch(tBaseUiForm.MyUIType.MyUIFormShowMode)
		{
			case UIFormShowMode.Normal:             //普通显示窗口
			tBaseUiForm.gameObject.SetActive(true);
			currentUIFormsDic.Add(_uiName, tBaseUiForm);
			break;
			case UIFormShowMode.ReverseChange:          //需要反向切换的窗口
			pushUIForm2Stack(_uiName);
			break;
			case UIFormShowMode.HideOther:      //显示需要隐藏其他窗口的窗口
			EnterUIFormAndHideOther(_uiName);
			break;
			default:
			break;
		}
		//Debug.LogError("最小偏移" + tRectTrans.offsetMin + "最大偏移" + tRectTrans.offsetMax);
		adjustRect(tRectTrans);

		//Debug.LogError(tBaseUiForm.transform.localPosition + "！" + tBaseUiForm.transform.localScale);
		//loadedUIFormsDic.Add(_uiName, tBaseUiForm);     //缓存已经的窗体
		Debug.LogError("已加载的所有UI窗体的数量是：" + loadedUIFormsDic.Count);
		Debug.LogError("已显示的所有UI窗体的数量是：" + currentUIFormsDic.Count);
		Debug.LogError("堆栈UI窗体的数量是：" + currentShowUIStack.Count);
	}
	public void CloseUIForm(string _uiName)             //用于关闭(返回上一个)UI窗体
	{
		BaseUIForm tBaseUIForm = null;
		loadedUIFormsDic.TryGetValue(_uiName, out tBaseUIForm);
		if(null == tBaseUIForm)
		{
			return;
		}
		switch(tBaseUIForm.MyUIType.MyUIFormShowMode)
		{
			case UIFormShowMode.Normal:                 //普通显示窗口直接关闭
			exitNormalForm(_uiName);
			break;
			case UIFormShowMode.ReverseChange:          //处理反向切换窗口
			exitReverseChangeUIForm(_uiName);
			break;
			case UIFormShowMode.HideOther:              //处理被隐藏的其他窗口
			exitUIFormAndDisplayOther(_uiName);
			break;
			default:
			break;
		}
	}
	private BaseUIForm loadUIForm(string _uiName)
	{
		if(string.IsNullOrEmpty(_uiName))
		{
			Debug.LogError("输入的UIName错误");
		}
		BaseUIForm tBaseUiForm;
		loadedUIFormsDic.TryGetValue(_uiName, out tBaseUiForm);     //尝试在已经加载的UIDictionary中获取UI窗体
		if(null == tBaseUiForm)
		{
			tBaseUiForm = loadUIFormPrefab(_uiName);
		}
		return tBaseUiForm;
	}
	private BaseUIForm loadUIFormPrefab(string _uiName)             //用于实例化没有加载出的UI窗体
	{
		string tUIFormPath = null;
		GameObject tUIPrefab = null;
		BaseUIForm tBaseUIForm = null;
		uiFormsPathDic.TryGetValue(_uiName, out tUIFormPath);
		if(!string.IsNullOrEmpty(tUIFormPath))
		{
			tUIPrefab = Instantiate(Resources.Load<GameObject>(tUIFormPath));
		}
		tBaseUIForm = tUIPrefab.GetComponent<BaseUIForm>();
		//Debug.LogError(null == tBaseUIForm);
		if(!loadedUIFormsDic.ContainsKey(_uiName))
		{
			loadedUIFormsDic.Add(_uiName, tBaseUIForm);                 //缓存已经加载的UI窗体 
		}

		return tBaseUIForm;
	}
	private void exitNormalForm(string _uiName)                                 //用于关闭普通的UIForm
	{
		BaseUIForm tBaseUIForm;
		currentUIFormsDic.TryGetValue(_uiName, out tBaseUIForm);                //尝试从已经加载的窗体中返回
		if(null == tBaseUIForm)
		{
			return;
		}
		tBaseUIForm.Hiding();
		currentUIFormsDic.Remove(_uiName);
	}
	private void exitReverseChangeUIForm(string _uiName)                            //用于退出当前的能够反向切换的窗体
	{
		if(currentShowUIStack.Count >= 2)                                           //如果栈内有1个以上的元素
		{
			currentShowUIStack.Pop().Hiding();                                      //先出栈并隐藏
			currentShowUIStack.Peek().ReDisplay();                                  //再恢复上一个被冻结的窗体
		}
		else if(currentShowUIStack.Count == 1)                                      //只有一个元素则仅出栈隐藏
		{
			currentShowUIStack.Pop().Hiding();
		}
		currentUIFormsDic.Remove(_uiName);                                          //从已显示的窗体中移除要被隐藏的窗体
	}
	private void pushUIForm2Stack(string _uiName)           //用于显示能够反向切换的UI窗体
	{
		BaseUIForm tForm;
		if(currentShowUIStack.Count > 0)                   //冻结栈内的其他窗体
		{
			currentShowUIStack.Peek().Freeze();
		}
		loadedUIFormsDic.TryGetValue(_uiName, out tForm);           //尝试从已经加载的UI窗体缓存中呼出指定的窗体并入栈
		if(tForm != null)
		{
			tForm.Display();
			currentShowUIStack.Push(tForm);
		}
		else
		{
			Debug.LogError("TryGetValue From LoadedUIFormsDic Fail!");
		}
	}
	private void EnterUIFormAndHideOther(string _uiName)            //打开指定的窗体并隐藏其他窗体
	{
		BaseUIForm tBaseUIForm;                                     //UI窗体基类
		BaseUIForm tBaseUIFormFromDic;
		loadedUIFormsDic.TryGetValue(_uiName, out tBaseUIForm);
		if(null == tBaseUIForm)
		{
			return;
		}
		foreach(var baseUiForm in currentUIFormsDic)
		{
			baseUiForm.Value.Hiding();
		}
		foreach(var item in currentShowUIStack)
		{
			item.Hiding();
		}
		tBaseUIForm.Display();
		if(!currentUIFormsDic.ContainsKey(_uiName))
		{
			currentUIFormsDic.Add(_uiName, tBaseUIForm);
		}
	}
	private void exitUIFormAndDisplayOther(string _uiName)            //关闭指定的窗体并显示其他窗体
	{
		BaseUIForm tBaseUIForm;                                     //UI窗体基类
		BaseUIForm tBaseUIFormFromDic;
		loadedUIFormsDic.TryGetValue(_uiName, out tBaseUIForm);
		if(null == tBaseUIForm)
		{
			return;
		}
		tBaseUIForm.Hiding();                                       //隐藏当前窗体
		currentUIFormsDic.Remove(_uiName);
		foreach(var item in currentUIFormsDic)
		{
			item.Value.ReDisplay();
		}
		foreach(var item in currentShowUIStack)
		{
			item.ReDisplay();
		}
	}
	public int ShowAllUIFormsCount()                                //返回显示所有UIForm的数量
	{
		return loadedUIFormsDic.Count;
	}
	public int ShowCurrentUIFormsCount()                                //返回所有已显示的UIForm的数量
	{
		return currentUIFormsDic.Count;
	}
	public int ShowCurrentStackUIFormsCount()                       //返回当前栈中的UIForm数量
	{
		return currentShowUIStack.Count;
	}
	private void adjustRect(RectTransform _rect)                    //用于实例化窗口出来后调整rect
	{
		//_rect.anchoredPosition = new Vector2(0.5f, 0.5f);
		//_rect.anchorMin = new Vector2(0, 0);
		//_rect.anchorMax = new Vector2(1, 1);
		_rect.transform.localScale = new Vector3(1, 1, 1);
		_rect.offsetMax = new Vector2(0, 0);
		_rect.offsetMin = new Vector2(0f, 0f);
	}
	private void clearStack()                                       //清空栈
	{
		if(currentShowUIStack.Count >= 1)
		{
			currentShowUIStack.Clear();
		}
	}
	private void initUIFormsData()                                  //初始化“UI窗体预设”路径数据
	{
		IConfigManagable tManagable = new ConfigManagerByJson("UIFormsConfigInfo");
		uiFormsPathDic = tManagable.AppSettingDic;
	}
}
