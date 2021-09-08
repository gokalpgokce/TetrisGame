using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Blocks : MonoBehaviour
{
    public abstract Vector3[] FindChildPos(Vector3 centerBlockPosition);

}
