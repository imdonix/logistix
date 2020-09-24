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
        if(model.IsPremiumOnly)
        {
            state = LevelState.Premium;
        }
        else if (map.IsDone(id))
        {
            state = LevelState.Done;
        }
        else
        {
            if (map.IsUnlocked(id))
                state = LevelState.Open;
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
        switch (state)
        {

            case LevelState.Locked:
                Menu.Instance.Pop("Locked", "This level is locked!");
                break;
            case LevelState.Done:
            case LevelState.Premium:
            case LevelState.Open:
                GameManager.Instance.StartGame(id);
                break;
        }
    }

    #endregion
}
