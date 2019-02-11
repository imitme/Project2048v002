using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Grid : MonoBehaviour
{
    public int totalCount = 4;
    public int testNumCellCountLimit = 1;
    public float cellMovingTime = 0.1f;

    public GameObject gridCellsPanel;
    public GameObject gridCellPrefab;
    public GameObject numCellsPanel;
    public GameObject numCellPrefab;

    private float myCellSize;
    private Vector3 firstPos = Vector3.zero;

    public List<NumCell> cellsNum;

    private void Start()
    {
        PlayGameStart();
    }

    private void PlayGameStart()
    {
        //lobbyPanel.SetActive(false);
        //playPanel.SetActive(true);

        //inGameCanvasAnim.SetTrigger("Start");

        ////RESET
        //deleteCellsPanel();
        //deleteCellsNumPanel();
        //cellsNum.Clear();   //리스트.Clear() ;

        SetGridMap(totalCount);
        SetCells(totalCount);
        DrawRandomCells(totalCount, testNumCellCountLimit);
    }

    private void SetGridMap(int count)
    {
        var myPanel = gridCellsPanel.GetComponent<RectTransform>();
        Vector2 myPanelSize = myPanel.sizeDelta;

        var firstPositionX = myPanelSize.x / count / 2;
        var firstPositionY = myPanelSize.y / count / 2;
        Vector2 firstPosition = new Vector2(firstPositionX, firstPositionY);
        firstPos = firstPosition;

        var cellSize = myPanelSize.x / count;
        myCellSize = cellSize;
    }

    private void SetCells(int count)
    {
        for (int c = 0; c < count; c++)
        {
            for (int r = 0; r < count; r++)
            {
                DrawCells(gridCellPrefab, gridCellsPanel, c, r, myCellSize, string.Format("Cell ({0}, {1})", c + 1, r + 1));
            }
        }
    }

    private void DrawCells(GameObject cellPrefab, GameObject CellsPanel, int c, int r, float cellSize, string cellname)
    {
        GameObject cel = Instantiate(cellPrefab, CellsPanel.transform);
        cel.GetComponent<RectTransform>().localPosition = PointToVector3(c, r);
        cel.name = cellname;
    }

    private Vector3 PointToVector3(int col, int row)
    {
        return new Vector3(firstPos.x + col * myCellSize, firstPos.y + row * myCellSize, 0);
    }

    private void DrawRandomCells(int count, int totalCellNum)
    {
        int limitCount = 0;

        while (limitCount < totalCellNum)
        {
            int col = Random.Range(0, count);
            int row = Random.Range(0, count);

            if (IsEmpty(col, row))
            {
                DrawCellNum(numCellPrefab, numCellsPanel, col, row);
                limitCount++;
            }
        }
    }

    private bool IsEmpty(int col, int row)
    {
        foreach (NumCell cellNum in cellsNum)
        {
            if (cellNum.c == col && cellNum.r == row)
            {
                return false;
            }
        }
        return true;
    }

    private void DrawCellNum(GameObject cellNumPrefab, GameObject cellNumPanel, int col, int row)
    {
        GameObject cel = Instantiate(cellNumPrefab, cellNumPanel.transform);
        cel.GetComponent<RectTransform>().localPosition = PointToVector3(col, row);

        //cel.GetComponent<Image>().color = Random.ColorHSV();

        var cellNum = cel.GetComponent<NumCell>();
        cellNum.c = col;
        cellNum.r = row;
        cellNum.name = string.Format("({0}, {1})", cellNum.c, cellNum.r);
        cellsNum.Add(cellNum);
    }
}