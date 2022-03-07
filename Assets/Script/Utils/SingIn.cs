using System;

namespace Utils
{
    public class SingIn : Singleton<SingIn>
    {
        public void InvokeLoginScreen(Action<string> token, Action<string> error)
        {
            token.Invoke("TESTID"); //TODO
        }

    }
}