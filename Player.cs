using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	public int speed;

	//player shot variables
	public  GameObject shot;//bullet game object, can be changed from editor	
	public float shotSpeed;	//shot speed
	public float shotRange;	//shot life time in frames
	public float interval;	//interval of fire, this value is the interval between shots, smaller interval = more shots/sec

	public float nextFire; //time for next shot
	public float fireRate; //interval of time between shots

	public int playerID;
	public int score;
	public int hp;
    public Cannon cannon;
    [SyncVar]
    public string myA, myB, myC;
	// Use this for initialization
	void Start () {
		speed = 2;
		nextFire = Time.time;
		fireRate = 1;
		hp = 3;
		score = 0;
		playerID = 1;
	}
	
	// Update is called once per frame
	void Update () {
		newMovement ();
		fireShot ();
	}

	void newMovement(){
		var move = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}


    //[ClientRpc]
    public void updateMatrices(int i, int j, int k, string sA, string sB, string sC)
    {
        cannon = new Cannon();
        Debug.Log("in rpc");
        if (!isServer)
        {
            //CmdSetMatrices("aa", "bb", "cc");
            return;
        }
        Debug.Log("server");
        Debug.Log(sA);
        Debug.Log(sB);
        Debug.Log(sC);
        int[,] mA = cannon.stringToMatrix(sA, 4);
        int[,] mB = cannon.stringToMatrix(sB, cannon.size);
        int[,] mC = cannon.stringToMatrix(sC, cannon.size);
        var m = (i + j + k) % cannon.root_p;
        int[,] sliceA = cannon.sliceMatrix(mA, i, m);
        int[,] sliceB = cannon.sliceMatrix(mB, m, j);
        int[,] product = cannon.MatrixProduct(sliceA, sliceB, cannon.bsize);
        cannon.addSlice(mC, product, i, j);
        mA = cannon.ShiftLeft(mA, i);
        mB = cannon.ShiftUp(mB, j);
        //string[] results = { newSA, newSB, newSC };
        //return results;
        //return cannon.matrixToString(mA, cannon.size);
        setMatrices(cannon.matrixToString(mA, cannon.size), cannon.matrixToString(mB, cannon.size), cannon.matrixToString(mC, cannon.size));
    }

    //[Command]
    public void setMatrices(string A, string B, string C)
    {
        //if (isServer)
        //{
        //    Debug.Log(1232132132131);
        //}
        //else {
        //    Debug.Log("cmd");
        //}
        myA = A;
        myB = B;
        myC = C;
    }


    void fireShot(){

		if (nextFire < Time.time ) {
			//shooting in all cases follows this pattern (may be subject to change):
			if (Input.GetKey ("right")) {

				nextFire = Time.time + fireRate;
				//get player position and store in vector v 
				Vector3 v = transform.position;
				//add a small value to the vector to avoid bullet/player collision
				v.x += 0.5f;
				//instansiate bullet at location v
				GameObject s = (GameObject)Instantiate (shot, v, Quaternion.identity);
				//get the shot behavior script
				Shooting ss = s.GetComponent<Shooting> ();
				//set parameters to the shot via the script
				ss.setParam (shotRange, 5, shotSpeed, 0,playerID);
			} else if (Input.GetKey ("down")) {
				nextFire = Time.time + fireRate;
				Vector3 v = transform.position;
				v.y -= 0.5f;
				GameObject s = (GameObject)Instantiate (shot, v, Quaternion.identity);
				Shooting ss = s.GetComponent<Shooting> ();
				ss.setParam (shotRange, 5, shotSpeed,1, playerID);
			
			} else if (Input.GetKey ("left")) {
				nextFire = Time.time + fireRate;
				Vector3 v = transform.position;
				v.x -= 0.5f;
				GameObject s = (GameObject)Instantiate (shot, v, Quaternion.identity);
				Shooting ss = s.GetComponent<Shooting> ();
				ss.setParam (shotRange, 5, shotSpeed, 2,playerID);
			
			} else if (Input.GetKey ("up")) {
				nextFire = Time.time + fireRate;
				Vector3 v = transform.position;
				v.y += 0.5f;
				GameObject s = (GameObject)Instantiate (shot, v, Quaternion.identity);
				Shooting ss = s.GetComponent<Shooting> ();
				ss.setParam (shotRange, 5, shotSpeed, 3,playerID);
			
			}
	


		}

	}
}
