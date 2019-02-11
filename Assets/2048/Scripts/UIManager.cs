using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Grid grid = null;
    private GameManager gameManager = null;
    private DIRECTION dir;

    private void Awake()
    {
        grid = GameObject.FindObjectOfType<Grid>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
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

    public void OnGotoMenu_Button()
    {
        gameManager.GotoMenu();
    }

    public void OnPlay_Button()
    {
        StartCoroutine(gameManager.GotoPlay());
    }
}