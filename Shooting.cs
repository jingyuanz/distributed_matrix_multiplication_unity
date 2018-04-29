using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
	private  float damage;		//projectile damage
	private float range;		//projectile range
	private float speed;		//projectile speed
	private float rangeCounter; 
	private int direction;		//projectile movement direction
	private int playerID; // player who owns the shot
	// Use this for initialization
	void Start () {
		rangeCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (direction == 0) 
			//GetComponent<Rigidbody2D>().AddForce (gameObject.transform.right * speed);
			transform.Translate(new Vector3(1,0) * Time.deltaTime * speed);
		else if (direction == 1) 
			transform.Translate(new Vector3(0,-1) * Time.deltaTime * speed);
		else if (direction == 2) 
			transform.Translate(new Vector3(-1,0) * Time.deltaTime * speed);
		else if (direction == 3) 
			transform.Translate(new Vector3(0,1) * Time.deltaTime * speed);
		
	}

	public void setParam(float rng, float dmg,float spd,int dir, int pID){
		range = rng;
		damage = dmg;
		speed = spd;
		direction = dir;
		playerID = pID;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//destroy both projectiles
		if (col.gameObject.tag == "wall") {
			Destroy (gameObject);
		}

	}

	public int getPlayer(){
		return playerID;
	}
}
