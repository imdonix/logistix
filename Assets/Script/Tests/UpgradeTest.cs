using System;
using UnityEngine;

public class UpgradeTest : MonoBehaviour
{
    void Start()
    {
        ShipUpgrade.Instance.ResetAll();
        foreach (Upgrade up in Enum.GetValues(typeof(Upgrade)))
        {
            for (int i = 0; i < 2; i++) 
            {
                Debug.Log($"{up} - {ShipUpgrade.Instance.GetUpgrade(up)} - next: {ShipUpgrade.Instance.GetNextLevelCost(up)}");
                ShipUpgrade.Instance.UpgradeOne(up);
            }
        }

        Debug.Log(ShipUpgrade.Instance.GetSpent());
    }
}
