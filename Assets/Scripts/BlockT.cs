using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockT : Blocks
{
    public override Vector3[] FindChildPos(Vector3 centerBlockPosition)
    {
        Vector3[] childPos = new Vector3[4];
        
        switch (CurrentDirection)
        {
            case Directions.up: //default
                Debug.Log("case up");
                childPos[0] = new Vector3(centerBlockPosition.x, centerBlockPosition.y - 1); // left child position
                childPos[1] = new Vector3(centerBlockPosition.x, centerBlockPosition.y + 1); // right child position
                childPos[2] = new Vector3(centerBlockPosition.x, centerBlockPosition.y);       // center child position
                childPos[3] = new Vector3(centerBlockPosition.x + 1, centerBlockPosition.y); // up child position
                break;
            case Directions.right: // -90
                Debug.Log("case right");
                childPos[0] = new Vector3(centerBlockPosition.x-1, centerBlockPosition.y + 1); // bottomright child position
                childPos[1] = new Vector3(centerBlockPosition.x-2, centerBlockPosition.y); // bottom2 child position
                childPos[2] = new Vector3(centerBlockPosition.x, centerBlockPosition.y);       // center child position
                childPos[3] = new Vector3(centerBlockPosition.x - 1, centerBlockPosition.y); // bottom child position
                break;
            case Directions.down: // -180
                Debug.Log("case down");
                childPos[0] = new Vector3(centerBlockPosition.x-1, centerBlockPosition.y - 2); // left child position
                childPos[1] = new Vector3(centerBlockPosition.x-2, centerBlockPosition.y - 1); // right child position
                childPos[2] = new Vector3(centerBlockPosition.x-1, centerBlockPosition.y);       // center child position
                childPos[3] = new Vector3(centerBlockPosition.x - 1, centerBlockPosition.y-1); // up child position
                break;
            case Directions.left: // -270
                Debug.Log("case left");
                childPos[0] = new Vector3(centerBlockPosition.x+1, centerBlockPosition.y - 1); // left child position
                childPos[1] = new Vector3(centerBlockPosition.x-1, centerBlockPosition.y - 1); // right child position
                childPos[2] = new Vector3(centerBlockPosition.x, centerBlockPosition.y-1);       // center child position
                childPos[3] = new Vector3(centerBlockPosition.x, centerBlockPosition.y-2); // up child position
                break;
        }

        return childPos;
    }
    
    
}
