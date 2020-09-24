using System;
using UnityEngine;

public abstract class MenuPanel : MonoBehaviour
{

    [Header("Overlays")]
    [SerializeField] private MenuPanel[] Overlays;

    public void Show()
    {
        foreach (MenuPanel panel in Overlays) 
            panel.Show();
        gameObject.SetActive(true);
        OnOpen();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Back() { }

    protected abstract void OnOpen();


}
