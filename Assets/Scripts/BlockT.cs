using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockT : Blocks
{
    public override Vector3[] FindChildPos(Vector3 centerBlockPosition)
    {
        Vector3[] childPos = new Vector3[4];
        
        childPos[0] = new Vector3(centerBlockPosition.x, centerBlockPosition.y - 1); // left child position
        childPos[1] = new Vector3(centerBlockPosition.x, centerBlockPosition.y + 1); // right child position
        childPos[2] = new Vector3(centerBlockPosition.x, centerBlockPosition.y);       // center child position
        childPos[3] = new Vector3(centerBlockPosition.x + 1, centerBlockPosition.y); // up child position

        return childPos;
    }
}
