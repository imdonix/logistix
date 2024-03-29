﻿using Logistix;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NetworkIndicator : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private float Speed;
        [SerializeField] private Image LoadingImage;


        void Update()
        {
            bool pending = GameManager.Instance.API.IsRequestPendig();
            gameObject.SetActive(pending);
            if (pending)
                LoadingImage.transform.Rotate(Vector3.forward * Time.deltaTime * Speed);
        }
    }
}

