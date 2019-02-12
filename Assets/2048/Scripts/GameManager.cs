using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject lobbyCanvas;
    public GameObject inGameCanvas;

    public Text scoreText;

    private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; scoreText.text = string.Format("Score : {0}", score); }
    }

    public event Action OnPlayGame, OnResetScore;

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
        OnResetScore?.Invoke();
    }

    public void GotoPlay()
    {
        StartCoroutine(GotoPlayProcess());
    }

    private IEnumerator GotoPlayProcess()
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

        OnPlayGame?.Invoke();
    }
}