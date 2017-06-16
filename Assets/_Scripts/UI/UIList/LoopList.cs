using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class LoopList : MonoBehaviour
{

    public int maxNum = 8;

    public Transform scrollViewTrm;
    protected Transform content;
    private ScrollRect scrollRect;
    private Scrollbar scrollbar;
    public GameObject pre;

    protected List<object> preList = new List<object>();
    protected int preListLength;

    private Transform fristChild;
    private float heightTop;
    private float heightBottom;
    private ScollRectUI scollRectUI;
    private int count;

    protected string oldName = "old";

    public bool isUseScrollbar = false;
    private float x;

    protected virtual void Awake()
    {
        initModules();
    }

    private void initModules()
    {
        content = scrollViewTrm.Find("Viewport/Content");
        scrollRect = scrollViewTrm.GetComponent<ScrollRect>();
        scrollbar = scrollRect.verticalScrollbar;
        scollRectUI = content.GetComponent<ScollRectUI>();
        if (isUseScrollbar)
        {
            x = (Screen.width - scrollbar.GetComponent<RectTransform>().rect.width) * 0.5f;
        }
        else
        {
            x = Screen.width * 0.5f;
        }
    }

    //必须重写，获取list数据
    public virtual void getData()
    {

    }
    public virtual void getData(List<object> list)
    {

    }
    private void afterGetDate()
    {
        preListLength = preList.Count;
        scollRectUI.setContentSize(preList.Count);
    }

    //子类调用来实例化pre显示数据
    public virtual void showData()
    {
        afterGetDate();

        if (preList.Count > maxNum)
        {
            for (int i = 0; i < maxNum; i++)
            {
                creatPre(preList[i], i);
            }
        }
        else
        {
            int length = preList.Count;
            for (int i = 0; i < length; i++)
            {
                creatPre(preList[i], i);
            }
        }
        count = maxNum;

        fristChild = content.GetChild(0);

        heightTop = 3 * scollRectUI.getGridSize().y + Screen.height;
        heightBottom = heightTop - scollRectUI.getGridSize().y;
    }

    //实例化pre
    protected virtual void creatPre(object m, int num)
    {
        GameObject obj = Instantiate(pre);
        obj.transform.SetParent(content);
        updatePre(obj.transform, num);
        obj.GetComponent<RectTransform>().sizeDelta = scollRectUI.getGridSize();
        setgridLocalPosition(obj.transform, num);
        obj.SetActive(true);
    }

    //设置pre位置
    private void setgridLocalPosition(Transform trm, int num)
    {
        //trm.localPosition = new Vector3(-content.GetComponent<RectTransform>().sizeDelta.x * 0.5f, -(num + 0.5f) * scollRectUI.getGridSize().y, 0);
        //trm.localPosition = new Vector3(scollRectUI.getGridSize().x*0.5f, -(num + 0.5f) * scollRectUI.getGridSize().y, 0);
        //trm.localPosition = new Vector3(Screen.width * 0.5f, -(num + 0.5f) * scollRectUI.getGridSize().y, 0);
        trm.localPosition = new Vector3(x, -(num + 0.5f) * scollRectUI.getGridSize().y, 0);
    }

    protected virtual void Update()
    {
        updateDate();
    }

    //根据坐标，调整pre位置，刷新数据
    private void updateDate()
    {
        if (fristChild == null)
        {
            return;
        }
        if (count < preListLength)
        {
            if (fristChild.position.y > heightTop)
            {
                fristChild.SetAsLastSibling();
                updatePre(fristChild, count);
                setgridLocalPosition(fristChild, count);

                fristChild = content.GetChild(0);
                count++;
            }
        }
        if (count > maxNum)
        {
            if (fristChild.position.y < heightBottom)
            {
                Transform lastChild = content.GetChild(content.childCount - 1);
                updatePre(lastChild, count - maxNum - 1);
                setgridLocalPosition(lastChild, count - maxNum - 1);

                lastChild.SetAsFirstSibling();
                fristChild = content.GetChild(0);
                count--;
            }
        }
    }

    //子类必须重写，更新实例化的pre上显示的数据
    protected virtual void updatePre(Transform pre, int num)
    {

    }

    public virtual void whenClose()
    {
        if (preListLength == 0) {
            return;
        }
        for (int i = 0; i < maxNum; i++)
        {
            DestroyImmediate(content.GetChild(0).gameObject);
        }
        content.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
}
