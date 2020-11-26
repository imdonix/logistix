using UnityEditor;
using UnityEngine;

public class ResetPlayerPrefs
{
    [MenuItem("PlayerPrefs/Reset")]
    static void Reset()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}