using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine.Networking;
public class Cannon {
    public int size = 4;
    public int bsize = 2;
    public int num_p = 4;
    public int root_p = 2;
    public int[,] ShiftLeft(int[,] matrix, int i)
    {
        int[,] newM = (int[,])matrix.Clone();
        int[,] temp_slice = sliceMatrix(matrix, i, 0);
        int indl = size / bsize - 1;
        for (int j = 0; j < indl; j++)
        {
            int[,] slice = sliceMatrix(matrix, i, j + 1);
            assignSlice(newM, slice, i, j);
        }
        assignSlice(newM, temp_slice, i, indl);
        return newM;
    }
    public int[,] ShiftUp(int[,] matrix, int j)
    {

        int[,] newM = (int[,])matrix.Clone();
        int[,] temp_slice = sliceMatrix(matrix, 0, j);
        int indl = size / bsize - 1;
        for (int i = 0; i < indl; i++)
        {
            int[,] slice = sliceMatrix(matrix, i + 1, j);
            assignSlice(newM, slice, i, j);
        }
        assignSlice(newM, temp_slice, indl, j);
        return newM;
    }



    public string matrixToString(int[,] m, int size)
    {
        string s = "";
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                string e = m[i, j].ToString() + "$";
                s += e;
            }
        }
        return s;
    }

    public int[,] stringToMatrix(string s, int size)
    {
        Debug.Log(s);
        string[] elements = s.Split('$');
        int[,] m = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int e = Convert.ToInt16(elements[size * i + j]);
                m[i, j] = e;
            }
        }
        return m;
    }

    public void addSlice(int[,] originM, int[,] slice, int row, int col)
    {
        for (int i = 0; i < bsize; i++)
        {
            for (int j = 0; j < bsize; j++)
            {
                int n = slice[i, j];
                originM[row * bsize + i, col * bsize + j] += n;
            }
        }
    }

    public void assignSlice(int[,] originM, int[,] slice, int row, int col)
    {
        for (int i = 0; i < bsize; i++)
        {
            for (int j = 0; j < bsize; j++)
            {
                int n = slice[i, j];
                originM[row * bsize + i, col * bsize + j] = n;
            }
        }
    }
    public int[,] sliceMatrix(int[,] m, int row, int col)
    {
        int[,] slice = new int[bsize, bsize];
        for (int i = bsize * row; i < bsize * row + bsize; i++)
        {
            for (int j = bsize * col; j < bsize * col + bsize; j++)
            {
                slice[i % bsize, j % bsize] = m[i, j];
            }

        }

        return slice;
    }

    public void printMatrix(int[,] m, int n)
    {
        String s = "";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                s += (m[i, j].ToString() + " ");
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    public int[,] MatrixProduct(int[,] a, int[,] b, int n)
    {
        int[,] c = new int[n, n];
        int r, s, k;
        for (r = 0; r < n; r++)
        {
            for (s = 0; s < n; s++)
            {
                for (k = 0; k < n; k++)
                {
                    c[r, s] += a[r, k] * b[k, s];
                }
            }
        }
        return c;
    }
}
