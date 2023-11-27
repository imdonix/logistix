using UnityEngine;
using Newtonsoft.Json;
using Networking.Core;
using Networking.Models;
using Logistix.Core;
using Utils;
using Logistix.Utils;
using UI;

namespace Logistix
{
    public class GameManager : Singleton<GameManager>
    {
        private const string LEVEL_COUT_KEY = "levelcount";

        [Header("Settings")]
        [SerializeField] private bool IsOfflineMode;
        [SerializeField] private bool DebugMode;
        [SerializeField] private bool AddFreeMode;

        [Header("Prefabs")]
        [SerializeField] private Ship Ships;
        [SerializeField] private Game Template;

        [Header("Game Elements")]
        [SerializeField] public Crane Crane;

        private Box[] Boxes;
        private LevelMap Map;
        private Ship Ship;
        private Game Current;

        public IAPI API => IsOfflineMode ? (IAPI) GetComponent<OfflineAPI>() : (IAPI) GetComponent<LogisticAPI>();


        #region UNITY

        public void Start()
        {
            Boxes = Resources.LoadAll<Box>("Boxes");
            Ship = Instantiate(Ships);

            DownloadLevelMap();

            Menu.Instance.Swich(Menu.Instance.Login);
        }

        #endregion

        #region PUBLIC

        public Game GetGameTemplate()
        {
            return Template;
        }

        public bool IsDebugMode()
        {
            return DebugMode || IsRunningEditorMode();
        }

        public LevelMap GetMap()
        {
            return Map;
        }

        public Ship GetShip()
        {
            return Ship;
        }

        public Game GetCurrent()
        {
            return Current;
        }

        public Box GetBox(int id)
        {
            int i = 0;
            while (i < Boxes.Length && Boxes[i].ID != id)
                i++;

            if (i < Boxes.Length)
                return Boxes[i];
            else
                throw new BoxNotExistsExeption(id);
        }

        public Ship GetShipTemplate()
        {
            return Ships;
        }

        public void StartGame(string id)
        {
            Current = Game.CreateGame(Map.GetLevelByID(id));
            Menu.Instance.Swich(Menu.Instance.InGame);
            Current.StartGame(GameUpdate, GameEnd);
        }

        public bool GetAddFreeMode()
        {
            return AddFreeMode;
        }

        public static LevelRowModel[] LoadOfflineMap()
        {
            TextAsset cache = Resources.Load<TextAsset>("levelmap");
            return JsonConvert.DeserializeObject<LevelRowModel[]>(cache.text);
        }

        #endregion

        #region PRIVATE

        private void DownloadLevelMap()
        {
            GameManager.Instance.API.GetLevelMap(
                res =>
                {
                    try
                    {
                        Map = LevelMap.Create(res);
                        ShowLevesLoaded(Map.CountLevels());
                    }
                    catch (IllegalLevelMapExeption e)
                    {
                        Debug.LogError($"Levelmap can't be parsed: {e}");

                        Map = LevelMap.Create(LoadOfflineMap());
                        ShowLevesLoaded(Map.CountLevels());
                    };
                },
                err => 
                {
                    Debug.LogWarning("Levelmap cant be loaded - " + err);

                    Map = LevelMap.Create(LoadOfflineMap());
                    ShowLevesLoaded(Map.CountLevels());
                });
        }

        private void ShowLevesLoaded(int count)
        {
            int old = 0;
            if (PlayerPrefs.HasKey(LEVEL_COUT_KEY))
                old = PlayerPrefs.GetInt(LEVEL_COUT_KEY);

            if (count > old)
            {
                Menu.Instance.Pop("New levels aviable!", $"{count - old} new level available");
                PlayerPrefs.SetInt(LEVEL_COUT_KEY, count);
            }
        }

        private bool IsRunningEditorMode()
        {
            return Application.platform == RuntimePlatform.WindowsEditor;
        }

        #endregion

        #region GAME_EVENTS

        private void GameUpdate()
        {
            (Menu.Instance.InGame as InPanel).UpdateDropped();
        }

        private void GameEnd()
        {
            Ship.Send(Current);

            Menu.Instance.Swich(Menu.Instance.EndGame);
            (Menu.Instance.EndGame as EndPanel).SetResoult(Current.GetResoult());

            Current = null;
        }

        #endregion

    }
}