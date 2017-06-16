using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGraphicScr : MonoBehaviour
{
	[SerializeField]
	private GameObject emptyGraphPrefab;

	private WMG_Axis_Graph graph;
	[SerializeField]
	private List<Vector2> series1Data;      //Y轴数据Lst

	[SerializeField]
	private List<Vector2> series1Data1;

	[SerializeField]
	private List<string> contentSeriesLst;  //X轴数据

	private List<float> contentYSeriesLst;  //Y轴数据
	private WMG_Series series1;
	//private WMG_Series series2;
	private bool isUseData = false;
	void Start()
	{
		setYAxisContentDatas();
		List<Vector2> tdatas = new List<Vector2>();
		GameObject tgo = Instantiate(emptyGraphPrefab);
		tgo.transform.SetParent(gameObject.transform, false);
		graph = tgo.GetComponent<WMG_Axis_Graph>();
		setXAxisContentDatas(graph);
		series1 = graph.addSeries();
		graph.xAxis.AxisMaxValue = 8;
		//graph.yAxis.AxisNumTicks
		if(!isUseData)
		{
			List<string> tLst = new List<string>();
			for(int i = 0; i < contentSeriesLst.Count; i++)
			{
				string[] row = contentSeriesLst[i].Split(',');
				//Debug.LogError(row[0]);
				//tgroups.Add(row[0]);
				if(!string.IsNullOrEmpty(row[1]))
				{
					float y = float.Parse(row[1]);
					tdatas.Add(new Vector2(i + 1, contentYSeriesLst[i]));
					tLst.Add(row[0]);
				}
			}
			graph.groups.SetList(tLst);

			series1.seriesName = "Profit Data";

			series1.UseXDistBetweenToSpace = true;
			series1.AutoUpdateXDistBetween = true;
			series1.pointValues.SetList(tdatas);
		}
		else
		{
			series1.pointValues.SetList(series1Data);
		}
		//series2 = graph.addSeries();
		//series2.pointValues.SetList(series1Data1);
	}
	private void setXAxisContentDatas(WMG_Axis_Graph _graph)        //设置X轴数据
	{
		contentSeriesLst.Clear();
		for(int i = 9; i >= 0; i--)
		{
			string tDateTime = DateTime.Now.AddDays(-i).ToString("dd");
			contentSeriesLst.Add(tDateTime + "日" + "," + i);
			Debug.LogError(tDateTime);
		}
		_graph.groups.SetList(contentSeriesLst);
		_graph.useGroups = true;
		_graph.xAxis.LabelType = WMG_Axis.labelTypes.groups;
		_graph.xAxis.AxisNumTicks = contentSeriesLst.Count;
	}
	private void setYAxisContentDatas(List<string> _profitList = null)     //设置Y轴数据
	{
		_profitList = new List<string>();
		_profitList.Add("10");
		_profitList.Add("20");
		_profitList.Add("30");
		_profitList.Add("40");
		_profitList.Add("30");
		_profitList.Add("20");
		_profitList.Add("10");
		_profitList.Add("10");
		_profitList.Add("10");
		_profitList.Add("10");

		contentYSeriesLst = new List<float>();
		for(int i = 0; i < _profitList.Count; i++)
		{
			contentYSeriesLst.Add(float.Parse(_profitList[i]));
		}
	}


}
