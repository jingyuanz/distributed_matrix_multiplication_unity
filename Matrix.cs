using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour {
    public int nrow;
    public int ncol;
    public int[,] data;
    // Use this for initialization
    public Matrix(int row, int col)
    {
        nrow = row;
        ncol = col;
        data = new int[nrow, ncol];
        for (int i = 0; i < nrow; i++)
        {
            for (int j = 0; j < ncol; j++)
            {
                data[i,j] = 0;
            }
        }
    }
    public void populate_matrix(Matrix m)
    {
        int i, j;
        for (i = 0; i < m.nrow; i++)
        {
            for (j = 0; j < m.ncol; j++)
            {
                m.data[i,j] = UnityEngine.Random.Range(1,10);
            }
        }
    }
    void print_matrix()
    {
        int i, j;

        for (i = 0; i < this.nrow; i++)
        {
            for (j = 0; j < this.ncol; j++)
            {
                string s = string.Format("[%d,%d] = %d  ", i, j, this.data[i, j]);
                Debug.Log(s);
            }
        }
    }

    void shift_matrix_left(Matrix m, int block_sz, int initial)
    {
        int i, j, k, s, step = block_sz;
        Matrix aux = new Matrix(1, m.ncol);

        for (k = 0, s = 0; k < m.ncol; k += block_sz, s++)
        {
            for (i = k; i < (k + block_sz); i++)
            {
                if (initial > 0)
                {
                    step = s * block_sz;
                }
                for (j = 0; j < m.ncol; j++)
                {
                    aux.data[0,j] = m.data[i,(j + step) % m.ncol];
                }
                for (j = 0; j < m.ncol; j++)
                {
                    m.data[i,j] = aux.data[0,j];
                }
            }
        }
    }

    void shift_matrix_up(Matrix m, int block_sz, int initial)
    {
        int i, j, k, s, step = block_sz;
        Matrix aux = new Matrix(1, m.ncol);

        for (k = 0, s = 0; k < m.nrow; k += block_sz, s++)
        {
            for (i = k; i < (k + block_sz); i++)
            {
                if (initial > 0)
                {
                    step = s * block_sz;
                }
                for (j = 0; j < m.nrow; j++)
                {
                    aux.data[0,j] = m.data[(j + step) % m.nrow,i];
                }
                for (j = 0; j < m.nrow; j++)
                {
                    m.data[j,i] = aux.data[0,j];
                }
            }
        }
    }
    void matrix_product(Matrix c, Matrix a, Matrix b)
    {
        int r, s, k;

        for (r = 0; r < a.nrow; r++)
        {
            for (s = 0; s < b.ncol; s++)
            {
                for (k = 0; k < a.ncol; k++)
                {
                    c.data[r,s] += a.data[r,k] * b.data[k,s];
                }
            }
        }
    }

    int[] create_array_as_matrix(int r, int c)
    {
        int[] mat = new int[r*c];
        return mat;
    }

    void populate_array_as_matrix(int[] arr, int r, int c)
    {
        int j;
        for (j = 0; j < r * c; j++)
        {
            arr[j] = UnityEngine.Random.Range(1,10);
        }
    }
    bool array_as_matrix_equals(int[] a, int[] b, int r, int c)
    {
        int i = 0;
        for (i = 0; i < r * c; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }
        return true;
    }
}
