using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Box : MonoBehaviour
{

    private float WATER_LEVEL = -1;

    [Header("Data")]
    [SerializeField] public int ID;
    [SerializeField] public int Score;

    private Rigidbody2D Rigidbody;
    private Collider2D Colider;

    #region UNITY

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Colider = GetComponent<Collider2D>();
    }

    #endregion

    public Rigidbody2D GetRigidbody()
    {
        return Rigidbody;
    }

    public Collider2D GetColider()
    {
        return Colider;
    }

    public Vector2 GetSize()
    {
        return Colider.bounds.size;
    }

    /// <summary>
    /// Return is the box is lost
    /// </summary>
    public virtual bool IsLost()
    {
        return transform.position.y < WATER_LEVEL;
    }

    /// <summary>
    /// Return is the box position
    /// </summary>
    public virtual Vector2 GetPosition()
    {
        return transform.position;
    }
}
