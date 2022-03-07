using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private int ShowsFor;
    [SerializeField] private int Cooldown;
    [SerializeField] private Text Header;
    [SerializeField] private Text Content;

    private RectTransform trans;
    private bool locked;
    private List<(string, string)> pops;

    #region UNITY
    
    private void Awake()
    {
        gameObject.SetActive(true);
        trans = GetComponent<RectTransform>();
        locked = false;
        pops = new List<(string, string)>();
        ShowPanel(false);
    }

    private void Update()
    {
        if (!locked && pops.Count > 0)
            Pop();
    }

    #endregion

    #region PUBLIC

    public void Pop(string header, string message)
    {
        pops.Add((header, message));
    }

    #endregion

    #region PRIVATE

    private void Pop()
    {
        var data = pops[0];
        pops.RemoveAt(0);
        locked = true;
        StartCoroutine(Show(data.Item1, data.Item2));

        IEnumerator Show(string header, string message)
        {
            Header.text = header;
            Content.text = message;
            this.ShowPanel(true);

            yield return new WaitForSeconds(ShowsFor);

            this.ShowPanel(false);

            yield return new WaitForSeconds(Cooldown);

            locked = false;
        }
    }

    private void ShowPanel(bool visible)
    {
        trans.anchoredPosition = Vector2.up * trans.sizeDelta.y * (visible ? -1 : 1);
    }

    #endregion

}
