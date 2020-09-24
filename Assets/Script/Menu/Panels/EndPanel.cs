using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MenuPanel
{
    private const int RECORD_SIZE = 115;
    private const int TOP = 5;
    private const int START = RECORD_SIZE * 2;

    [Header("Properties")]
    [SerializeField] public RecordComponent Record;

    [Header("Dependencies")]
    [SerializeField] public GameObject ScorePanel;
    [SerializeField] public GameObject RewardPanel;
    [SerializeField] public GameObject ButtonPanel;
    [SerializeField] public GameObject ToplistPanel;

    [SerializeField] public Text Status;
    [SerializeField] public Text Score;
    [SerializeField] public Text Wood;
    [SerializeField] public Text Iron;
    [SerializeField] public Text Primary;
    [SerializeField] public Text Secoundary;
    [SerializeField] public GameObject ToplistLoading;
    [SerializeField] public GameObject RewardButton;
    [SerializeField] public RectTransform Toplist;

    private ResoultModel resoult;
    private int randomMultipier;
    private List<RecordComponent> comps = new List<RecordComponent>();
    private float rotation;

    protected override void OnOpen()
    {
        ScorePanel.SetActive(false);
        RewardPanel.SetActive(false);
        ToplistPanel.SetActive(false);
        ButtonPanel.SetActive(false);
        RewardButton.SetActive(true);
        Status.text = "Loading";
    }

    private void Update()
    {
        rotation += 480 * Time.deltaTime;
        ToplistLoading.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);
    }

    public override void Back()
    {
        if (resoult.IsWin)
            OnPrimary();
        else
            OnSecoundary();
    }

    public void SetResoult(ResoultModel resoult)
    {
        this.resoult = resoult;
        SetUp();
        GenerateRandomMultiplier();
    }

    private void SetUp()
    {
        Primary.text = resoult.IsWin ? "Continue" : "Retry";
        Secoundary.text = resoult.IsWin ? "Continue" : "Retry";
        Status.text = resoult.IsWin ? "You won!" : "You lost!";
        Primary.text = resoult.IsWin ? "Continue" : "Retry";
        Secoundary.text = resoult.IsWin ? "Xx" : "Back";
        ButtonPanel.SetActive(true);

        if (resoult.IsWin)
        {
            ScorePanel.SetActive(true);
            Score.text = resoult.Score.ToString();

            RewardPanel.SetActive(true);
            Wood.text = resoult.Wood.ToString();
            Iron.text = resoult.Iron.ToString();
        }

        LoadTopList();
    }

    #region UI

    public void OnPrimary()
    {
        if (resoult.IsWin)
            UploadLevel(false);
        else
            UploadLevel(true);
            
    }

    public void OnSecoundary()
    {
        if (resoult.IsWin)
            PopRewardedAdd();
        else
            UploadLevel(false);
    }

    #endregion

    private void UploadLevel(bool retry)
    {
        ButtonPanel.SetActive(false);
        LogisticAPI.Instance.UploadLevelResoult(resoult,
        playerModel =>
        {
            if(retry)
                GameManager.Instance.StartGame(resoult.ID);
            else
                Player.Instance.Refresh(playerModel);

            Debug.Log($"Resoult uploaded {resoult}");
        },
        err =>
        {
            Debug.Log($"Resoult saved localy {resoult}");
            Debug.LogError("[Network] " + err);
            //TODO store localy
            Player.Instance.Refresh();
        });
    }

    private void PopRewardedAdd()
    {
        OnAddSuccesfull();
        Debug.Log("Rewarded Add popped");
    }

    private void OnAddSuccesfull()
    {
        resoult.Iron *= randomMultipier;
        resoult.Wood *= randomMultipier;
        resoult.Multiplies = true;

        RewardButton.SetActive(false);
        Wood.text = $"{resoult.Wood / randomMultipier} x {randomMultipier}";
        Iron.text = $"{resoult.Iron / randomMultipier} x {randomMultipier}";

        Debug.Log("Rewarded Add finished");
    }

    private void GenerateRandomMultiplier()
    {
        randomMultipier = UnityEngine.Random.Range(3, 5);
        if (resoult.IsWin)
            Secoundary.text = $"{randomMultipier}x";
    }

    private void LoadTopList()
    {
        ToplistPanel.SetActive(true);
        ToplistLoading.SetActive(true);
        LogisticAPI.Instance.GetToplist(resoult.ID,
            toplist =>
            {
                GenerateToplist(toplist);
                
                if (toplist.Length > 0) 
                {
                    if (toplist[0].Score < resoult.Score)
                        Status.text = $"New highscore!";
                }
                else
                    Status.text = $"New highscore!";

                ToplistLoading.SetActive(false);
            },
            error =>
            {
                Debug.LogError($"Toplist cant be loaded! {error}");
                ToplistPanel.SetActive(false);
            });
    }

    private void GenerateToplist(RecordModel[] toplist)
    {
        ClearComps();
        for (int i = 0; i < toplist.Length && i < TOP; i++)
        {
            RecordComponent comp = Instantiate(Record, Toplist);
            comp.gameObject.name = $"${i+1}";
            comp.Get().localPosition = new Vector2(0, i * RECORD_SIZE + START);
            comp.Set(i+1,toplist[i]);
            comps.Add(comp);
        }
    }

    private void ClearComps()
    {
        foreach (var comp in comps)
            Destroy(comp.gameObject);
        comps.Clear();
    }
}

