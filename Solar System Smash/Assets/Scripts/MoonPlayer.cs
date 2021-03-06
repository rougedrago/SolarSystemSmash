﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoonPlayer : MonoBehaviour {
	
	private Rigidbody2D rb;
	private float speed;
	private bool isControlSwitched = false;
	private bool greenPlanet = false;
	private GameObject target;
    public static int cometCount = 0;	
	private Vector3 healthPos;
	private bool immune = false;
	private float timer;
	private bool controlSwitchState;
	public Slider healthBarSliderMoon;
    public SoundEffectPlayer soundEffectPlayer;

	// Use this for initialization
	void Start () {

		speed = 250;
		rb = GetComponent<Rigidbody2D>();
	    cometCount = 0;
		controlSwitchState = isControlSwitched;
        target = GameObject.FindGameObjectWithTag("Planet3");
	}

	void gainHealth()
	{
		if(healthBarSliderMoon.value > 0 && healthBarSliderMoon.value < healthBarSliderMoon.maxValue)
		{
			healthBarSliderMoon.value += 0.4f;
		}
	}

	void greenPlanetKill()
	{
		healthBarSliderMoon.value = 0.0f;
	}

	void bluePlanetDamage()
	{
		healthBarSliderMoon.value -= 0.2f;
	}

	void applyDamage()
	{
		if (!immune) {
            soundEffectPlayer.PlaySoundEffect("moon_comet_hit", 0.5f);
			healthBarSliderMoon.value -= 0.2f;
		}
	}

	void setImmune()
	{
		immune = true;
		isControlSwitched = false;
		timer = 10;
	}
	
	void Update () {

		if (immune && timer > 0) {

			timer -= Time.deltaTime;

		}
		if (timer <= 0) {

			immune = false;
			isControlSwitched = controlSwitchState;

		}

		if (healthBarSliderMoon.value <= 0) {

			// go to game over scene

			Debug.Log("Moon - Game Over");

		}

		if (greenPlanet && !immune) {

			rb.AddForce((target.transform.position - transform.position) * 40 * Time.deltaTime);

		}

		if (!isControlSwitched) {
			if (Input.GetKey (KeyCode.W)) {
				rb.AddForce (Vector2.up * speed * Time.deltaTime);
			}
		
			if (Input.GetKey (KeyCode.A)) {
				rb.AddForce (-1 * Vector2.right * speed * Time.deltaTime);
			}
		
			if (Input.GetKey (KeyCode.S)) {
				rb.AddForce (-1 * Vector2.up * speed * Time.deltaTime);
			}
		
			if (Input.GetKey (KeyCode.D)) {
				rb.AddForce (Vector2.right * speed * Time.deltaTime);
			}

		} else {
			if (Input.GetKey (KeyCode.S)) {
				rb.AddForce (Vector2.up * speed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.D)) {
				rb.AddForce (-1 * Vector2.right * speed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.W)) {
				rb.AddForce (-1 * Vector2.up * speed * Time.deltaTime);
			}
			
			if (Input.GetKey (KeyCode.A)) {
				rb.AddForce (Vector2.right * speed * Time.deltaTime);
			}
		}


	}

	void setGreenPlanet()
	{
		greenPlanet = !greenPlanet;
	}

	void switchControls()
	{
		isControlSwitched = !isControlSwitched;
		controlSwitchState = isControlSwitched;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Comet") {

		    if (cometCount < 5)
		    {
                col.gameObject.BroadcastMessage("setNearMoon");
			} 
		}

		if ((col.gameObject.name == "Projectile(Clone)" || col.gameObject.name == "Planet1Projectile(Clone)") && healthBarSliderMoon.value > 0) {


			applyDamage();
            Destroy(col.gameObject);
		}
	}
}
