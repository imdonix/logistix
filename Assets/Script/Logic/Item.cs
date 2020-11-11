using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{

    private Rigidbody2D Rigid;
    private Collider2D Collider;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Collider = GetComponent<Collider2D>();
    }


    #region PUBLIC

    public virtual bool IsLost()
    {
        return transform.position.y < Game.WATER_LEVEL;
    }

    #endregion

}
