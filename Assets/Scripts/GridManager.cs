using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int[,] Grid;
    public GameObject[,] GridVisual;
    public GameObject squarePrefab;
    private GameObject Block;
    public int UserInputRun;
    int Rows = 20 , Columns = 10;
    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[Rows, Columns];
        GridVisual = new GameObject[Rows, Columns];

        CreateBlock(5,20);
        
        StartCoroutine(UserInput());
        StartCoroutine(Fall());
    }

    private void CreateBlock(int x, int y)
    {
        Block = Instantiate(squarePrefab, new Vector3(x,y,0), Quaternion.identity);
    }

    public bool CheckBlockLeft(Vector3 blockPosition)
    {
        int LeftMove = (int)blockPosition.y-1;
        if (Grid[(int) blockPosition.x, LeftMove] == 1)
        {
            return false;
        }
        return true;
    }

    public bool CheckBlockRight(Vector3 blockPosition)
    {
        int RightMove = (int)blockPosition.y+1;
        if (Grid[(int) blockPosition.x, RightMove] == 1)
        {
            return false;
        }
        return true;
    }
    public bool CheckBlockVertical(Vector3 blockPosition)
    {
        if(Grid[(int)blockPosition.x-1, (int)blockPosition.y] == 1)
        {
            return false;
        }
        return true;
    }

    public void UpdateGrid(Vector3 blockPosition)
    {
        int gridx = (int)blockPosition.x;
        int gridy = (int)blockPosition.y;
        Grid[gridx,gridy] = 1;
        Block.gameObject.name = "Blok: (" +gridx+"," +gridy+")";
        GridVisual[gridx, gridy] = Block;
        IsRowsFull();
        CreateBlock(5,20);
    }

    IEnumerator Fall()
    {
        while (true)
        {
            if (UserInputRun == 0)
            {
                if (IsReachBottom(FindBlockPosition()))
                {
                    UpdateGrid(FindBlockPosition());
                    yield return null;
                }
                else if(CheckBlockVertical(FindBlockPosition()))
                {
                    Block.transform.position += new Vector3(0,-1,0);
                }
                else
                {
                    UpdateGrid(FindBlockPosition());
                }
                yield return new WaitForSeconds(0.4f);
            }
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator UserInput()
    {
        while (true)
        {
            UserInputRun = 0;
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(CheckBordersLeft(FindBlockPosition()) && CheckBlockLeft(FindBlockPosition()))
                {
                    Block.transform.position += new Vector3(-1,0,0);
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(CheckBordersRight(FindBlockPosition()) && CheckBlockRight(FindBlockPosition()))
                {
                    Block.transform.position += new Vector3(1,0,0);
                }
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(!IsReachBottom(FindBlockPosition()) && CheckBlockVertical(FindBlockPosition()))
                {
                    UserInputRun=1;
                    Block.transform.position += new Vector3(0,-1,0);
                }
            }
            yield return null;
        }
    }

    public Vector3 FindBlockPosition()
    {
        int col = Mathf.RoundToInt(Block.transform.position.x);
        int row = Mathf.RoundToInt(Block.transform.position.y);
        return new Vector3(row, col);
    }
    
    public bool CheckBordersLeft(Vector3 blockPosition)
    {
        if(blockPosition.y <= 0)
            return false;
        return true;
    }

    public bool CheckBordersRight(Vector3 blockPosition)
    {
        if(blockPosition.y >=9)
            return false;
        return true;
    }

    public bool IsReachBottom(Vector3 blockPosition)
    {
        if(blockPosition.x <= 0)
        {
            return true;
        }
        return false;
    }

    public void IsRowsFull()
    {
        int ColumsCount = 0;
        for(int row = 0 ; row < Rows ; row++)
        {
            for(int col = 0 ; col < Columns ; col++)
            {
                if(Grid[row,col] == 1)
                {
                    ColumsCount++;
                    if(ColumsCount == 10)
                    {
                        Debug.Log("Satir dolu delete row calisacak");
                        DeleteRows(row);
                    }
                }
            }
            //Debug.Log("ColumsCount: " + ColumsCount);
            ColumsCount = 0;
        }
    }

    public void DeleteRows(int row)
    {
        Debug.Log("delete row func silinecek satir: " + row);
        for(int col = 0 ; col < Columns ; col++)
        {
            Debug.Log("row" + row);
            Debug.Log("col" + col);
            GameObject.Destroy(GridVisual[row,col]);
            GridVisual[row, col] = null;
            Grid[row, col] = 0;
            Debug.Log("Grid: " + Grid[row,col]);
        }
    }
}
