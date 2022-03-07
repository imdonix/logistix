using Logistix.Core;
using System.Collections;
using UnityEngine;

namespace Logistix.Items
{
    public class Gun : Item
    {
        private const float TRIGGER = 10;

        [SerializeField] private Bullet bullet;

        [SerializeField] private Vector2 pivot;
        [SerializeField] private int ammo;
        [SerializeField] private float firerate;
        [SerializeField] private bool auto;
        [SerializeField] private float recoil;

        private float cooldown;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            cooldown += Time.deltaTime;
            if (cooldown > 0.25f)
                if (collision.relativeVelocity.magnitude > TRIGGER)
                {
                    StartCoroutine(ShotBullet());
                    cooldown = 0;
                }
        }

        private IEnumerator ShotBullet()
        {
            while (ammo-- > 0)
            {
                Bullet bull = Instantiate(bullet, GetBulletPos(), transform.rotation);
                bull.Fire();
                Recoil();

                if (!auto) break;
                yield return new WaitForSeconds(1 / firerate);
            }

        }

        private void Recoil()
        {
            GetRigidbody2D().AddForce((transform.position - GetBulletPos()).normalized * recoil);
        }

        private Vector3 GetBulletPos()
        {
            return transform.position + transform.rotation * pivot;
        }

        #region GIZMO

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(GetBulletPos(), 0.025f);
        }

        #endregion
    }
}