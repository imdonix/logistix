using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class AddNamePanel : MenuPanel
{

    [Header("Menu Elements")]
    [SerializeField] private InputField NameField;
    [SerializeField] private Button Button;
    [SerializeField] private Text Error;

    protected override void OnOpen()
    {}

    public override void Back()
    { 
        Menu.Instance.Pop("Info", "You need to set a nickname for your account"); 
    }

    #region UI

    public void OnEnter()
    {
        Button.gameObject.SetActive(false);

        Player.Instance.SetName(NameField.text,
        error =>
        {
            Error.text = error;
            Button.gameObject.SetActive(true);
        });
    }

    #endregion

}
