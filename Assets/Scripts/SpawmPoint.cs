using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawmPoint : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject target;

    private Vector3 targetPos;

    private bool isVisible;
    private bool canInstantiate;
    private bool backSpawn;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        canInstantiate = true;
        backSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = target.transform.position;

        if(isVisible && canInstantiate && !backSpawn)
        {
            Spawn();
            canInstantiate = false;
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;

        if(spawnObject.tag=="Chase")
        {
            backSpawn = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (canInstantiate && backSpawn)
            Spawn();
    }

    void Spawn()
    {
        GameObject childObject = spawnObject.transform.Find("Instantiate").gameObject;
        if (childObject.gameObject.tag == "NormalEnemy")
        {
            Quaternion homingRotate = Quaternion.LookRotation(targetPos - transform.position);
            Instantiate(spawnObject, transform.position, homingRotate);
        }
        else
        {
            Instantiate(spawnObject, transform.position, Quaternion.identity);
        }
    }
}
