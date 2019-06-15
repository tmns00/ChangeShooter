using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacle;

    public float interval = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            Vector3 pos = transform.position;
            pos.y += Random.Range(-7.0f, 7.0f);
            Instantiate(obstacle, pos, Quaternion.identity);

            yield return new WaitForSeconds(interval);
        }
    }
}
