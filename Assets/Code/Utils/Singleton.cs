﻿using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (ReferenceEquals(Instance, null))
            {
                Instance = gameObject.GetComponent<T>();
                if (ReferenceEquals(transform.parent, null))
                    DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

    }
}