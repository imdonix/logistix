﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LevelMap : IEnumerable<LevelRowModel>
{
    private LevelRowModel[] Rows;

    private LevelMap(){}

    #region PUBLIC

    public int CountLevels()
    {
        int c = 0;
        foreach (var row in Rows)
            c += row.Levels.Length;
        return c;
    }

    public bool IsDone(int id)
    {
        return Contain(Player.Instance.GetModel().CompletedLeves, id);
    }

    public bool IsUnlocked(int id)
    {
        (int, int[]) level = FindRowAndDependencyByID(id);
        if (Rows.Length < 2) return true;
        if (level.Item1 < 1) return true; // First row alway unlocked

        bool unlock = false;
        for (int i = 0; i < level.Item2.Length; i++)
            unlock = unlock || IsDone(level.Item2[i]);
        return unlock;
    }

    public LevelModel GetLevelByID(int id)
    {
        for (int i = 0; i < Rows.Length; i++)
            for (int j = 0; j < Rows[i].Levels.Length; j++)
                if (Rows[i].Levels[j].ID == id)
                    return Rows[i].Levels[j];
        throw new IllegalLevelMapExeption(id);
    }

    #endregion

    #region PRIVATE

    private (int, int[]) FindRowAndDependencyByID(int id)
    {
        for (int i = 0; i < Rows.Length; i++)
            for (int j = 0; j < Rows[i].Levels.Length; j++)
                if (Rows[i].Levels[j].ID == id)
                    return (i, Rows[i].Levels[j].Unlocks);
        throw new IllegalLevelMapExeption(id);
    }

    #endregion

    #region STATIC

    /// <summary>
    /// Create the LevelMap by all level.
    /// Throw IllegalLevelMapExeption.
    /// </summary>
    /// <param name="levelModels">Levels</param>
    /// <returns>The map.</returns>
    public static LevelMap Create(LevelRowModel[] levelModels)
    {
        LevelMap map = new LevelMap();
        map.Rows = levelModels;
        return map;
    }

    private static bool Contain(int[] arr, int item)
    {
        if (arr == null) return false;

        int i = 0;
        while (i < arr.Length && arr[i] != item)
            i++;

        return i < arr.Length;     
    }

    #endregion

    #region ENUMERATOR

    public IEnumerator<LevelRowModel> GetEnumerator()
    {
        foreach (var row in Rows)
            yield return row;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion
}
