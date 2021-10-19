using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blocks : MonoBehaviour
{
    protected Directions CurrentDirection = Directions.up;
    public abstract Vector3[] FindChildPos(Vector3 centerBlockPosition);

    public void ChangeDirection()
    {
        switch (CurrentDirection)
        {
            case Directions.up:
                CurrentDirection = Directions.right;
                break;
            case Directions.right:
                CurrentDirection = Directions.down;
                break;
            case Directions.down:
                CurrentDirection = Directions.left;
                break;
            case Directions.left:
                CurrentDirection = Directions.up;
                break;
        }
        
    }
    
}
