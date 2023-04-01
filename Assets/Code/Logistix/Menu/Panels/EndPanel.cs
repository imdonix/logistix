using Audio;
using Logistix;
using Logistix.Core;
using Networking.Models;
using Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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

        [SerializeField] public FireworkSpawner spawner;

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

        protected override void OnClose()
        {
            spawner.Stop();
            ClearComps();
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
            AddFreeBonus();
        }

        private void SetUp()
        {
            SoundPlayer.Instance.Play(resoult.IsWin ? SoundPlayer.Instance.win : SoundPlayer.Instance.lose);
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

                spawner.Spawn();
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
            GameManager.Instance.API.UploadLevelResoult(resoult,
            playerModel =>
            {
                if (retry)
                    GameManager.Instance.StartGame(resoult.ID);
                else
                    Player.Refresh(playerModel);

                Debug.Log($"Resoult uploaded {resoult}");
            },
            err =>
            {
                Debug.LogError("[Network] " + err);
                Menu.Instance.Pop("You went offline.", "You can't get reward while you're offline.");
                Player.Refresh();
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
            randomMultipier = UnityEngine.Random.Range(2, 3 + ShipUpgrade.GetExtraMultiplier());
            if (resoult.IsWin)
                Secoundary.text = $"{randomMultipier}x";
        }

        private void AddFreeBonus()
        {
            if (GameManager.Instance.GetAddFreeMode())
                if (resoult.IsWin)
                    OnSecoundary();
        }

        private void LoadTopList()
        {
            ToplistPanel.SetActive(true);
            ToplistLoading.SetActive(true);
            GameManager.Instance.API.GetToplist(resoult.ID,
                toplist =>
                {
                    if (resoult.IsWin)
                        AddResoultToList(toplist);

                    GenerateToplist(toplist);

                    if (toplist.Count > 0)
                    {
                        if (toplist[0].Score <= resoult.Score)
                            Status.text = $"New highscore!";
                    }

                    ToplistLoading.SetActive(false);
                },
                error =>
                {
                    Menu.Instance.Pop("You went offline.", "We can't get the toplist while you're offline.");
                    ToplistPanel.SetActive(false);
                });
        }

        private void AddResoultToList(List<RecordModel> toplist)
        {
            var player = Player.GetModel();
            RecordModel model = toplist.Find(r => r.Name.Equals(player.Name));
            if (ReferenceEquals(model, null))
                toplist.Add(new RecordModel() { Name = player.Name, Premium = player.Premium, Score = resoult.Score });
            else
                if (model.Score < resoult.Score)
                model.Score = resoult.Score;

            toplist.Sort();
        }

        private void GenerateToplist(List<RecordModel> toplist)
        {
            for (int i = 0; i < toplist.Count && i < TOP; i++)
            {
                RecordComponent comp = Instantiate(Record, Toplist);
                comp.gameObject.name = $"${i + 1}";
                comp.Get().localPosition = new Vector2(0, -i * RECORD_SIZE - (i > 0 ? RECORD_SIZE * .2f : 0) + START);
                comp.Set(i + 1, toplist[i]);
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
}