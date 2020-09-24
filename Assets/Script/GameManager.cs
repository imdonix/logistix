using DSoft;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
        return DebugMode;
    }

    public LevelMap GetMap()
    {
        return Map;
    }

    public Ship GetShip()
    {
        return Ship;
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


    #endregion

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
                    Debug.Log($"[LevelMap] Level map filled with {Map.CountLevels()}");
                }
                catch (IllegalLevelMapExeption ex) 
                { Debug.LogError(ex); };
            },
            err => Debug.LogError(err)
            );
    }

    #region GAME_EVENTS

    private void GameUpdate()
    {

    }

    private void GameEnd()
    {
        Menu.Instance.Swich(Menu.Instance.EndGame);
        (Menu.Instance.EndGame as EndPanel).SetResoult(Current.GetResoult());

        Destroy(Current);
        Current = null;
    }

    #endregion

}
