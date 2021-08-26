using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] blockPrefabs;

    // Gameobject blockPrefab = blockPrefab[Random.Range(0,blockPrefab.Length)];
    // Start is called before the first frame update
    void Start()
    {
        SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBlock()
    {
        //int index = Random.Range(0,blockPrefabs.Length);
        //Instantiate (blockPrefabs[index], transform.position, Quaternion.identity);
    }
}
