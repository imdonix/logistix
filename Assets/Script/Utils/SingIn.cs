using Facebook.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingIn : Singleton<SingIn>
{
    protected override void Awake()
    {
        base.Awake();

        if (!FB.IsInitialized)
            FB.Init(InitCallback, OnHideUnity);
        else
            FB.ActivateApp();
    }

    private void InitCallback()
    {
        if (FB.IsInitialized) 
        {
            FB.ActivateApp();
            ((LoginPanel)Menu.Instance.Login).AfterInitCompletetd();
        }
        else
            Debug.LogError("Failed to Initialize the Facebook SDK");
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log(isGameShown);
    }

    public void GetLoginStatus(Action<ILoginStatusResult> succesfull) 
    {
        FB.Android.RetrieveLoginStatus(res => succesfull.Invoke(res));
    }

    public void InvokeLoginScreen(Action<string> token, Action<string> error)
    {
        var perms = new List<string>() {"public_profile"};
        FB.LogInWithReadPermissions(perms, AuthCallback);

        void AuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
                token.Invoke(AccessToken.CurrentAccessToken.UserId);
            else
                error.Invoke("User cancelled login");
        }
    }


}
