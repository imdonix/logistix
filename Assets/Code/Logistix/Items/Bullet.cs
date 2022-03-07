using Logistix.Core;
using UnityEngine;

namespace Logistix.Items
{
    public class Bullet : Item
    {
        [SerializeField] private GameObject fx;
        [SerializeField] private float force;
        [SerializeField] private float explosion;
        [SerializeField] private float size;

        #region UNITY

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Instantiate(fx, transform.position, Quaternion.identity).transform.localScale *= size;
            Item.ItemDestroy(this);
        }

        #endregion


        #region PUBLIC
        public void Fire()
        {
            GetRigidbody2D().AddForce((transform.rotation * Vector2.right) * force);
        }

        #endregion
    }

}
