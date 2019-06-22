using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    public GameObject target;
    [SerializeField]
    int spawnDelay;
    int spawnTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnTime++;
        EnemySpawn();
       
        Vector3 pos = transform.position;
        pos.y = Random.Range(-4.0f, 6.0f);
        transform.position = pos;
    }

    void EnemySpawn()
    {
        if (spawnDelay < spawnTime / 60)
        {
            Vector3 pos = transform.position;

            GameObject childObject = enemyPrefab.transform.Find("Instantiate").gameObject;
            if (childObject.gameObject.tag == "NormalEnemy")
            {
                Quaternion homingRotate = Quaternion.LookRotation(target.transform.position - transform.position);
                Instantiate(enemyPrefab, pos, homingRotate);
            }
            else
            {
                Instantiate(enemyPrefab, pos, transform.rotation);
            }
            spawnTime = 0;
        }
    }

    public Vector3 GetTarget()
    {
        return target.transform.position;
    }
}
