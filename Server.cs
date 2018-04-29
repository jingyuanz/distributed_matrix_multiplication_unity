using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class Server : NetworkManager
{
    public List<GameObject> players;
    public const int size = 4;
    public const int bsize = 2;
    public const int num_p = 4;
    public int root_p = Convert.ToInt16(Math.Sqrt(num_p));
    //string sA;
    //string sB;
    //string sC;
    Cannon cannon;
    int[,] array;
    public void cannonEnemySpawn()
    {
        //Debug.Log("here");
        //if (!isServer) { return; }
        Debug.Log("in");
        cannon = new Cannon();
        array = new int[,] {
            {5,2,6,1},
            {5,2,4,2},
            {1,1,8,1},
            {1,1,8,1},

        };
        int[,] A = array;
        int[,] B = (int[,])A.Clone();
        int[,] D = (int[,])A.Clone();
        int[,] E = (int[,])A.Clone();

        Distribute(A, B);

        //printMatrix(C, Convert.ToInt16(Math.Sqrt(A.Length)));

        int[,] localC = cannon.MatrixProduct(D, E, size);
        cannon.printMatrix(localC, size);
    }
    

    public void Distribute(int[,] A, int[,] B)
    {
        int[,] C = new int[size, size];
        string sC = cannon.matrixToString(C, size);
        //for (int i = 0; i < C.Length; i++)
        //C[i] = new int[A.Length];
        for (int i = 0; i < root_p; i++)
        {
            for (int j = 0; j < i; j++)
            {
                A = cannon.ShiftLeft(A, i);
            }
        }
        string sA = cannon.matrixToString(A, size);
        Debug.Log(sA);

        for (int i = 0; i < root_p; i++)
        {
            for (int j = 0; j < i; j++)
                B = cannon.ShiftUp(B, i);
        }

        string sB = cannon.matrixToString(B, size);
        Debug.Log(sB);

        Server server = GameObject.Find("Server").GetComponent<Server>();
        List<GameObject> players = server.players;
        for (int k = 0; k < root_p; k++)
        {
            for (int i = 0; i < root_p; i++)
            {
                for (int j = 0; j < root_p; j++)
                {
                    Debug.Log(i * root_p + j);
                    Debug.Log(k.ToString() + ' ' + i.ToString() + ' ' + j.ToString());
                    GameObject p = players[i * root_p + j];
                    Player psp = p.GetComponent<Player>();
                    Debug.Log("sent rpc");
                    psp.updateMatrices(i, j, k, sA, sB, sC);
                    sA = psp.myA;
                    sB = psp.myB;
                    sC = psp.myC;
                    Debug.Log(sA);
                    Debug.Log(sB);
                    Debug.Log(sC);

                }

            }
            //Debug.Log(acc);
        };
        C = cannon.stringToMatrix(sC, size);
        cannon.printMatrix(C, size);
    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players.Add(player);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        Debug.Log(players.Count);
        if (players.Count == 4)
        {

            Debug.Log("aaaaaaaaaa");
            //GameObject spawnerObject = (GameObject)Instantiate(Resources.Load("Spawner"));
            //Spawn spn = spawnerObject.GetComponent<Spawn>();
            cannonEnemySpawn();
        }
    }


}
