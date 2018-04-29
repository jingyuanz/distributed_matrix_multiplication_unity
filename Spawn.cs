using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.Networking;
public class Spawn : NetworkBehaviour {
    public GameObject room;
	public  GameObject room2;
	public  GameObject room3;
	public  GameObject room4;
	public  GameObject room5;
	public  GameObject room6;
	public  GameObject room7;
	public  GameObject room8;


    private int roomCost;
	public  GameObject[] enemies;
	public int[ , ] array;
    private List<NetworkConnection> finishedConnections = new List<NetworkConnection>();
    public NetworkPlayer[] connections;
    
    // Use this for initialization
    void Start ()
    {
        Debug.Log("zz");

		Vector3 v = new Vector3 (0, 0, 1);

        //filling in the 2D array that decides map generation
        array = new int[,] {
            {5,2,6,1},
            {5,2,4,2},
            {1,1,8,1},
            {1,1,8,1},

        };
        //Instantiating rooms based on the 2D array 
        for (int i=0; i<4; i++) {
			
			v.x = 0;
			for (int j = 0; j < 4; j++) {
				if (array [i, j] == 1) {
					room = (GameObject)Instantiate (room, v, Quaternion.identity);
				} else if (array [i, j] == 2) {
					room2 = (GameObject)Instantiate (room2, v, Quaternion.identity);
				} else if (array [i, j] == 3) {
					room3 = (GameObject)Instantiate (room3, v, Quaternion.identity);
				} else if (array [i, j] == 4) {
					room4 = (GameObject)Instantiate (room4, v, Quaternion.identity);
				} else if (array [i, j] == 5) {
					room5 = (GameObject)Instantiate (room5, v, Quaternion.identity);
				} else if (array [i, j] == 6) {
					room6 = (GameObject)Instantiate (room6, v, Quaternion.identity);
				} else if (array [i, j] == 7) {
					room7 = (GameObject)Instantiate (room7, v, Quaternion.identity);
				} else {
					room8 = (GameObject)Instantiate (room8, v, Quaternion.identity);
				}

				enemySpawn (v,roomCost);


				v.x += room.GetComponent<Renderer> ().bounds.size.x * 5;
			}
			v.y -= room.GetComponent<Renderer> ().bounds.size.y * 5;
		}



	}
    

    

    private void enemySpawn(Vector3 v, int cost){
		int currentCost = cost;
		while (currentCost > 0 && currentCost != 0) {
			
			int num = (int)(Mathf.Floor (UnityEngine.Random.Range (0, currentCost)));
			GameObject enemy = (GameObject)Instantiate (enemies [num], v, Quaternion.identity);
			currentCost -= num;
		}
	}
    
    // Update is called once per frame
    void Update () {
		
	}
}
