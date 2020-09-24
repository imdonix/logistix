﻿using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MenuPanel
{
    [Header("Components")]
    [SerializeField] private GameObject Panel;
    [SerializeField] private Text Test;

    [Header("DebugMode")]
    [SerializeField] private string DebugMail = string.Empty;

    protected override void OnOpen() 
    {
        Panel.SetActive(false);
    }

    private void StartRequestingPlayer(string userID)
    {
        Panel.SetActive(false);
        Test.text = $"Google sync was succesfull: {userID}";
        Player.Instance.Load(userID, OnUnsuccesfull);
    }

    private void OnUnsuccesfull()
    {
        Test.text = $"Cannot get the player from backend!";
        Panel.SetActive(true);
    }

    #region UI

    public void OnGoogleClick()
    {
        SingIn.Instance.InvokeLoginScreen(StartRequestingPlayer,
            err =>
            {
                Test.text = "Error while trying to use google login.";
                Debug.LogError($"Login with google is failed: {err}");
                Panel.SetActive(true);
            });
    }

    public void OnTestClick()
    {
        StartRequestingPlayer(DebugMail);
    }

    #endregion

    public void AfterInitCompletetd()
    {
        SingIn.Instance.GetLoginStatus(res =>
        {
            Panel.SetActive(res.Failed);
            if (!res.Failed)
                StartRequestingPlayer(res.AccessToken.UserId);
        });
    }

}