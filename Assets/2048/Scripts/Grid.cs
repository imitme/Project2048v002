using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int totalCount = 4;

    public GameObject cellsPanel;
    public GameObject cellPrefab;

    private float myCellSize;
    private Vector3 firstPos = Vector3.zero;

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
        //DrawRandomCells(totalCount, firstSettingLimitNum);
    }

    private void SetGridMap(int count)
    {
        var myPanel = cellsPanel.GetComponent<RectTransform>();
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
                DrawCells(cellPrefab, cellsPanel, c, r, myCellSize, string.Format("Cell ({0}, {1})", c, r));
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
}