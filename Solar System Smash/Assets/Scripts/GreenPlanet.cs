﻿using UnityEngine;
using System.Collections;

public class GreenPlanet : MonoBehaviour {

	private Rigidbody2D rb;
	private int health = 10;
	public static bool isActive = false;
	private int count = 1;
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();

	}
	
	void OnCollisionEnter2D(Collision2D col)		
	{
		if (col.gameObject.tag == "Moon") {
			GameObject.FindGameObjectWithTag ("Moon").BroadcastMessage("greenPlanetKill");
			col.gameObject.SetActive(false);
			
		}

		if (col.gameObject.name == "Projectile(Clone)") {
			
			health--;
			
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive && count == 1)
		{
			GameObject.FindGameObjectWithTag ("Moon").BroadcastMessage ("setGreenPlanet");
			count++;
		}

		if(health == 0)
		{
            //GameObject.Find("GameManager").
			Destroy(this.gameObject);
			GameObject.FindGameObjectWithTag ("Moon").BroadcastMessage("setGreenPlanet");
		}
		//rb.AddForce (Vector2.up * 20 * Time.deltaTime);
		
	}
	
}
