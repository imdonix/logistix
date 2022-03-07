using System;
using UnityEngine;

namespace Utils
{
    public static class SingIn
    {
        public static void InvokeLoginScreen(Action<string> token, Action<string> error)
        {
            token.Invoke(SystemInfo.deviceUniqueIdentifier);
        }

    }
}