using UnityEngine;
using System.Collections;

public class MyTestScr : MonoBehaviour
{
	void Start()
	{
		loadAsset();
	}
	private void loadAsset()            //解析指定的Json文件
	{
		TextAsset tAsset = Resources.Load<TextAsset>("Hero");
		//Debug.LogError(tAsset.text);
		HeroInfo tHeroInfo = JsonUtility.FromJson<HeroInfo>(tAsset.text);
		for(int i = 0; i < tHeroInfo.HeroLst.Count; i++)
		{
			Debug.LogError("英雄名：" + tHeroInfo.HeroLst[i].Name + "英雄年龄：" + tHeroInfo.HeroLst[i].Age);
		}
	}
}
