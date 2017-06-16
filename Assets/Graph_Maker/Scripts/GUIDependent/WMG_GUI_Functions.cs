using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Contains GUI system dependent functions

public class WMG_GUI_Functions : WMG_Text_Functions
{

	public void SetActive(GameObject _obj, bool _state)       //设置物体激活与否
	{
		_obj.SetActive(_state);
	}

	public bool activeInHierarchy(GameObject _obj)           //设置在层级视图激活
	{
		return _obj.activeInHierarchy;
	}

	public void SetActiveAnchoredSprite(GameObject _obj, bool _state)
	{
		SetActive(_obj, _state);
	}

	public void SetActiveImage(GameObject _obj, bool _state)            //设置图片激活
	{
		_obj.GetComponent<Image>().enabled = _state;
	}

	public Texture2D getTexture(GameObject _obj)        //获得图片
	{
		return (Texture2D)_obj.GetComponent<Image>().mainTexture;
	}

	public void setTexture(GameObject _obj, Sprite _sprite)     //设置纹理
	{
		_obj.GetComponent<Image>().sprite = _sprite;
	}

	public void changeSpriteFill(GameObject _obj, float _fill)      //改变图片填充量
	{
		Image theSprite = _obj.GetComponent<Image>();
		theSprite.fillAmount = _fill;
	}

	public void changeRadialSpriteRotation(GameObject _obj, Vector3 _newRot)        //改变图片环状旋转量
	{
		_obj.transform.localEulerAngles = _newRot;
	}

	public void changeSpriteColor(GameObject _obj, Color _Color)        //改变图片颜色
	{
		Graphic theSprite = _obj.GetComponent<Graphic>();
		theSprite.color = _Color;
	}

	public void changeSpriteWidth(GameObject _obj, int _Width)      //改变图片宽度
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		if(theSprite == null)
			return;
		theSprite.sizeDelta = new Vector2(_Width, theSprite.rect.height);
	}

	public void changeSpriteHeight(GameObject _obj, int _Height)        //改变图片高度
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		if(theSprite == null)
			return;
		theSprite.sizeDelta = new Vector2(theSprite.rect.width, _Height);
	}

	public void setTextureMaterial(GameObject _obj, Material _Mat)      //设置图片纹理
	{
		Image curTex = _obj.GetComponent<Image>();
		curTex.material = new Material(_Mat);
	}

	public Material getTextureMaterial(GameObject _obj)         //获取图片材质
	{
		Image curTex = _obj.GetComponent<Image>();
		if(curTex == null)
			return null;
		return curTex.material;
	}

	public void changeSpriteSize(GameObject _obj, int _Width, int _Height)          //改变图片大小(整形)
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		if(theSprite == null)
			return;
		theSprite.sizeDelta = new Vector2(_Width, _Height);
	}

	public void changeSpriteSizeFloat(GameObject _obj, float _Width, float _Height)		//改变图片大小(浮点)
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		if(theSprite == null)
			return;
		theSprite.sizeDelta = new Vector2(_Width, _Height);
	}

	public Vector2 getSpriteSize(GameObject _obj)								//得到图片大小
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		return theSprite.sizeDelta;
	}

	public void changeBarWidthHeight(GameObject _obj, int _Width, int _Height)		//改变图片宽度
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		if(theSprite == null)
			return;
		theSprite.sizeDelta = new Vector2(_Width, _Height);
	}

	public float getSpriteWidth(GameObject _obj)						//得到图片宽度
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		return theSprite.rect.width;
	}

	public float getSpriteHeight(GameObject _obj)						//得到图片宽度
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		return theSprite.rect.height;
	}

	public void forceUpdateUI()											//强制刷新UI
	{
		Canvas.ForceUpdateCanvases();
	}

	public void setAnchor(GameObject _go, Vector2 _anchor, Vector2 _pivot, Vector2 _anchoredPosition)		//设置锚点
	{
		RectTransform rt = _go.GetComponent<RectTransform>();
		rt.pivot = _pivot;
		rt.anchorMin = _anchor;
		rt.anchorMax = _anchor;
		rt.anchoredPosition = _anchoredPosition;
	}

	public void stretchToParent(GameObject _go)				//拉伸至父物体相同宽度
	{
		RectTransform rt = _go.GetComponent<RectTransform>();
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
		rt.sizeDelta = Vector2.zero;
	}

	public bool rectIntersectRect(GameObject _r1, GameObject _r2)		//
	{
		RectTransform rt1 = _r1.GetComponent<RectTransform>();
		Vector3[] rtCorners1 = new Vector3[4];
		rt1.GetWorldCorners(rtCorners1);

		RectTransform rt2 = _r2.GetComponent<RectTransform>();
		Vector3[] rtCorners2 = new Vector3[4];
		rt2.GetWorldCorners(rtCorners2);

		// If one rectangle is on left side of other
		if(rtCorners1[1].x > rtCorners2[3].x || rtCorners2[1].x > rtCorners1[3].x)
			return false;

		// If one rectangle is above other
		if(rtCorners1[1].y < rtCorners2[3].y || rtCorners2[1].y < rtCorners1[3].y)
			return false;

		return true;
	}

	public void getRectDiffs(GameObject _child, GameObject _container, ref Vector2 _xDif, ref Vector2 _yDif)		//
	{
		RectTransform rtChild = _child.GetComponent<RectTransform>();
		Vector3[] childCorners = new Vector3[4];
		rtChild.GetWorldCorners(childCorners);

		RectTransform rtCont = _container.GetComponent<RectTransform>();
		Vector3[] contCorners = new Vector3[4];
		rtCont.GetWorldCorners(contCorners);

		Vector2 minChild = new Vector2(Mathf.Infinity, Mathf.Infinity);
		Vector2 maxChild = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);
		Vector2 minCont = new Vector2(Mathf.Infinity, Mathf.Infinity);
		Vector2 maxCont = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);

		Graphic graphic = rtChild.GetComponent<Graphic>();

		getMinMaxFromCorners(ref minChild, ref maxChild, childCorners, graphic == null ? null : graphic.canvas);
		getMinMaxFromCorners(ref minCont, ref maxCont, contCorners, graphic == null ? null : graphic.canvas);

		float scaleFactor = graphic == null ? 1 : (graphic.canvas == null ? 1 : graphic.canvas.scaleFactor);

		_xDif = new Vector2((minChild.x - minCont.x) / scaleFactor, (maxCont.x - maxChild.x) / scaleFactor);
		_yDif = new Vector2((minChild.y - minCont.y) / scaleFactor, (maxCont.y - maxChild.y) / scaleFactor);
	}

	void getMinMaxFromCorners(ref Vector2 _min, ref Vector2 _max, Vector3[] _corners, Canvas _canvas)
	{
		Camera cam = _canvas == null ? null : _canvas.worldCamera;
		if(_canvas != null && _canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			cam = null;
		for(int i = 0; i < 4; i++)
		{
			Vector3 screenCoord = RectTransformUtility.WorldToScreenPoint(cam, _corners[i]);
			if(screenCoord.x < _min.x)
			{
				_min = new Vector2(screenCoord.x, _min.y);
			}
			if(screenCoord.y < _min.y)
			{
				_min = new Vector2(_min.x, screenCoord.y);
			}
			if(screenCoord.x > _max.x)
			{
				_max = new Vector2(screenCoord.x, _max.y);
			}
			if(screenCoord.y > _max.y)
			{
				_max = new Vector2(_max.x, screenCoord.y);
			}
		}
	}

	public float getSpritePositionX(GameObject _obj)		//得到图片X坐标
	{
		return _obj.transform.localPosition.x;
	}

	public float getSpritePositionY(GameObject _obj)		//设置图片Y轴坐标
	{
		return _obj.transform.localPosition.y;
	}

	public Vector2 getSpritePositionXY(GameObject _obj)		//得到图片XY坐标
	{
		return new Vector2(_obj.transform.localPosition.x, _obj.transform.localPosition.y);
	}

	public float getSpriteFactorY2(GameObject _obj)			//得到图片高度占比
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		return 1 - theSprite.pivot.y; // Top corresponds to pivot of 1, return 1 for bottom
	}

	public Vector3 getPositionRelativeTransform(GameObject _obj, GameObject _relative)
	{
		return _relative.transform.InverseTransformPoint(_obj.transform.TransformPoint(Vector3.zero));
	}

	public void changePositionByRelativeTransform(GameObject _obj, GameObject _relative, Vector2 _delta)
	{
		_obj.transform.position = _relative.transform.TransformPoint(getPositionRelativeTransform(_obj, _relative) + new Vector3(_delta.x, _delta.y, 0));
	}

	public void changeSpritePositionTo(GameObject _obj, Vector3 _newPos)
	{
		_obj.transform.localPosition = new Vector3(_newPos.x, _newPos.y, _newPos.z);
	}

	public void changeSpritePositionToX(GameObject _obj, float _newPos)
	{
		Vector3 thePos = _obj.transform.localPosition;
		_obj.transform.localPosition = new Vector3(_newPos, thePos.y, thePos.z);
	}

	public void changeSpritePositionToY(GameObject _obj, float _newPos)
	{
		Vector3 thePos = _obj.transform.localPosition;
		_obj.transform.localPosition = new Vector3(thePos.x, _newPos, thePos.z);
	}

	public Vector2 getChangeSpritePositionTo(GameObject _obj, Vector2 _newPos)
	{
		return new Vector2(_newPos.x, _newPos.y);
	}

	public void changeSpritePositionRelativeToObjBy(GameObject _obj, GameObject _relObj, Vector3 _changeAmt)
	{
		Vector3 thePos = _relObj.transform.localPosition;
		_obj.transform.localPosition = new Vector3(thePos.x + _changeAmt.x, thePos.y + _changeAmt.y, thePos.z + _changeAmt.z);
	}

	public void changeSpritePositionRelativeToObjByX(GameObject _obj, GameObject _relObj, float _changeAmt)
	{
		Vector3 thePos = _relObj.transform.localPosition;
		Vector3 curPos = _obj.transform.localPosition;
		_obj.transform.localPosition = new Vector3(thePos.x + _changeAmt, curPos.y, curPos.z);
	}

	public void changeSpritePositionRelativeToObjByY(GameObject _obj, GameObject _relObj, float _changeAmt)
	{
		Vector3 thePos = _relObj.transform.localPosition;
		Vector3 curPos = _obj.transform.localPosition;
		_obj.transform.localPosition = new Vector3(curPos.x, thePos.y + _changeAmt, curPos.z);
	}

	public Vector2 getSpritePivot(GameObject _obj)
	{
		RectTransform theSprite = _obj.GetComponent<RectTransform>();
		return theSprite.pivot;
	}

	public void changeSpriteParent(GameObject _child, GameObject _parent)
	{
		_child.transform.SetParent(_parent.transform, false);
	}

	public void getFirstCanvasOnSelfOrParent(Transform _trans, ref Canvas _canv)
	{
		_canv = _trans.GetComponent<Canvas>();
		if(_canv != null)
			return;
		if(_trans.parent == null)
			return;
		getFirstCanvasOnSelfOrParent(_trans.parent, ref _canv);
	}

	public void addRaycaster(GameObject _obj)
	{
		_obj.AddComponent<GraphicRaycaster>();
	}

	public void setAsNotInteractible(GameObject _obj)
	{
		CanvasGroup cg = _obj.GetComponent<CanvasGroup>();
		if(cg == null)
		{
			cg = _obj.AddComponent<CanvasGroup>();
		}
		cg.interactable = false;
		cg.blocksRaycasts = false;
	}

	public void bringSpriteToFront(GameObject _obj)
	{
		_obj.transform.SetAsLastSibling();
	}

	public void sendSpriteToBack(GameObject _obj)
	{
		_obj.transform.SetAsFirstSibling();
	}

	public string getDropdownSelection(GameObject _obj)
	{
		//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		//		return dropdown.value;
		return null;
	}

	public void setDropdownSelection(GameObject _obj, string _newval)
	{
		//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		//		dropdown.value = newval;
	}

	public void addDropdownItem(GameObject _obj, string _item)
	{
		//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		//		dropdown.items.Add(item);
	}

	public void deleteDropdownItem(GameObject _obj)
	{
		//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		//		dropdown.items.RemoveAt(dropdown.items.Count-1);
	}

	public void setDropdownIndex(GameObject _obj, int _index)
	{
		//		UIPopupList dropdown = obj.GetComponent<UIPopupList>();
		//		dropdown.value = dropdown.items[index];
	}

	public void setButtonColor(Color _Color, GameObject _obj)
	{
		//		UILabel aButton = obj.GetComponent<UILabel>();
		//		aButton.color = aColor;
	}

	public bool getToggle(GameObject _obj)
	{
		//		UIToggle theTog = obj.GetComponent<UIToggle>();
		//		return theTog.value;
		return false;
	}

	public void setToggle(GameObject _obj, bool _state)
	{
		//		UIToggle theTog = obj.GetComponent<UIToggle>();
		//		theTog.value = state;
	}

	public float getSliderVal(GameObject _obj)
	{
		//		UISlider theSlider = obj.GetComponent<UISlider>();
		//		return theSlider.value;
		return 0;
	}

	public void setSliderVal(GameObject obj, float val)
	{
		//		UISlider theSlider = obj.GetComponent<UISlider>();
		//		theSlider.value = val;
	}

	public void showControl(GameObject _obj)
	{
		SetActive(_obj, true);
	}

	public void hideControl(GameObject _obj)
	{
		SetActive(_obj, false);
	}

	public bool getControlVisibility(GameObject _obj)
	{
		return activeInHierarchy(_obj);
	}
}
