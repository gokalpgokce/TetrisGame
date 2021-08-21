using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int[,] Grid;
    int Rows = 20 , Columns = 10;
    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[Columns, Rows];

        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Location(i, j);
            }
        }
    }

    private void Location(int x, int y)
    {
        GameObject g = new GameObject("X: " + x + "Y:" + y);
        g.transform.position = new Vector3(x + 0.5f, y + 0.5f);
    }
}
