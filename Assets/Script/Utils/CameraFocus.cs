using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    private const float L = 0.05f;

    private const float DEFAULT_SIZE = 5;
    private const float MARGIN = 1.5f;
    private const float SPEED = 3.4f;

    private Camera Camera;
    private float Target;

    #region UNITY

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        Target = DEFAULT_SIZE;
    }

    private void Update()
    {
        SetCameraSize();
        Translate();
    }

    #endregion

    private void SetCameraSize()
    {
        Ship ship = GameManager.Instance.GetShip();
        if (!ReferenceEquals(ship, null))
            Target = ship.GetSize() + MARGIN;
    }

    private void Translate()
    {
        if (Math.Abs(Camera.orthographicSize - Target) > L)
        {
            int dir = Target - Camera.orthographicSize > 0 ? 1 : -1;
            float delta = dir * SPEED * Time.deltaTime;
            Camera.orthographicSize += delta;
        }
    }
}
