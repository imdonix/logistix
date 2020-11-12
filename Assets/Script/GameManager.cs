using DSoft;
using Facebook.Unity;
using System;
using UnityEngine;
using UnityScript.Lang;

public class GameManager : Singleton<GameManager>
{
    private const string LEVEL_COUT_KEY = "levelcount";

    [Header("Settings")]
    [SerializeField] private bool DebugMode;

    [Header("Prefhabs")]
    [SerializeField] private Box[] Boxes;

    [SerializeField] private Ship[] Ships;
    [SerializeField] private Game Template;

    [Header("Game Elements")]
    [SerializeField] public Crane Crane;


    private LevelMap Map;

    private Ship Ship;
    private Game Current;


    #region UNITY

    public void Start()
    {
        DownloadLevelMap();
        CreateShip();
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

    public void StartGame(int id)
    {
        Current = Game.CreateGame(Map.GetLevelByID(id));
        Menu.Instance.Swich(Menu.Instance.InGame);
        Current.StartGame(GameUpdate, GameEnd);   
    }

    public void StartFacebookShare()
    {
        if (FB.IsInitialized)
            FB.ShareLink(new Uri($"{LogisticAPI.Instance.GetServerURI()}/invite/{Player.Instance.GetModel().Name}"),
                "Start playing logistix now!",
                "Can you beat my highscore? Try logistix now!");
        else
            throw new Exception("FB not inicialized");
    }

    #endregion

    #region PRIVATE

    private void CreateShip()
        {
            Ship = Instantiate(Ships[0]);
        }

        private void DownloadLevelMap()
        {
            LogisticAPI.Instance.GetLevelMap(
                res =>
                {
                    try
                    {
                        Map = LevelMap.Create(res);
                        ShowLevesLoaded(Map.CountLevels());
                    }
                    catch (IllegalLevelMapExeption ex)
                    { Debug.LogError(ex); };
                },
                err => Debug.LogError(err)
                );
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
