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
            FB.ActivateApp();
        else
            Debug.LogError("Failed to Initialize the Facebook SDK");
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log(isGameShown);
    }

    public void InvokeLoginScreen(Action<string> email, Action<string> error)
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);

        void AuthCallback(ILoginResult result)
        {
            if (FB.IsLoggedIn)
            {
                // AccessToken class will have session details
                var aToken = AccessToken.CurrentAccessToken;
                // Print current access token's User ID
                Debug.Log(aToken.UserId);
                // Print current access token's granted permissions
                foreach (string perm in aToken.Permissions)
                {
                    Debug.Log(perm);
                }

                email.Invoke(aToken.UserId);
            }
            else
            {
                Debug.Log("User cancelled login");
                error.Invoke("User cancelled login");
            }
        }
    }


}
