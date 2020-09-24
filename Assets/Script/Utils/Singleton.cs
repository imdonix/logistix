using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (ReferenceEquals(Instance, null)) 
        {
            Instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

}
