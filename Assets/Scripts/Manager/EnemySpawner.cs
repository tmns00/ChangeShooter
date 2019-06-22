using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    public GameObject target;
    int spawnTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float num = Random.Range(1.0f, 5.0f);
        if(spawnTime % 60 == 0)
        {
            EnemySpawn(num);
        }
        spawnTime++;
        Vector3 pos = transform.position;
        pos.y = Random.Range(-7.0f, 7.0f);
        transform.position = pos;
    }

    void EnemySpawn(float num)
    {
        Vector3 pos = transform.position;
        if(num < 1.0f)
        {
        }
        else if(num < 5.0f)
        {
            GameObject gameObject = GameObject.Find("NormalEnemy");
            if (enemyPrefab == gameObject)
            {
                Quaternion homingRotate = Quaternion.LookRotation(target.transform.position - transform.position);
                Instantiate(enemyPrefab, pos, homingRotate);
            }
            else
            {
                Instantiate(enemyPrefab, pos, transform.rotation);
            }
        }
    }

    public Vector3 GetTarget()
    {
        return target.transform.position;
    }
}
