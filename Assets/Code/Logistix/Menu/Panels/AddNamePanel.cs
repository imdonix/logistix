using Logistix.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class AddNamePanel : MenuPanel
    {

        [Header("Menu Elements")]
        [SerializeField] private TMP_InputField NameField;
        [SerializeField] private Button Button;
        [SerializeField] private Text Error;

        protected override void OnOpen() { }

        protected override void OnClose() { }

        public override void Back()
        {
            Menu.Instance.Pop("Info", "You need to set a nickname for your account");
        }

        #region UI

        public void OnEnter()
        {
            Button.gameObject.SetActive(false);

            Player.SetName(NameField.text,
            error =>
            {
                Error.text = error;
                Button.gameObject.SetActive(true);
            });
        }

        #endregion

    }
}