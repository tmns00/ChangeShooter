using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawmPoint : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject target;

    private bool isVisible;
    private bool canInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        canInstantiate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isVisible && canInstantiate)
        {
            Spawn();
            canInstantiate = false;
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    void Spawn()
    {
        GameObject childObject = spawnObject.transform.Find("Instantiate").gameObject;
        if (childObject.gameObject.tag == "NormalEnemy")
        {
            Quaternion homingRotate = Quaternion.LookRotation(target.transform.position - transform.position);
            Instantiate(spawnObject, transform.position, homingRotate);
        }
        else
        {
            Instantiate(spawnObject, transform.position, Quaternion.identity);
        }
    }
}
