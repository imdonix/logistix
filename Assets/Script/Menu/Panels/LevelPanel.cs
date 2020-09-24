using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MenuPanel
{
    public const float WIDTH = 900;
    public const float HEIGHT = 300;
    

    [Header("Properties")]
    [SerializeField] private RowUIComponent RowComp;
    [SerializeField] private DependencyUIComponent DependecyComp;

    [Header("Objects")]
    [SerializeField] private RectTransform Panel;
    [SerializeField] private RectTransform Dependencies;
    [SerializeField] private RectTransform LevelStart;


    private List<RowUIComponent> rows;
    private List<DependencyUIComponent> dependecies;


    protected override void OnOpen()
    {
        ShowTree();
    }

    public override void Back()
    {
        Menu.Instance.Swich(Menu.Instance.Main);
    }

    private void ShowTree()
    {
        Clear();
        ShowLevels();
        ShowDep();
    }

    private void ShowLevels()
    {
        int i = 0;
        foreach (var row in GameManager.Instance.GetMap())
            rows.Add(GenerateRow(row, i++));
    }

    private void ShowDep()
    {
        for (int i = 1; i < rows.Count; i++)
            for (int j = 0; j < rows[i].Levels.Length; j++)
                ShowDepFor(rows[i].Levels[j], i - 1);
    }

    private void ShowDepFor(LevelUIComponent level, int row)
    {
        int[] deps = level.GetDeps();
        for (int i = 0; i < deps.Length; i++)
        {
            Vector2 delta = (Vector2.up * 20);
            Vector2 start = FindLoc(deps[i], row) - delta;
            Vector2 end = (Vector2)level.Rect.position + delta;
            DependencyUIComponent comp = Instantiate(DependecyComp);
            comp.Rect.SetParent(Dependencies);
            comp.Show(start, end);
            dependecies.Add(comp);
        }
    }

    private Vector2 FindLoc(int id, int row) 
    {
        foreach (var level in rows[row].Levels)
            if (level.GetID() == id)
                return level.Rect.position;
        throw new IllegalLevelMapExeption(id);
    }

    private RowUIComponent GenerateRow(LevelRowModel row, int index)
    {
        var comp = Instantiate(RowComp);
        comp.gameObject.name = $"{index + 1}";
        comp.Rect.SetParent(Panel);
        comp.Rect.localScale = Vector3.one;
        comp.Rect.anchoredPosition = 
            new Vector2(0, LevelStart.localPosition.y - HEIGHT * index);
        comp.LoadData(row.Levels);
        return comp;
    }

    private void Clear()
    {
        if(dependecies != null)
            foreach (var dep in dependecies) Destroy(dep.gameObject);
        if (rows != null)
            foreach (var row in rows) Destroy(row.gameObject);
        rows = new List<RowUIComponent>();
        dependecies = new List<DependencyUIComponent>();
    }
}
