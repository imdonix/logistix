﻿using System;
using UnityEngine;
using Utils;

namespace Logistix.Core
{
    public static class ShipUpgrade
    {
        private const string domain = "upgrade";
        private const int Base = 50;

        #region PUBLIC

        public static int GetUpgradeMax(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.Tower: return GameManager.Instance.GetShipTemplate().Towers.Length - 1;
                case Upgrade.Body:
                    LevelMap map = GameManager.Instance.GetMap();
                    if (!ReferenceEquals(map, null))
                        return map.GetDepth() - 2;
                    else
                        return 0;
                case Upgrade.Life: return GameManager.Instance.GetShipTemplate().Sides.Length - 1;
            }
            return 0;
        }

        public static int GetUpgrade(Upgrade upgrade)
        {
            return PlayerPrefs.GetInt(GetKey(upgrade), 0);
        }

        public static (int, int) GetNextLevelCost(Upgrade upgrade)
        {
            int i = GetUpgrade(upgrade) + 1;
            return GetCost(upgrade, i);
        }

        public static (int, int) GetSpent()
        {
            (int, int) sum = (0, 0);
            foreach (Upgrade up in Enum.GetValues(typeof(Upgrade)))
            {
                int ups = GetUpgrade(up);
                for (int i = 1; i <= ups; i++)
                {
                    (int, int) add = GetCost(up, i);
                    sum.Item1 += add.Item1;
                    sum.Item2 += add.Item2;
                }
            }
            return sum;
        }

        public static void UpgradeOne(Upgrade upgrade)
        {
            PlayerPrefs.SetInt(GetKey(upgrade), GetUpgrade(upgrade) + 1);
            PlayerPrefs.Save();
        }

        public static int GetExtraLife()
        {
            return GetUpgrade(Upgrade.Life);
        }

        public static int GetExtraMultiplier()
        {
            return GetUpgrade(Upgrade.Tower);
        }

        public static void ResetAll()
        {
            foreach (Upgrade up in Enum.GetValues(typeof(Upgrade)))
                PlayerPrefs.DeleteKey(GetKey(up));
            PlayerPrefs.Save();
        }

        #endregion

        private static (int, int) GetCost(Upgrade upgrade, int level)
        {
            switch (upgrade)
            {
                case Upgrade.Tower: return CalculateNextLevelConst(level, 50, 30);
                case Upgrade.Body: return CalculateNextLevelConst(level, 30, 75);
                case Upgrade.Life: return CalculateNextLevelConst(level, 300, 300);
            }
            return (0, 0);
        }

        private static (int, int) CalculateNextLevelConst(int level, float wood, float iron)
        {
            float expOf(int index) { return Mathf.Pow(index, 1.15f); }

            (float, float) res = (Base + wood, Base + iron);
            float exp; int i;
            for (i = 0, exp = expOf(i); i < level; i++, exp = expOf(i))
            {
                res.Item1 += wood * exp;
                res.Item2 += iron * exp;
            }
            return (Mathf.RoundToInt(res.Item1), Mathf.RoundToInt(res.Item2));
        }

        private static string GetKey(Upgrade upgrade)
        {
            switch (upgrade)
            {
                case Upgrade.Tower: return domain + "_tower";
                case Upgrade.Body: return domain + "body";
                case Upgrade.Life: return domain + "_life";
            }
            return string.Empty;
        }

    }
}
