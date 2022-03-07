using Logistix.Utils;
using System;
using UnityEngine;

namespace Logistix.Core
{
    public class Ship : MonoBehaviour
    {
        private const float L = 0.05f;

        [Header("Propeties")]
        [SerializeField] private float Speed;
        [SerializeField] private float Fog;
        [SerializeField] private float Slowzone;
        [SerializeField] private Vector3 FloatPosition;

        [SerializeField] public Sprite[] Towers;
        [SerializeField] public Sprite[] Sides;

        [Header("Dependecies")]
        [SerializeField] private SpriteRenderer Tower;
        [SerializeField] private SpriteRenderer Side;

        private BoxCollider2D colider;
        private ShipState state;
        private Game game;

        #region UNITY

        private void Awake()
        {
            colider = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            Arive();
        }

        private void Update()
        {
            Move();
        }

        #endregion

        #region PUBLIC

        public float GetSize()
        {
            return colider.size.x;
        }

        public bool IsReady()
        {
            return state == ShipState.Ready;
        }

        public void Send()
        {
            state = ShipState.Leave;
        }

        public void Send(Game game)
        {
            this.game = game;
            Send();
        }


        #endregion

        #region PRIVATE

        private void Arive()
        {
            if (!ReferenceEquals(game, null))
            { Destroy(game.gameObject); game = null; }

            transform.position = Vector3.left * Fog + FloatPosition;
            UpdateVisual();
            state = ShipState.Arive;
        }

        private void UpdateVisual()
        {
            Tower.sprite = Towers[Math.Min(Towers.Length - 1, ShipUpgrade.Instance.GetUpgrade(Upgrade.Tower))];
            Side.sprite = Sides[Math.Min(Sides.Length - 1, ShipUpgrade.Instance.GetUpgrade(Upgrade.Life))];
        }

        private void Move()
        {
            Vector3 force = Vector3.right * Speed * Time.deltaTime;
            float abs = Mathf.Abs(transform.position.x);

            if (state != ShipState.Ready)
            {
                if (abs < Slowzone)
                    force *= Mathf.Clamp(abs / Slowzone, 0.35f, 1);

                transform.position += force;

                if (state == ShipState.Arive && abs < L)
                    state = ShipState.Ready;
                else if (state == ShipState.Leave && abs > Fog)
                    Arive();
            }
        }

        #endregion

    }
}