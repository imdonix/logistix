using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    private const float TRIGGER = 2;

    [SerializeField] private Bullet bullet;

    [SerializeField] private Vector2 pivot;
    [SerializeField] private int ammo;
    [SerializeField] private bool auto;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude > TRIGGER)
            ShotBullet();
    }

    private void ShotBullet()
    {
        while (ammo > 0)
        {
            Bullet bull = Instantiate(bullet, transform.position + (Vector3)pivot, transform.rotation);
            bull.Fire();

            if (!auto)
                break;
        }

    }

    #region GIZMO

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (Vector3)pivot, 0.025f);
    }

    #endregion
}
