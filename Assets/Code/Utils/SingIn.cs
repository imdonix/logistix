using System;
using UnityEngine;

namespace Utils
{
    public class SingIn : Singleton<SingIn>
    {
        public void InvokeLoginScreen(Action<string> token, Action<string> error)
        {
            token.Invoke(SystemInfo.deviceUniqueIdentifier);
        }

    }
}