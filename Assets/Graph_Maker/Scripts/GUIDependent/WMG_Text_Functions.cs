using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WMG_Text_Functions : MonoBehaviour
{

	public enum WMGpivotTypes { Bottom, BottomLeft, BottomRight, Center, Left, Right, Top, TopLeft, TopRight };

	public void changeLabelText(GameObject _obj, string _Text)       //用于更改Label内容
	{
		Text theLabel = _obj.GetComponent<Text>();
		theLabel.text = _Text;
	}

	public void changeLabelFontSize(GameObject _obj, int _newFontSize)        //用于改变Label字体大小
	{
		Text theLabel = _obj.GetComponent<Text>();
		theLabel.fontSize = _newFontSize;
	}

	public Vector2 getTextSize(GameObject _obj)          //用于得到字体大小
	{
		Text text = _obj.GetComponent<Text>();
		return new Vector2(text.preferredWidth, text.preferredHeight);
	}

	public void changeSpritePivot(GameObject _obj, WMGpivotTypes _theType)        //用于改变图片锚点
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		Text theText = _obj.GetComponent<Text>();
		if(theSprite == null)
			return;
		if(_theType == WMGpivotTypes.Bottom)
		{
			theSprite.pivot = new Vector2(0.5f, 0f);
			if(theText != null)
				theText.alignment = TextAnchor.LowerCenter;
		}
		else if(_theType == WMGpivotTypes.BottomLeft)
		{
			theSprite.pivot = new Vector2(0f, 0f);
			if(theText != null)
				theText.alignment = TextAnchor.LowerLeft;
		}
		else if(_theType == WMGpivotTypes.BottomRight)
		{
			theSprite.pivot = new Vector2(1f, 0f);
			if(theText != null)
				theText.alignment = TextAnchor.LowerRight;
		}
		else if(_theType == WMGpivotTypes.Center)
		{
			theSprite.pivot = new Vector2(0.5f, 0.5f);
			if(theText != null)
				theText.alignment = TextAnchor.MiddleCenter;
		}
		else if(_theType == WMGpivotTypes.Left)
		{
			theSprite.pivot = new Vector2(0f, 0.5f);
			if(theText != null)
				theText.alignment = TextAnchor.MiddleLeft;
		}
		else if(_theType == WMGpivotTypes.Right)
		{
			theSprite.pivot = new Vector2(1f, 0.5f);
			if(theText != null)
				theText.alignment = TextAnchor.MiddleRight;
		}
		else if(_theType == WMGpivotTypes.Top)
		{
			theSprite.pivot = new Vector2(0.5f, 1f);
			if(theText != null)
				theText.alignment = TextAnchor.UpperCenter;
		}
		else if(_theType == WMGpivotTypes.TopLeft)
		{
			theSprite.pivot = new Vector2(0f, 1f);
			if(theText != null)
				theText.alignment = TextAnchor.UpperLeft;
		}
		else if(_theType == WMGpivotTypes.TopRight)
		{
			theSprite.pivot = new Vector2(1f, 1f);
			if(theText != null)
				theText.alignment = TextAnchor.UpperRight;
		}
	}

	public void changeLabelColor(GameObject _obj, Color _newColor)        //用于改变文字颜色
	{
		Text theLabel = _obj.GetComponent<Text>();
		theLabel.color = _newColor;
	}

	public void changeLabelFontStyle(GameObject _obj, FontStyle _newFontStyle)        //用于改变文字字体风格
	{
		Text theLabel = _obj.GetComponent<Text>();
		theLabel.fontStyle = _newFontStyle;
	}

	public void changeLabelFont(GameObject _obj, Font _newFont)           //用于改变字体
	{
		Text theLabel = _obj.GetComponent<Text>();
		theLabel.font = _newFont;
	}

}
