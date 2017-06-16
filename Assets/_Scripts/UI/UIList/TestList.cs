using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestList : LoopList {

	// Use this for initialization
	void Start () {
        getData();
        showData();
    }

    public override void getData()
    {
        for (int i = 0; i < 20; i++)
        {
            preList.Add(i);
        }
    }

    protected override void updatePre(Transform pre, int num)
    {
        int n = (int)preList[num];
        pre.GetChild(0).GetComponent<Text>().text = "测试单元" + n;
    }
}
