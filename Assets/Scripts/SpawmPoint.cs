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

    public GameObject alarm;
    private GameObject alarmClone;
    private GameObject alarmUI;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        canInstantiate = true;
        backSpawn = false;

        alarmUI = GameObject.Find("AlarmUI");
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

        if(spawnObject.transform.Find("Instantiate").gameObject.tag == "Chase")
        {
            backSpawn = true;
        }
    }

    private void OnBecameInvisible()
    {
        if (canInstantiate && backSpawn)
            StartCoroutine("ChaseSpawn");
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

    IEnumerator ChaseSpawn()
    {
        Vector3 pos = transform.position;
        Vector3 imagePos = new Vector3(-50,pos.y,pos.z);
        //imagePos.x += 100;
        alarmClone = Instantiate(alarm, imagePos, Quaternion.identity);
        alarmClone.transform.SetParent(alarmUI.transform, false);

        yield return new WaitForSeconds(1.5f);

        Instantiate(spawnObject, transform.position, Quaternion.identity);
        Destroy(alarmClone);
    }
}
