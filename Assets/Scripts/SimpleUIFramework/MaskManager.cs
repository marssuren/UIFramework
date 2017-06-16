using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MaskManager : MonoBehaviour
{
	private static MaskManager _m_pIns;
	public static MaskManager m_pIns
	{
		get
		{
			if(null == _m_pIns)
			{
				//_m_pIns = FindObjectOfType<MaskManager>();
				_m_pIns = new GameObject("UIMaskManager").AddComponent<MaskManager>();
			}
			return _m_pIns;
		}
	}

	private GameObject rootCanvasGo;                //UI根节点对象
	private Transform ScriptNode;                   //UI脚本节点对象
	private GameObject goTopPanel;                  //顶层面板
	private GameObject maskPanel;                   //遮罩面板
	private Camera uiCamera;                        //UI摄像机
	private float uiCameraOriDepth;                 //UI摄像机初始层深
	private GameObject displayUIPanel;              //当前显示的UIPanel

	void Awake()
	{

	}
	private void init()
	{
		//rootCanvasGo=GameObject.FindGameObjectWithTag(SysDefine.CanvasPath);
		//ScriptNode = UnityHelper.FindChildNode(rootCanvasGo, );
		UnityHelper.AddParentNode(ScriptNode.gameObject, gameObject);           //实例化脚本作为“脚本节点对象”的子节点
		goTopPanel = rootCanvasGo;                                              //得到顶层面板
		maskPanel = UnityHelper.FindChildNode(goTopPanel, "UIMaskManager");     //得到遮罩面板
		uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();     //得到UI摄像机
		uiCameraOriDepth = uiCamera.depth;                                      //得到UI摄像机的原始深度

	}
	public void SetMaskStatus(GameObject _uiForm, UIFormTransparency _transType)                        //设置遮罩状态
	{
		goTopPanel.transform.SetAsLastSibling();        //顶层窗体下移
		switch(_transType)                              //启用遮罩窗体以及设置透明度
		{
			case UIFormTransparency.Lucency:            //完全透明，不穿透
			maskPanel.SetActive(true);
			maskPanel.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
			break;
			case UIFormTransparency.Impenetrable:       //低透明度，不能穿透
			maskPanel.SetActive(true);
			maskPanel.GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 50 / 255f);

			break;
			case UIFormTransparency.Penetrate:          //可以穿透

			if(maskPanel.activeInHierarchy)
			{
				maskPanel.SetActive(false);
			}
			break;
			case UIFormTransparency.Translucency:       //半透明，不穿透
			break;
		}
		maskPanel.transform.SetAsLastSibling();         //遮罩窗体下移
		uiCamera.depth += 100;                          //增加当前UI摄像机的层深(保证当前摄像机为最前显示)
	}

	public void CancelMaskStatus()                     //取消遮罩状态
	{
		goTopPanel.transform.SetAsFirstSibling();       //顶层窗体上移
		if(maskPanel.activeInHierarchy)                 //隐藏遮罩窗体
		{
			maskPanel.SetActive(false);
		}
		uiCamera.depth = uiCameraOriDepth;               //恢复UI摄像机原始深度
	}


}
