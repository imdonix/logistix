using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SingIn : Singleton<SingIn>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void InvokeLoginScreen(Action<string> token, Action<string> error)
    {
        token.Invoke("TESTID");
    }


}
