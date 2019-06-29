using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public GameObject bossBullet; //弾
    private GameObject moveObj; //弾を移動させるための変数
    public float shotInterval = 5; //射撃間隔
    private Vector3 velocity; //移動量

    private GameObject player; //プレイヤー

    private float timer; //タイマー用

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        Vector3 pos = transform.position; //砲台の位置
        Vector3 playerPos = player.transform.position; //プレイヤーの現在の位置

        //Debug.Log(timer);
        if(timer >= shotInterval)
        {
            TrackingShot(pos, playerPos);
            timer = 0;
        }

        //ガード説
        if (moveObj == null)
            return;

        moveObj.transform.position += velocity * 0.4f;

        if (moveObj.transform.position.x < -30f)
            Destroy(moveObj);
    }

    /// <summary>
    /// 追尾弾メソッド
    /// </summary>
    /// <param name="batteryPos">砲台の位置</param>
    /// <param name="playerPos">プレイヤーの位置</param>
    void TrackingShot(Vector3 batteryPos, Vector3 playerPos)
    {
        //弾がプレイヤーの方に向くよう設定
        Vector3 diff = playerPos - batteryPos;

        //変数に生成した弾を入れる
        moveObj = Instantiate(bossBullet, batteryPos, Quaternion.LookRotation(diff));

        //移動量をプレイヤーに向かうよう設定
        velocity = (playerPos - batteryPos).normalized;
    }
}
