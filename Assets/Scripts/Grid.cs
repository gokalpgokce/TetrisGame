using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int row = 10;
    public static int column = 20;

    public static Transform[,] grid = new Transform[row,column];

    public static Vector2 RoundVector(Vector2 v)
    {
        Debug.Log(new Vector2 (v.x,v.y));

        // return new Vector2 (Mathf.Round(v.x),Mathf.Round(v.y));
        return new Vector2 (v.x,v.y);
        
    }

    public static bool InsideBorder(Vector2 pos)  //childlardan herhangi biri sinir gecerse false olmali
    {
        Debug.Log("inside bordera gelen vektor " + pos);
        Debug.Log(pos.y);
        return (pos.x >= 0 && pos.x < row && pos.y > 0.51);
    }

    public static void DeleteRows(int y)
    {
        for (int x = 0; x < row; x++)
        {
            GameObject.Destroy(grid[x,y].gameObject);
            grid[x,y] = null;
        }
    }

    public static void DecreaseRows(int y)
    {
        for (int x = 0; x < row; x++)
        {
            if(grid[x,y] != null)
            {
                grid[x,y-1] = grid[x,y];
                grid[x,y] = null;
                grid[x,y-1].position += new Vector3(0,-1,0);
            }
        }
    }

    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < column; i++)
        {
            DecreaseRows(i);
        }
    }

    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < row; x++)
        {
            if(grid[x,y] == null)
                return false;
        }
        return true;
    }

    public static void DeleteWholeRows()
    {
        for (int y = 0; y < column; y++)
        {
            if(IsRowFull(y))
                DeleteRows(y);
        }
    }






}
