using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

	private BoxCollider2D colider;

	#region UNITY

	private void Awake()
	{
		colider = GetComponent<BoxCollider2D>();
	}

	#endregion


	public float GetSize()
	{
		return colider.size.x;
	}

}
