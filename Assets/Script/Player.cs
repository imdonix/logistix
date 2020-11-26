using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class Player : Singleton<Player>
{
    private const string ID_KEY = "key";

    [Header("View")]
    [SerializeField] private PlayerModel Model;
    [SerializeField] private string UserID;
    [SerializeField] private bool IsLoggedIn;

    #region UNITY

    protected override void Awake()
    {
        base.Awake();
        LoadUser();
    }

    #endregion

    #region PUBLIC

    public bool IsLogged()
    {
        return IsLoggedIn;
    }

    public string GetUserID()
    {
        return UserID;
    }

    public PlayerModel GetModel()
    {
        return Model;
    }

    /// <summary>
    /// Load the palyer data from email
    /// </summary>
    /// <param name="email">email</param>
    /// <param name="error">callback when error</param>
    public void Load(string email, Action error)
    {
        PlayerPrefs.SetString(ID_KEY, email);
        LoadUser();

        LogisticAPI.Instance.GetPlayer(model => Refresh(model), 
        err =>
        {
            Debug.LogError(err);
            error.Invoke();
        });
    }

    public void SetName(string name, Action<string> error)
    {
        string errorText;
        if (IsNameValid(name, out errorText))
            LogisticAPI.Instance.SetName(name,
            model => Refresh(model),
            err => error.Invoke(err));
        else
            error.Invoke(errorText);
    }

    /// <summary>
    /// Refresh the player model.
    /// </summary>
    /// <param name="model"></param>
    public void Refresh(PlayerModel model)
    {
        if(!ReferenceEquals(Model, null))
            if (model.Premium && !Model.Premium)
                OnPremiumAccountActivated();

        Model = model;

        if (ReferenceEquals(model.Name, null))
            Menu.Instance.Swich(Menu.Instance.AddNamePanel);
        else
            Menu.Instance.Swich(Menu.Instance.Main);
    }

    /// <summary>
    /// Call this when you have no new playermodel to Refresh
    /// </summary>
    public void Refresh()
    {
        Refresh(Model);
    }

    public int GetExtraLife()
    {
        return 0;
    }

    #endregion

    private void LoadUser()
    {
        IsLoggedIn = PlayerPrefs.HasKey(ID_KEY);
        if (IsLoggedIn)
            UserID = PlayerPrefs.GetString(ID_KEY);
    }

    private bool IsNameValid(string name, out string error)
    {
        if (name.Length < 4) 
        { error = "short"; return false; }
        if (name.Length > 8)
        { error = "long"; return false; }
        if (!Regex.IsMatch(name, "^[a-zA-Z]*$"))
        { error = "special characters"; return false; }
        error = "ok";
        return true;
    }

    private void OnPremiumAccountActivated()
    {
        Menu.Instance.Pop("Premium account", "Congratulation! Your account is premium now.");
    }
}
