using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScollRectUI : MonoBehaviour {

    // Use this for initialization
    private float width;
    private float height;

    public float widthAmend;//默认单元宽度与屏幕宽度比
    private float gridWidth;
    public float gridAspect;//单元宽度与高度比
    private float gridHeight;
    private int leftPadding;

    public bool isFull = true;
    public float viewWidthAmend;//Scroll View宽度与屏幕宽度比

    private GridLayoutGroup mGLG;

    void Awake() {
        if (isFull)
        {
            width = Screen.width;
        }
        else {
            width = Screen.width * viewWidthAmend;
        }
        height = Screen.height;
        mGLG = GetComponent<GridLayoutGroup>();
        getAmend();
        setGrid();
    }

    private void getAmend() {
        gridWidth = width * widthAmend;
        gridHeight = gridWidth / gridAspect;
        var n = mGLG.constraintCount;
        leftPadding = Convert.ToInt32((width - gridWidth * n) / (n + 1));
    }

    private void setGrid() {
        mGLG.cellSize = new Vector2(gridWidth, gridHeight);
        mGLG.padding.left = leftPadding;
        mGLG.spacing = new Vector2(leftPadding, mGLG.spacing.y);
    }

    public void setContentSize(int max) {
        var rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, gridHeight * max + mGLG.spacing.y * (max - 1));
    }

    public Vector2 getGridSize() {
        return mGLG.cellSize;
    }

    public Vector2 getGridSpacing() {
        return mGLG.spacing;
    }

}
