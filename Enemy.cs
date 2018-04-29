using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	//Enemy stat variables
	public float hp;	//enemy hit points
	public float dmg;	//enemy direct contact damage
	public float speed; //enemy movement speed

	public int powerlevel;

	// Use this for initialization
	void Start () {
		hp = 1;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//destroy both projectiles
		if (col.gameObject.tag == "shot") {

			int pID = col.gameObject.GetComponent<Shooting> ().getPlayer ();
			GameObject[] playerList = GameObject.FindGameObjectsWithTag ("Player");

			for (int i = 0; i < playerList.Length; i++) {


				if (playerList [i].GetComponent<Player> ().playerID == pID) {
					playerList [i].GetComponent<Player> ().score += 1;
					Destroy (gameObject);
					break;
				}
			}


		}
	}

}
