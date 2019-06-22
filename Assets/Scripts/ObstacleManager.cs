using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstacle;　//岩石オブジェクト

    public float interval = 3.0f; //生成間隔

    // Start is called before the first frame update
    void Start()
    {
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
            pos.y += Random.Range(-7.0f, 7.0f);
            Instantiate(obstacle, pos, Quaternion.identity);

            //間隔をあける
            yield return new WaitForSeconds(interval);
        }
    }
}
