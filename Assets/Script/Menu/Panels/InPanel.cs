﻿using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InPanel : MenuPanel
{

    [SerializeField] public int CountDown;
    [SerializeField] public Text TitleLabel;
    [SerializeField] public Text CountDownLabel;
    [SerializeField] public Transform Panel;
    [SerializeField] public Text Left;
    [SerializeField] public Text Mistakes;


    public bool IsInit { get; private set; }

    protected override void OnClose(){}

    protected override void OnOpen()
    {
        SetState(true);
        ShowStart(GetGame());
    }

    #region PUBLIC
    
    public void UpdateDropped()
    {
        int left = GetGame().GetBoxesLeft();
        (int, int) lost = GetGame().GetLostAndMax();
        
        Left.text = left > 0 ? $"{left} left" : "Last one";

        StringBuilder sb = new StringBuilder("");
        for (int i = 0; i < lost.Item2; i++)
            sb.Append(lost.Item1 > i ? 'X' : 'O');
        Mistakes.text = sb.ToString();            
    }

    #endregion

    #region PRIVATE

    private void SetState(bool isInit)
    {
        TitleLabel.gameObject.SetActive(isInit);
        CountDownLabel.gameObject.SetActive(isInit);
        Panel.gameObject.SetActive(!isInit);
        IsInit = isInit;
    }

    private void ShowStart(Game game)
    {
        TitleLabel.text = game.GetName();
        StartCoroutine(StartCountDown());
    }

    private IEnumerator StartCountDown()
    {
        CountDownLabel.text = "Waiting for the ship...";

        yield return new WaitUntil(() => GameManager.Instance.GetShip().IsReady());

        for (int i = 0; i < CountDown; i++)
        {
            CountDownLabel.text = $"{CountDown - i}";
            yield return new WaitForSeconds(1);
        }

        CountDownLabel.text = "Drop.";

        yield return new WaitForSeconds(0.5f);

        SetState(false);
    }

    #endregion

    private static Game GetGame()
    {
        return GameManager.Instance.GetCurrent();
    }

}