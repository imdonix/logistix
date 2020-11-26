using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : Singleton<Menu>
{
    [Header("Panels")]
    [SerializeField] public MenuPanel Login;
    [SerializeField] public MenuPanel AddNamePanel;
    [SerializeField] public MenuPanel Main;
    [SerializeField] public MenuPanel Levels;
    [SerializeField] public MenuPanel InGame;
    [SerializeField] public MenuPanel EndGame;
    [SerializeField] public MenuPanel Premium;
    [SerializeField] public MenuPanel Upgrade;

    [Header("Overlays")]
    [SerializeField] private MenuPanel Logo;
    [SerializeField] public MenuPanel SoundSettings;

    [Header("Utils")]
    [SerializeField] private Popup Popup;

    private List<MenuPanel> Panels;
    private MenuPanel Opened;

    #region UNITY

    protected override void Awake()
    {
        base.Awake();
        LoadPanels();
        EnablePopUp();
    }

    private void Start()
    {
        InputHandler.Instance.AddBackHandler(OnBack);
    }

    #endregion

    /// <summary>
    /// Open the given panel and close the oppened one.
    /// </summary>
    /// <param name="panel">Menu.Instance.<PANELNAME></param>
    public void Swich(MenuPanel panel)
    {
        ClosePanels();
        panel.Show();
        Opened = panel;
    }

    public void Pop(string header, string message)
    {
        Popup.Pop(header, message);
    }

    private void ClosePanels()
    {
        foreach (MenuPanel panel in Panels)
            panel.Hide();
    }

    private void LoadPanels()
    {
        Panels = new List<MenuPanel>(); 
        Panels.Add(Levels);
        Panels.Add(Login); 
        Panels.Add(AddNamePanel);      
        Panels.Add(Logo);
        Panels.Add(Main);
        Panels.Add(InGame);
        Panels.Add(EndGame);
        Panels.Add(Premium);
        Panels.Add(SoundSettings);
        Panels.Add(Upgrade);
    }

    private void EnablePopUp()
    {
        Popup.gameObject.SetActive(true);
    }

    private void OnBack()
    {
        Opened.Back();
    }
}
