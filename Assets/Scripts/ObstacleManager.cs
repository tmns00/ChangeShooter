using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacle;　//岩石オブジェクト

    public float maxInterval = 3.0f; //生成間隔
    public float minInterval = 2.0f;

    private bool isUp;

    // Start is called before the first frame update
    void Start()
    {
        isUp = false;
        //生成コルーチン開始
        StartCoroutine("Spawn");
    }

    /// <summary>
    /// 生成コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Spawn()
    {
        while(true)
        {
            Vector3 pos = transform.position;
            //高さを設定
            float height = isUp ? 4.5f : -4.5f;
            pos.y += (height + Random.Range(-3.0f, 3.0f));
            Instantiate(obstacle, pos, Quaternion.identity);

            isUp = !isUp;
            //間隔をあける
            float interval = Random.Range(maxInterval, maxInterval);
            yield return new WaitForSeconds(interval);
        }
    }
}
