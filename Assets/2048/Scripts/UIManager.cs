using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Grid grid = null;
    private DIRECTION dir;

    private void Awake()
    {
        grid = GameObject.FindObjectOfType<Grid>();
    }

    public void OnR_Button()
    {
        dir = DIRECTION.RIGHT;
        grid.MovetoDir(dir);
    }

    public void OnL_Button()
    {
        dir = DIRECTION.LEFT;
        grid.MovetoDir(dir);
    }

    public void OnU_Button()
    {
        dir = DIRECTION.UP;
        grid.MovetoDir(dir);
    }

    public void OnD_Button()
    {
        dir = DIRECTION.DOWN;
        grid.MovetoDir(dir);
    }
}