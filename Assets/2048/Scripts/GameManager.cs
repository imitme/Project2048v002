using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject lobbyCanvas;
    public GameObject inGameCanvas;

    public int totalCount = 4;
    public int testNumCellCountLimit = 1;
    public float cellMovingTime = 0.1f;

    private Grid grid = null;

    private void Awake()
    {
        grid = GameObject.FindObjectOfType<Grid>();
    }

    private void Start()
    {
        GotoMenu();
    }

    public void GotoMenu()
    {
        StartCoroutine(ChangePaneltoMenu());
        OnResetPlayerInfo();
    }

    private IEnumerator ChangePaneltoMenu()
    {
        //inGameCanvasAnim.SetTrigger("Stop");
        //lobbyCanvasAnim.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        inGameCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);

        //lobbyCanvasAnim.SetTrigger("Start");
    }

    private void OnResetPlayerInfo()
    {
        //ResetScore();
    }

    public IEnumerator GotoPlay()
    {
        Debug.Log("aa");
        // playButtonAnim.SetTrigger("Press");
        yield return new WaitForSeconds(0.3f);
        PlayGameStart();
    }

    private void PlayGameStart()
    {
        inGameCanvas.SetActive(true);
        lobbyCanvas.SetActive(false);

        //inGameCanvasAnim.SetTrigger("Start");

        //RESET
        grid.ResetPanel();

        grid.SetGridMap(totalCount);
        grid.SetCells(totalCount);
        grid.DrawRandomCells(totalCount, testNumCellCountLimit);
    }
}