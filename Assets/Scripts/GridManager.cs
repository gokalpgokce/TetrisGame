using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int[,] Grid;
    public GameObject squarePrefab;
    private GameObject Block;
    public int UserInputRun;
    int Rows = 20 , Columns = 10;
    // Start is called before the first frame update
    void Start()
    {

        Grid = new int[Columns, Rows];

        CreateBlock(5,20);
        
        StartCoroutine(UserInput());
        StartCoroutine(Fall());
    }

    private void CreateBlock(int x, int y)
    {
        Block = Instantiate(squarePrefab, new Vector3(x,y,0), Quaternion.identity);
        //GameObject g = new GameObject("X: " + x + "Y:" + y);
        //g.transform.position = new Vector3(x + 0.5f, y + 0.5f);
    }

    public bool CheckBlockLeft(Vector3 blockPosition)
    {
        Debug.Log("CheckBlockLeft Func");
        int LeftMove = (int)blockPosition.x -1;
        if(Grid[LeftMove,(int)blockPosition.y] == 1)
            return false;
        return true;
    }

    public bool CheckBlockRight(Vector3 blockPosition)
    {
        Debug.Log("CheckBlockRight Func");
        int RightMove = (int)blockPosition.x +1;
        if(Grid[RightMove,(int)blockPosition.y] == 1)
            return false;
        return true;
    }
    public bool CheckBlockVertical(Vector3 blockPosition)
    {
        int DownMove = (int)blockPosition.y-1;
        if(Grid[(int)blockPosition.x,DownMove] == 1)
        {
            return false;
        }
        else
        {
            return true; 
        }    
    }

    public void UpdateGrid(Vector3 blockPosition)
    {
        int gridx = (int)blockPosition.x;
        int gridy = (int)blockPosition.y;
        Grid[gridx,gridy] = 1;
        Debug.Log(Grid[gridx,gridy]);
        CreateBlock(5,20);
    }

    IEnumerator Fall()
    {
        while (true)
        {
            if (UserInputRun == 0)
            {
                Debug.Log("while dongu icerisi");
                if (IsReachBottom(FindBlockPosition()))
                {
                    Debug.Log("isreachbottom true");
                    UpdateGrid(FindBlockPosition());
                    yield return null;
                }
                else if(CheckBlockVertical(FindBlockPosition()))
                {
                    Debug.Log("checkblockvert true");
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
                    //UserInputRun=1;
                    Block.transform.position += new Vector3(-1,0,0);
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(CheckBordersRight(FindBlockPosition()) && CheckBlockRight(FindBlockPosition()))
                {
                    //UserInputRun=1;
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
        return Block.transform.position;
    }
    
    public bool CheckBordersLeft(Vector3 blockPosition)
    {
        if(blockPosition.x <= 0)
            return false;
        return true;
    }

    public bool CheckBordersRight(Vector3 blockPosition)
    {
        if(blockPosition.x >=9)
            return false;
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

    public bool IsFullRows()
    {
        int ColumsCount = 0;
        for(int x = 0 ; x < Rows ; x++)
        {
            for(int y = 0 ; y < Columns ; y++)
            {
                if(Grid[x,y] == 1)
                {
                    ColumsCount++;
                    if(ColumsCount == 10)
                    {
                        return true;
                    }
                }
            }
            ColumsCount = 0;
        }
        return false;
    }

    public void DeleteRows()
    {
        for(int i = 0 ; i < Columns ; i++)
        {
            
        }
    }

}
