using UnityEngine;

namespace Logistix.Core
{
    public abstract class Box : MonoBehaviour
    {

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

        public virtual Vector2 GetSize()
        {
            return Colider.bounds.size;
        }

        /// <summary>
        /// Return is the box is lost
        /// </summary>
        public virtual bool IsLost()
        {
            return transform.position.y < Game.WATER_LEVEL;
        }

        /// <summary>
        /// Return is the box position
        /// </summary>
        public virtual Vector2 GetPosition()
        {
            return transform.position;
        }

        /// <summary>
        /// Called when the crane eject the box
        /// </summary>
        public virtual void OnEject()
        { }

        public virtual void Destroy()
        {
            Destroy(gameObject, 1f);
        }
    }
}