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
        /*
        bool LoggedIn = Player.Instance.IsLogged();
        Panel.SetActive(!LoggedIn);

        if (LoggedIn)
            StartRequestingPlayer(Player.Instance.GetEmail());
        */
    }

    private void StartRequestingPlayer(string email)
    {
        Panel.SetActive(false);
        Test.text = $"Google sync was succesfull: {email}";
        Player.Instance.Load(email, OnUnsuccesfull);
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

}