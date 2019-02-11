using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalCount = 4;
    public int testNumCellCountLimit = 1;
    public float cellMovingTime = 0.1f;
    public GameObject lobbyCanvas;
    public GameObject inGameCanvas;

    public Text score_Text;

    private Grid grid = null;
    public int _score = 0;

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
        ResetPlayerInfo();
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

    private void ResetPlayerInfo()
    {
        grid.ResetScore();
    }

    public IEnumerator GotoPlay()
    {
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