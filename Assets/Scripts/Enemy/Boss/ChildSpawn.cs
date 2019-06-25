using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawn : MonoBehaviour
{
    //public GameObject normal;
    public GameObject bullet;
    //public GameObject chase;
    public GameObject armor;

    private GameObject[] enemyList;

    public float interval = 10;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new GameObject[]
        {
            //normal,
            bullet,
            //chase,
            armor,
        };
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= interval)
        {
            int enemyNum = Random.Range(0, 2);
            Debug.Log(enemyNum);
            SpawnEnemys(enemyList[enemyNum]);

            timer = 0;
        }
    }

    void SpawnEnemys(GameObject enemyPrefab)
    {
        for(int i=0;i<5; i++)
        {
            Vector3 pos = transform.position;
            pos.y += 4 - i * 2;
            Instantiate(enemyPrefab, pos, transform.rotation);
        }

    }
}
