using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacle1;　//岩石オブジェクト
    //public GameObject obstacle2;
    //private GameObject[] obstacles;

    public float maxInterval = 4.0f; //生成間隔
    public float minInterval = 1.0f;

    private bool isUp;

    private bool canStart;

    // Start is called before the first frame update
    void Start()
    {
        isUp = false;

        canStart = true;
        //obstacles = new GameObject[]
        //{
        //    obstacle1,
        //    obstacle2,
        //};

        ////生成コルーチン開始
        //StartCoroutine("Spawn");
    }

    private void Update()
    {
        if(!MoveFlagManager.isMove && canStart)
        {
            canStart = false;
            //生成コルーチン開始
            StartCoroutine("Spawn");
        }

        if(MoveFlagManager.isMove && !canStart)
        {
            StopCoroutine("Spawn");
            canStart = true;
        }
    }

    /// <summary>
    /// 生成コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Spawn()
    {
        while(true)
        {
            GameObject spawnObject = obstacle1;
            Vector3 pos = transform.position;
            //高さを設定
            float height = isUp ? 4.5f : -4.5f;
            pos.y += (height + Random.Range(-3.0f, 3.0f));
            Instantiate(spawnObject, pos, Quaternion.identity);

            isUp = !isUp;
            //間隔をあける
            float interval = Random.Range(maxInterval, maxInterval);
            yield return new WaitForSeconds(interval);
        }
    }
}
