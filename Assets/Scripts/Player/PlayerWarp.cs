﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarp : MonoBehaviour
{
    private Vector3 pos; //オブジェクトの位置取得
    private bool isVisible; //画面内かどうか
    private bool isChange; //切り替えボタンのフラグ
    private bool posChanging; //表に移動中か
    private bool negChanging; //裏に移動中か

    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();
        isVisible = false;
        isChange = false;
        posChanging = false;
        negChanging = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move(); //右へ移動していく処理,前後移動との競合をさけるためメソッド化

        //ガード説、見えていなければ下の処理はしない
        if (!isVisible)
            return;

        //Cキーを押したら切り替え
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Change"))
            isChange = true;

        //ガード説、切り替えキーが押されてなければ下の処理はしない
        if (!isChange)
            return;

        //裏でマイナス移動でなければ表へ
        if (pos.z <= 0f && !negChanging)
        {
            posChanging = true;
            pos.z += 0.5f;
        }

        //表でプラス移動でなければ裏へ
        if (pos.z >= 0f && !posChanging)
        {
            negChanging = true;
            pos.z -= 0.5f;
        }

        //プラス移動しつづけるための処理
        if (pos.z >= 0f && posChanging)
            pos.z += 0.5f;

        //マイナス移動しつづけるための処理
        if (pos.z <= 0f && negChanging)
            pos.z -= 0.5f;

        //表か裏についたら
        if (pos.z <= -5f || pos.z >= 5f)
        {
            isChange = false;
            posChanging = false;
            negChanging = false;
            return;
        }

        transform.position = pos; //設定した位置を代入
    }

    /// <summary>
    /// カメラに写っているかどうか
    /// </summary>
    private void OnWillRenderObject()
    {
#if UNITY_EDITOR
        if (Camera.current.name != "SceneCamera" && Camera.current.name != "Preview Camera")
#endif
        {
            isVisible = true;
        }
    }

    /// <summary>
    /// オブジェクトに当たった時の処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "PlayerBullet" || other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "BossBullet")
            Destroy(other.gameObject);
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        pos = transform.position;
        pos.x += 0.1f;
        transform.position = pos;
    }
}
