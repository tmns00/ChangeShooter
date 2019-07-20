using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        Debug.Log("画面外");
        Debug.Log(transform.position);
        Spawn();
    }

    void Spawn()
    {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
