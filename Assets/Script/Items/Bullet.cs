using System.Xml.Serialization;
using UnityEngine;

public class Bullet : Item
{
    [SerializeField] private float force;
    [SerializeField] private float explosion;

    #region UNITY

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.SpawnExplosion(collision.transform.position, explosion);
        Destroy(gameObject);
    }

    #endregion


    #region PUBLIC
    public void Fire()
    {
        GetRigidbody2D().AddForce((transform.rotation * Vector2.right) * force);
    }

    #endregion



}

