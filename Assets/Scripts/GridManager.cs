using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public int[,] Grid;
    public GameObject[,] GridVisual;
    public GameObject squarePrefab;
    public GameObject blockTPrefab;
    private GameObject Block;
    public int UserInputRun;
    int Rows = 20 , Columns = 10;
    // Start is called before the first frame update
    void Start()
    {
        Grid = new int[Rows, Columns];
        GridVisual = new GameObject[Rows, Columns];
        CreateBlock(5,18);
        StartCoroutine(UserInput());
        StartCoroutine(Fall());
    }

    private void CreateBlock(int x, int y)
    {
        Block = Instantiate(blockTPrefab, new Vector3(x,y,0), Quaternion.identity);
    }
    
    public void UpdateGrid(Vector3 blockPosition)
    {
        Debug.Log("Update Grid");
        int gridx = (int)blockPosition.x;
        int gridy = (int)blockPosition.y;
        Grid[gridx,gridy] = 1;
        //Block.gameObject.name = "Blok: (" +gridx+"," +gridy+")";
        GameObject.Destroy(Block);
        GameObject square = Instantiate(squarePrefab, new Vector3(gridy,gridx), Quaternion.identity);
        // Debug.Log("square: " + square.transform.position);
        GridVisual[gridx, gridy] = square;
    }
    IEnumerator Fall()
    {
        while (true)
        {
            if (UserInputRun == 0)
            {
                bool shouldStop = false;
                Vector3 centerPosition = FindBlockPosition();
                Vector3[] childPositions = FindBlockChildPos(centerPosition);

                for (int i = 0; i < childPositions.Length; i++)
                {
                    if (IsReachBottom(childPositions[i]))
                    {
                        shouldStop = true;
                        break;
                    }
                    else if (CheckBlockVertical(childPositions[i]))
                    {
                        shouldStop = false;
                    }
                    else
                    {
                        shouldStop = true;
                        break;
                    }
                }
                
                if (shouldStop)
                {
                    for (int i = 0; i < childPositions.Length; i++)
                    {
                        UpdateGrid(childPositions[i]);
                        //Debug.Log("update gride gonderilecek koordinat" + childPositions[i]);
                    }
                    IsRowsFull();
                    CreateBlock(5,18);
                }
                else
                {
                    Block.transform.position += new Vector3(0,-1,0);
                }
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator UserInput()
    {
        while (true)
        {
            UserInputRun = 0;
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                bool canMoveLeft = false;
                Vector3 centerPosition = FindBlockPosition();
                Vector3[] childPositions = FindBlockChildPos(centerPosition);
                for (int i = 0; i < childPositions.Length; i++)
                {
                    if (CheckBordersLeft(childPositions[i]) && CheckBlockLeft(childPositions[i]))
                    {
                        canMoveLeft = true;
                    }
                    else
                    {
                        canMoveLeft = false;
                        break;
                    }
                }

                if (canMoveLeft)
                {
                    Block.transform.position += new Vector3(-1,0,0);    
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                bool canMoveRight = false;
                Vector3 centerPosition = FindBlockPosition();
                Vector3[] childPositions = FindBlockChildPos(centerPosition);
                for (int i = 0; i < childPositions.Length; i++)
                {
                    if (CheckBordersRight(childPositions[i]) && CheckBlockRight(childPositions[i]))
                    {
                        canMoveRight = true;
                    }
                    else
                    {
                        canMoveRight = false;
                        break;
                    }
                }

                if (canMoveRight)
                {
                    Block.transform.position += new Vector3(1,0,0);    
                }
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                bool canMoveBottom = false;
                Vector3 centerPosition = FindBlockPosition();
                Vector3[] childPositions = FindBlockChildPos(centerPosition);
                
                for (int i = 0; i < childPositions.Length; i++)
                {
                    if (!IsReachBottom(childPositions[i]) && CheckBlockVertical(childPositions[i]))
                    {
                        canMoveBottom = true;
                    }
                    else
                    {
                        canMoveBottom = false;
                        break;
                    }
                }

                if (canMoveBottom)
                {
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

    public Vector3[] FindBlockChildPos(Vector3 centerBlockPosition)
    {
        Vector3[] childPos = new Vector3[4];
        
        childPos[0] = new Vector3(centerBlockPosition.x, centerBlockPosition.y - 1); // left child position
        childPos[1] = new Vector3(centerBlockPosition.x, centerBlockPosition.y + 1); // right child position
        childPos[2] = new Vector3(centerBlockPosition.x, centerBlockPosition.y);       // center child position
        childPos[3] = new Vector3(centerBlockPosition.x + 1, centerBlockPosition.y); // up child position
        
        return childPos;
    }
    
    public bool CheckBordersLeft(Vector3 blockPosition)
    {
        if(blockPosition.y <= 0)
            return false;
        return true;
    }
    public bool CheckBlockLeft(Vector3 blockPosition)
    {
        int leftMove = (int)blockPosition.y-1;
        if (Grid[(int) blockPosition.x, leftMove] == 1)
        {
            return false;
        }
        return true;
    }

    public bool CheckBordersRight(Vector3 blockPosition)
    {
        if(blockPosition.y >=9)
            return false;
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
                        DeleteRows(row);
                    }
                }
            }
            ColumsCount = 0;
        }
    }
    public void DeleteRows(int row)
    {
        for(int col = 0 ; col < Columns ; col++)
        {
            GameObject.Destroy(GridVisual[row,col]);
            GridVisual[row, col] = null;
            Grid[row, col] = 0;
        }
        SlideRows(row);
    }

    public void SlideRows(int deletedRow)
    {
        for (int row = deletedRow; row < Rows-1; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                Grid[row, col] = Grid[row + 1, col];
                GridVisual[row, col] = GridVisual[row + 1, col];
                if (GridVisual[row, col] != null)
                {
                    GridVisual[row, col].transform.position = new Vector3(col, row);    
                }
                GridVisual[row + 1, col] = null;
            }
        }
    }
}
