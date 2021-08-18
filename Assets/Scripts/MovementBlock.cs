using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBlock : MonoBehaviour
{
    private float FallTime = 1.0f;
    public static int height = 10;
    public static int width = 20;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fall());
    }

    // Update is called once per frame
    void Update()
    {
       Movement();
    //    IsValidMove();
    }

    void Movement()
    {
         if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 vector = new Vector3(-1,0,0);
            transform.position += vector;
            Debug.Log("sol tus");
            if(IsValidMove())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(1,0,0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1,0,0);
            Debug.Log("sag tus");
            if(IsValidMove())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(-1,0,0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0,-1,0);
            if(IsValidMove())
            {
                UpdateGrid();
            }
            else
            {
                transform.position += new Vector3(0,1,0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate (0,0,-90);
            if(IsValidMove())
            {
                UpdateGrid();
            }
            else
            {
                transform.Rotate (0,0,90);
            }
        }
    }

    IEnumerator Fall()  // buraya da isvalidmove kontrolu koy. y=0 oldugunda corutine durmali.
    {
        while (IsValidMove())
        {
            transform.position += new Vector3(0,-1,0);
            Debug.Log("fall fonk son pozisyon" + transform.position);
            yield return new WaitForSeconds(FallTime);
            // Debug.Log(transform.position.x);
        }
    }

    bool IsValidMove()
    {
        foreach (Transform child in transform)  //blok icindeki childlerin pozisyinunu dolanir.
        {
            Vector2 v = Grid.RoundVector(child.position);
            //Debug.Log("inside border a giden vektor " + v);
            if(!Grid.InsideBorder(v))
            {
                Debug.Log("isvalidmove func ilk if false dondu");
                return false;
            }
                
            if(Grid.grid[(int)v.x,(int)v.y] != null && Grid.grid[(int)v.x,(int)v.y].parent != transform)
            {
                Debug.Log("isvalidmove func ikinci if false dondu");
                this.enabled = false;
                return false;
            }
        }
        Debug.Log("isValidMove True dondu");
        return true;
    }

    void UpdateGrid()
    {
        for (int y = 0; y < Grid.column; y++)
        {
            for (int x = 0; x < Grid.row; x++)
            {
                if(Grid.grid[x,y] != null)
                {
                    if(Grid.grid[x,y].parent == transform)
                    {
                        Grid.grid[x,y] = null;
                    }
                }
            }
        }

        foreach (Transform child in transform)
        {
            Vector2 v = Grid.RoundVector(child.position);
            Grid.grid[(int)v.x,(int)v.y] = child;  //ınt var buralarda
            //Debug.Log(Grid.grid[(int)v.x,(int)v.y]);
        }
    }
}