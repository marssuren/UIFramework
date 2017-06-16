using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DetailPanel : BaseUIForm
{
	[SerializeField]
	private Text textName;




	void Awake()
	{
		init();
	}
	private void init()
	{
		MyUIType.MyUIFormType = UIFormType.PopUp;
		MyUIType.MyUIFormShowMode = UIFormShowMode.ReverseChange;
		MyUIType.MyUIFormTransparency = UIFormTransparency.Translucency;

		MessageCenter.m_p_Ins.AddListener("pros", p =>
		{
			if(p.Key.Equals("scepter"))
			{
				textName.text = p.ObjectValue.ToString();
			}
		});
	}
}
