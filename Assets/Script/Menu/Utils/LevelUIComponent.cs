using System;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LevelUIComponent : MonoBehaviour
{
    [Header("Properties")]
    public Color Open;
    public Color Locked;
    public Color Done;
    public Color Premium;
    [Header("View")]
    public RectTransform Rect;
    public LevelState state;

    private DiamondGraph Image;
    private LevelMap map;
    private int id;
    private int[] dep;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        Image = GetComponent<DiamondGraph>();
        map = GameManager.Instance.GetMap();
        SetAlpha();
    }

    public int GetID()
    {
        return id;
    }

    public int[] GetDeps()
    {
        return dep;
    }

    private void SetAlpha()
    {
    }

    public void Load(LevelModel model)
    {
        id = model.ID;
        dep = model.Unlocks;

        if (map.IsDone(id))
            state = LevelState.Done;
        else
        {
            if (map.IsUnlocked(id))
            {
                if (model.IsPremiumOnly)
                    state = LevelState.Premium;
                else
                    state = LevelState.Open;
            }
            else
                state = LevelState.Locked;
        }

        SetColor();
    }

    private void SetColor()
    {
        switch (state)
        {
            case LevelState.Open:
                Image.color = Open;
                break;
            case LevelState.Locked:
                Image.color = Locked;
                break;
            case LevelState.Done:
                Image.color = Done;
                break;
            case LevelState.Premium:
                Image.color = Premium;
                break;
        }
    }

    #region UI

    public void OnClick()
    {
        if (state.Equals(LevelState.Locked))
            Menu.Instance.Pop("This level is locked", "You need to complete the mission before.");
        else if (state.Equals(LevelState.Premium) && !Player.Instance.GetModel().Premium)
            Menu.Instance.Pop("This level is locked", "Go the premium tab in the menu to unlock the premium levels.");
        else
            GameManager.Instance.StartGame(id);
    }

    #endregion
}
