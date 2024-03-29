﻿using System.Collections.Generic;
using UnityEngine;


namespace Logistix.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour
    {

        private static LinkedList<Item> items = new LinkedList<Item>();

        private Rigidbody2D Rigid;
        private Collider2D Collider;

        private void Awake()
        {
            Rigid = GetComponent<Rigidbody2D>();
            Collider = GetComponent<Collider2D>();
            items.AddFirst(this);
        }

        protected Rigidbody2D GetRigidbody2D()
        {
            return Rigid;
        }


        #region PUBLIC

        public virtual bool IsLost()
        {
            return transform.position.y < Game.WATER_LEVEL;
        }

        #endregion

        #region STATIC
        public static void Clear()
        {
            foreach (Item item in items)
                if (item.gameObject != null)
                    Destroy(item.gameObject);
            items.Clear();
        }

        public static void ItemDestroy(Item item)
        {
            if (item.gameObject != null)
                Destroy(item.gameObject);
            items.Remove(item);
        }

        #endregion

    }
}