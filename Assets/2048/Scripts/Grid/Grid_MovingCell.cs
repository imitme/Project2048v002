using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIRECTION
{
    UP = 0, DOWN, RIGHT, LEFT, COUNT
};

public partial class Grid : MonoBehaviour
{
    public void MovetoDir(DIRECTION dir)
    {
        CheckEmpthOriginalList();
        bool isMove = GetCellsDirLine(dir);

        DrawOneCell(isMove);
    }

    private void CheckEmpthOriginalList()
    {
        cellsNum.RemoveAll(cn => cn == null);
    }

    private bool GetCellsDirLine(DIRECTION dir)
    {
        bool checkMove = false;
        int dirCol = 0;
        int dirRow = 0;
        int startPoint = 0;

        switch (dir)
        {
            case DIRECTION.UP:
                dirRow = 1;
                startPoint = totalCount - 1;

                break;

            case DIRECTION.DOWN:
                dirRow = -1;

                break;

            case DIRECTION.RIGHT:
                dirCol = 1;
                startPoint = totalCount - 1;
                break;

            case DIRECTION.LEFT:
                dirCol = -1;

                break;

            case DIRECTION.COUNT:
                break;

            default:
                break;
        }

        checkMove = MoveCells(dir, dirCol, dirRow, startPoint);

        return checkMove;
    }

    private bool MoveCells(DIRECTION dir, int dirCol, int dirRow, int startPoint)
    {
        bool checkMove = false;
        bool checkMerge = false;
        int checkMergeCount = 0;
        int checkMoveCount = 0;

        for (int lineCount = 0; lineCount < totalCount; lineCount++)
        {
            int movePoint = startPoint;
            List<NumCell> celLine = new List<NumCell>();

            GetJustOneLineList(celLine, lineCount, dirRow);

            switch (dir)
            {
                case DIRECTION.UP:
                    celLine.Sort((a, b) => b.r.CompareTo(a.r));
                    checkMerge = MergeCellNum(celLine);
                    if (checkMerge == true)
                        checkMergeCount++;
                    checkMove = UpMove(celLine, checkMove, movePoint);
                    if (checkMove == true)
                        checkMoveCount++;
                    break;

                case DIRECTION.DOWN:
                    celLine.Sort((a, b) => a.r.CompareTo(b.r));
                    checkMerge = MergeCellNum(celLine);
                    if (checkMerge == true)
                        checkMergeCount++;
                    checkMove = DownMove(celLine, checkMove, movePoint);
                    if (checkMove == true)
                        checkMoveCount++;
                    break;

                case DIRECTION.RIGHT:
                    celLine.Sort((a, b) => b.c.CompareTo(a.c));
                    checkMerge = MergeCellNum(celLine);
                    if (checkMerge == true)
                        checkMergeCount++;
                    checkMove = RightMove(celLine, checkMove, movePoint);
                    if (checkMove == true)
                        checkMoveCount++;
                    break;

                case DIRECTION.LEFT:
                    celLine.Sort((a, b) => a.c.CompareTo(b.c));
                    checkMerge = MergeCellNum(celLine);
                    if (checkMerge == true)
                        checkMergeCount++;
                    checkMove = LeftMove(celLine, checkMove, movePoint);
                    if (checkMove == true)
                        checkMoveCount++;
                    break;

                case DIRECTION.COUNT:
                    break;

                default:
                    break;
            }
        }

        if (checkMergeCount > 0)
        {
            checkMerge = true;
        }
        if (checkMoveCount > 0)
        {
            checkMove = true;
        }

        Debug.Log("합쳐졌니? " + checkMerge + "  움직였니? " + checkMove);
        return checkMerge || checkMove;
    }

    private bool UpMove(List<NumCell> celLine, bool checkMove, int movePoint)
    {
        foreach (var cel in celLine)
        {
            if (cel == null)
                continue;

            checkMove = SetMovingPointofCell(checkMove, cel, cel.c, movePoint);
            movePoint--;
        }

        return checkMove;
    }

    private bool DownMove(List<NumCell> celLine, bool checkMove, int movePoint)
    {
        foreach (var cel in celLine)
        {
            if (cel == null)
                continue;

            checkMove = SetMovingPointofCell(checkMove, cel, cel.c, movePoint);
            movePoint++;
        }
        return checkMove;
    }

    private bool RightMove(List<NumCell> celLine, bool checkMove, int movePoint)
    {
        foreach (var cel in celLine)
        {
            if (cel == null)
                continue;

            checkMove = SetMovingPointofCell(checkMove, cel, movePoint, cel.r);
            movePoint--;
        }
        return checkMove;
    }

    private bool LeftMove(List<NumCell> celLine, bool checkMove, int movePoint)
    {
        foreach (var cel in celLine)
        {
            if (cel == null)
                continue;

            checkMove = SetMovingPointofCell(checkMove, cel, movePoint, cel.r);
            movePoint++;
        }
        return checkMove;
    }

    private bool SetMovingPointofCell(bool checkMove, NumCell cell, int col, int row)
    {
        int currCol = cell.c;
        int currRow = cell.r;
        Vector3 currPos = cell.GetComponent<RectTransform>().localPosition;
        float movingTime = cellMovingTime;

        if (currCol == col && currRow == row)
        {
            checkMove = false;
            return checkMove;
        }

        cell.c = col;
        cell.r = row;
        StartCoroutine(OnMoving(cell, currPos, col, row, movingTime));

        checkMove = true;
        return checkMove;
    }

    private IEnumerator OnMoving(NumCell movingCell, Vector3 startPos, int targetCol, int targetRow, float movingTime)
    {
        Vector3 currPos = startPos;
        Vector3 goalPos = PointToVector3(targetCol, targetRow);
        for (float t = 0.0f; t <= movingTime; t += Time.deltaTime)
        {
            currPos = Vector3.Lerp(startPos, goalPos, t / movingTime);
            movingCell.transform.localPosition = currPos;
            yield return null;  //왜 여기에?????  //렍더기다리는???
        }

        movingCell.GetComponent<RectTransform>().localPosition = goalPos;
    }

    private void GetJustOneLineList(List<NumCell> cellLine, int lineCount, int checkLineAsRow)
    {
        foreach (var cel in cellsNum)
        {
            if (checkLineAsRow == 0)    //행 단위로 줄 묶기 //좌우 버튼을 눌렀다는 뜻
            {
                if (lineCount == cel.r)    //행이 같은 애들 찾아
                {
                    var cellinline = GetCellNum(cel.c, cel.r);
                    cellLine.Add(cellinline);
                }
            }
            else    //열 단위로 줄 묶기 //위아래 버튼을 눌렀다는 뜻
            {
                if (lineCount == cel.c)     //열이 같은 애들 찾아.
                {
                    var cellinline = GetCellNum(cel.c, cel.r);
                    cellLine.Add(cellinline);
                }
            }
        }
    }

    private NumCell GetCellNum(int col, int row)
    {
        foreach (NumCell cellNum in cellsNum)
        {
            if (cellNum.c == col && cellNum.r == row)
            {
                return cellNum;
            }
        }
        return null;
    }

    private bool MergeCellNum(List<NumCell> celLine)
    {
        bool checkMove = false;
        ///정렬된 celLine에 있는 것의 숫자를 비교해!
        for (int cellPoint = 0; cellPoint < celLine.Count; cellPoint++)
        {
            int currentCell = cellPoint;
            int nextCell = cellPoint + 1;

            if (nextCell >= celLine.Count)
                break;

            if (celLine[currentCell].num == celLine[nextCell].num)
            {
                ///점수 보내주고
                SendScoreNum(celLine[currentCell].num);

                ///움직이고

                ///mergeAnim
                //celLine[currentCell].StartMergeAnim();

                ///숫자 합쳐주고.
                int mergeNum = celLine[nextCell].num;
                mergeNum += mergeNum;
                celLine[nextCell].num = mergeNum;

                ///i+1 없앤다 >> 현재셀을 없애는데, 움직시는 시간 지난 후에!
                DestroyImmediate(celLine[currentCell].gameObject);
                celLine.RemoveAt(currentCell);

                ///움직임체크!
                checkMove = true;
            }
        }

        CheckEmpthOriginalList();

        return checkMove;
    }

    private void SendScoreNum(int addScore)
    {
        int currentScore = score;
        currentScore += addScore;
        score = currentScore;
    }

    private void DrawOneCell(bool isMove)
    {
        Debug.Log(isMove);
        if (!isMove)
            return;
        else if (isMove)
        {
            DrawRandomCells(totalCount, 1);
        }
    }
}