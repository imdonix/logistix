using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
    public const float WATER_LEVEL = -1.5f;

    private const float TIME_BETWEEN_DROPS = 3;
    private const float SCORE_DISTANCE = 2;
    private const float SCORE_MIN = 0.25f;
    private const float SCORE_MAX = 3f;

    [Header("Data")]
    [SerializeField] private int ID;
    [SerializeField] private int[] BoxIDs;
    [SerializeField] private int Mistakes;


    private Action OnUpdate, OnEnd;
    private GameState State;
    private List<Box> Inventory;
    private List<Box> Dropped;
    private Box Selected;

    private int Wood;
    private int Iron;
    private bool Win;
    private int Score;
    private float PlayTime;
    private string Name;

    #region UNITY

    private void FixedUpdate()
    {
        UpdatePlayTime();
        CheckLoseConditions();
    }

    private void OnDestroy()
    {
        DeRegisterEvents();
        DestroyBoxes();
        Item.Clear();
    }

    #endregion

    #region PUBLIC

    public void StartGame(Action onUpdate, Action onEnd)
    {
        RegisterEvents(onUpdate, onEnd);

        SetCraneExcursion();
        SetCranePosition();
        CreateInventory();

        Select(0);
    }

    public void Touch()
    {
        if(CanInteract())
            if (State == GameState.Drop)
                Drop();
    }

    public void Swipe(int direction)
    {
        if (CanInteract())
            Debug.Log($"{direction}");
        //TODO
    }

    public ResoultModel GetResoult()
    {
        return new ResoultModel()
        {
            ID = this.ID,
            LostBoxes = CountLostBoxes(),
            IsWin = this.Win,
            Score = this.Score,
            Multiplies = false,
            Iron = Iron,
            Wood = Wood,
            Time = Mathf.RoundToInt(PlayTime),
            Email = Player.Instance.GetUserID()
        };
    }

    public string GetName()
    {
        return Name;
    }

    public int GetBoxesLeft()
    {
        return Inventory.Count;
    }

    public (int, int) GetLostAndMax()
    {
        return (CountLostBoxes(), Mistakes);
    }

    #endregion

    #region PRIVATE

    private void EndGame(bool isWin)
    {
        State = GameState.End;
        Win = isWin;
        AttachSuccesfullBoxesToShip();
        OnEnd.Invoke();
    }

    private void CreateInventory()
    {
        Inventory = new List<Box>(BoxIDs.Length);
        Dropped = new List<Box>(BoxIDs.Length);
        foreach (int id in BoxIDs)
            Inventory.Add(GameManager.Instance.GetBox(id));
    }

    private void Select(int slot)
    {
        if (Inventory.Count > 0 && IsGameInProgress())
        {
            Selected = Instantiate(Inventory[slot]);
            Inventory.Remove(Inventory[slot]);
            GameManager.Instance.Crane.Attach(Selected);
            State = GameState.Drop;
            OnUpdate.Invoke();
        }
        else
            if(!IsGameLost())
                EndGame(true);
    }

    private void Drop()
    {
        IEnumerator DeAtttach()
        {
            
            Box dropped = GameManager.Instance.Crane.Release();
            dropped.OnEject();
            Dropped.Add(dropped);
            State = GameState.Wait;

            yield return new WaitForSeconds(TIME_BETWEEN_DROPS);
            Score += GetScoreOf(dropped);
            Select(0);
        }
        StartCoroutine(DeAtttach());
    }

    private int GetScoreOf(Box dropped)
    {
        float worth = dropped.Score;
        float distance = Vector2.Distance(GetDroppedBoxesCenter(), dropped.GetPosition());
        float score = worth * (SCORE_DISTANCE / distance);
        return Mathf.RoundToInt(Mathf.Clamp(score, worth * SCORE_MIN, worth * SCORE_MAX));
    }

    private Vector2 GetDroppedBoxesCenter()
    {
        Vector2 center = Vector2.zero;
        int c = 0;
        foreach (var box in Dropped)
            if (!box.IsLost())
                center += box.GetPosition();
            else
                c++;
        return center /= Dropped.Count - c;
    }

    private void SetCraneExcursion()
    {
        Ship ship = GameManager.Instance.GetShip();
        float excursion = ship.GetSize() / 2;
        GameManager.Instance.Crane.SetExcursion(excursion);
    }

    private void SetCranePosition()
    {
        Crane crane = GameManager.Instance.Crane;
        Ship ship = GameManager.Instance.GetShip();
        crane.SetPosition(ship.GetSize());
    }

    private bool IsGameInProgress()
    {
        return State != GameState.End;
    }

    private bool IsGameLost()
    {
        return CountLostBoxes() > Mistakes;
    }

    private int CountLostBoxes()
    {
        return Dropped.Count(u => u.IsLost());
    }

    private void CheckLoseConditions()
    {
        if (IsGameLost() && IsGameInProgress()) 
            EndGame(false);
    }

    private void DestroyBoxes()
    {
        foreach (Box box in Dropped)
            box.Destroy();
        if (!ReferenceEquals(Selected, null))
            Selected.Destroy();
    }

    private void RegisterEvents(Action onUpdate, Action onEnd)
    {
        OnUpdate = onUpdate;
        OnEnd = onEnd;
        InputHandler.Instance.AddClickHandler(Touch);
        InputHandler.Instance.AddSwipeHandler(Swipe);
    }

    private void DeRegisterEvents()
    {
        InputHandler.Instance.RemoveTouchHandlers();
    }

    private void UpdatePlayTime()
    {
        PlayTime += Time.deltaTime;
    }

    private bool CanInteract()
    {
        return !(Menu.Instance.InGame as InPanel).IsInit;
    }

    private void AttachSuccesfullBoxesToShip()
    {
        Ship ship = GameManager.Instance.GetShip();
        foreach (var box in Dropped)
            if (!box.IsLost())
                box.transform.SetParent(ship.transform);
    }

    #endregion

    #region STATIC

    private static Game InitTemplate()
    {
        return Instantiate(GameManager.Instance.GetGameTemplate());
    }

    public static Game CreateGame(int[] boxes)
    {
        Game tmp = InitTemplate();
        tmp.BoxIDs = boxes;
        tmp.Score = 0;
        tmp.Name = "test.";
        return tmp;
    }

    public static Game CreateGame(LevelModel game)
    {
        Game tmp = CreateGame(game.BoxIDs);
        tmp.ID = game.ID;
        tmp.Mistakes = game.MaxLost;
        tmp.Iron = game.IronReward;
        tmp.Wood = game.WoodReward;
        tmp.Name = game.Name;
        return tmp;
    }
        
    #endregion
}
