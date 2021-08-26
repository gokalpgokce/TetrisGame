using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int[,] Grid;
    public GameObject squarePrefab;
    private GameObject Block;
    int Rows = 20 , Columns = 10;
    // Start is called before the first frame update
    void Start()
    {

        Grid = new int[Columns, Rows];

        CreateBlock(5,20);

        StartCoroutine(Fall());
        StartCoroutine(UserInput());

    }

    private void CreateBlock(int x, int y)
    {
        Block = Instantiate(squarePrefab, new Vector3(x,y,0), Quaternion.identity);
        //GameObject g = new GameObject("X: " + x + "Y:" + y);
        //g.transform.position = new Vector3(x + 0.5f, y + 0.5f);
    }

    IEnumerator Fall()
    {
        while (true)
        {
            Block.transform.position += new Vector3(0,-1,0);
            if(IsReachBottom(FindBlockPosition()))
                yield break;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator UserInput()
    {
        while (true)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(CheckBorders(FindBlockPosition()))
                {
                    Block.transform.position += new Vector3(-1,0,0);
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(CheckBorders(FindBlockPosition()))
                {
                    Block.transform.position += new Vector3(1,0,0);
                }
            }

            yield return null;
        }
    }

    public Vector3 FindBlockPosition()
    {
        return Block.transform.position;
    }
/*
    public bool CheckBlockNeighbour(Vector3 blockPosition)
    {
        //left control
        //rigth control
    }
*/

    public bool CheckBorders(Vector3 blockPosition)
    {
        if(blockPosition.x <= 0 || blockPosition.x > 10)
        {
            return false;
        }
        return true;
    }

    public bool IsReachBottom(Vector3 blockPosition)
    {
        if(blockPosition.y <= 0)
        {
            return true;
        }
        return false;
    }

}
