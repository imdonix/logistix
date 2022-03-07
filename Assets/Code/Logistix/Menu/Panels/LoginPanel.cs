using Logistix;
using Logistix.Core;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class LoginPanel : MenuPanel
    {
        [Header("Components")]
        [SerializeField] private GameObject Panel;

        protected override void OnOpen()
        {
            Player.LoadUser();
            Panel.SetActive(false);

            SingIn.InvokeLoginScreen(StartRequestingPlayer,
            err =>
            {
                Debug.LogError($"Login with google is failed: {err}");
                Panel.SetActive(true);
            });
        }

        protected override void OnClose() { }

        private void StartRequestingPlayer(string userID)
        {
            Panel.SetActive(false);

            if (GameManager.Instance.IsDebugMode())
            {
                string rand = $"{userID}-{Random.Range(int.MinValue, int.MaxValue)}";
                Player.Load(rand, OnUnsuccesfull);
            }
            else
            {
                Player.Load(userID, OnUnsuccesfull);
            }
        }

        private void OnUnsuccesfull()
        {
            Menu.Instance.Pop("Network error!", "Try again later.");
            Panel.SetActive(true);
        }

    }
}