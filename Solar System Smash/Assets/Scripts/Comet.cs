﻿using UnityEngine;
using System.Collections;

public class Comet : MonoBehaviour
{
	public static bool nearMoon = false; 			// NISH
	public Transform target;						// NISH
	private int count = 1;							// NISH
	private Rigidbody2D rb;							// NISH

    public SpriteRenderer spriteRender;
    public Sprite[] spriteList = new Sprite[8];

    // Use this for initialization
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        int index = (int)Random.Range(1, 8);
        spriteRender.sprite = spriteList[index];

		rb = GetComponent<Rigidbody2D> ();			// NISH
    }

    // Update is called once per frame
    void Update()
    {
		if (!nearMoon) {
			
		} else {
			
			if(count == 1)
			{
				//offset = transform.position - target.transform.position;
				count++;
			}
			
			//rb.AddForce((target.transform.position - transform.position) * 100 * Time.deltaTime);
			
			//transform.position = target.transform.position + offset;
			transform.RotateAround (target.transform.position, Vector3.forward, 5);
			
		}
    }

    void OnBecameInvisible()
    {
        // TODO: decrement shared player health or something
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // TODO: if other was with left side of screen, decrement shared player health or something; destroy object
        //       if other was with the sun, just destroy object; increment something?
        //       if other was with the moon, decrement moon health, destroy object

        if (other.gameObject.tag == "LeftBoundary")
        {
            Debug.Log("Comet passed the sun.");
            // Decrement shared player health or something
            Destroy(this.gameObject); 
        }
        else if (other.gameObject.name == "Moon")
        {
            // Decrement moon health
            // Destroy(this.gameObject); 
        }
        else if (other.gameObject.tag != "Comet" && other.gameObject.tag != "Moon") 
        {
            Destroy(this.gameObject); 
        }
    }
}
