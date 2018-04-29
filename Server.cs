using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class Server : NetworkManager
{
    public List<GameObject> players = new List<GameObject>();
    public const int size = 4;
    public const int bsize = 2;
    public const int num_p = 4;
    public const int root_p = 2;
    public string sA;
    public string sB;
    public string sC;
    public int[,] finalMatrix;
    Cannon cannon;
    int[,] array;
    public bool waitingForResult = true;
    public int playerIndex = 0;
    public int ind_index = 0;

    public List<string> inds = new List<string>();
    public int i, j, k;
    private void Start()
    {
        cannonEnemySpawn();
    }
    public void cannonEnemySpawn()
    {
        //Debug.Log("here");
        //if (!isServer) { return; }
        //Debug.Log("in");
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
        int[,] C = new int[size, size];
        sC = cannon.matrixToString(C, size);
        //for (int i = 0; i < C.Length; i++)
        //C[i] = new int[A.Length];
        for (int i = 0; i < root_p; i++)
        {
            for (int j = 0; j < i; j++)
            {
                A = cannon.ShiftLeft(A, i);
            }
        }
        sA = cannon.matrixToString(A, size);
        //Debug.Log(sA);

        for (int i = 0; i < root_p; i++)
        {
            for (int j = 0; j < i; j++)
                B = cannon.ShiftUp(B, i);
        }

        sB = cannon.matrixToString(B, size);
        //Debug.Log(sB);
        generateInds();

        //printMatrix(C, Convert.ToInt16(Math.Sqrt(A.Length)));

        int[,] localC = cannon.MatrixProduct(D, E, size);
        cannon.printMatrix(localC, size);
    }



    public void generateInds()
    {
        //Server server = GameObject.Find("Server").GetComponent<Server>();
        for (int k = 0; k < root_p; k++)
        {
            for (int i = 0; i < root_p; i++)
            {
                for (int j = 0; j < root_p; j++)
                {
                    ////players = server.players;
                    ////Debug.Log(i * root_p + j);
                    string s = k.ToString() + " " + i.ToString() + " " + j.ToString();
                    inds.Add(s);
                    //GameObject p = players[i * root_p + j];
                    //Player psp = p.GetComponent<Player>();
                    ////Debug.Log("sent rpc");
                    ////Debug.Log(psp.hp);
                    //psp.RpcUpdateMatrices(i, j, k, sA, sB, sC);
                    

                }

            }
            //Debug.Log(acc);
        };
        //Debug.Log("final : ");
        //Debug.Log(sC);
        //int[,] C = cannon.stringToMatrix(sC, size);
        //cannon.printMatrix(C, size);
    }

    public void Distribute()
    {
        waitingForResult = true;
        string sind = inds[ind_index];
        string[] split_inds = sind.Split(' ');
        k = Convert.ToInt16(split_inds[0]);
        i = Convert.ToInt16(split_inds[1]);
        j = Convert.ToInt16(split_inds[2]);
        playerIndex = i * root_p + j;
        Debug.Log(k + ", " + i + ", " + j);
        Debug.Log(playerIndex);
        Debug.Log(players.Count);
        GameObject p = players[playerIndex];
        //p.GetComponent<Player>().RpcFake(playerIndex);
        p.GetComponent<Player>().RpcUpdateMatrices(i,j,k,sA, sB, sC);
        ind_index = ind_index + 1;
        
        //playerIndex = (playerIndex + 1) % players.Count;
    }

    
    void Update()
    {
        
        if (isNetworkActive && !waitingForResult)
        {

            //Debug.Log(ind_index+" "+inds.Count);
            if (ind_index == inds.Count)
            {
                finalMatrix = cannon.stringToMatrix(sC, size);
                cannon.printMatrix(finalMatrix, size);

            }
            else {
                Debug.Log("dis");
                Distribute();

            }
        }
    }

    public void FakeDistribute()
    {
        waitingForResult = true;
          

        GameObject p = players[playerIndex];
        p.GetComponent<Player>().RpcFake(playerIndex);
        //Debug.Log(playerIndex);
        playerIndex = (playerIndex + 1) % players.Count;
    }

    
    public void FakeResult(int s)
    {
        Debug.Log(s + "sssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss");
        waitingForResult = false;
    }



    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        players.Add(player);
        
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        if (players.Count == 4)
        {
            Debug.Log("aaaaaaaaaa");
            waitingForResult = false;

            //GameObject spawnerObject = (GameObject)Instantiate(Resources.Load("Spawner"));
            //Spawn spn = spawnerObject.GetComponent<Spawn>();
        }
    }


}
