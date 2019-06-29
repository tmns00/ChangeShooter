using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSystem : MonoBehaviour
{
    public int HP = 5;

    private Vector3 pos; //オブジェクトの位置取得
    private bool isVisible; //画面内かどうか
    private bool isVisibleBreak; //オブジェクト消去のためのフラグ
    private bool isChange; //切り替えボタンのフラグ
    private bool posChanging; //表に移動中か
    private bool negChanging; //裏に移動中か

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        isVisibleBreak = false;
        isChange = false;
        posChanging = false;
        negChanging = false;
    }

    // Update is called once per frame
    void Update()
    {

        Move(); //左へ移動していく処理,前後移動との競合をさけるためメソッド化

        pos = transform.position; //位置取得

        //右へ切れていくか、耐久がなくなったら消去
        if ((isVisibleBreak && !isVisible) || HP <= 0)
            Break();

        //ガード説、見えていなければ下の処理はしない
        if (!isVisible)
            return;

        transform.Rotate(0, 0, 3); //オブジェクトを回転

        //Cキーを押したら切り替え
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Change"))
            isChange = true;

        //isVisible = false;

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
    //private void OnWillRenderObject()
    //{
    //    if (Camera.current.name == "Main Camera")
    //    {
    //        isVisible = true;
    //        isVisibleBreak = true;
    //    }
    //}

    private void OnBecameVisible()
    {
        isVisible = true;
        isVisibleBreak = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }

    /// <summary>
    /// オブジェクトに当たった時の処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            Break();
        }

        if (other.gameObject.tag == "PlayerBullet" || other.gameObject.tag == "EnemyBullet" ||
            other.gameObject.tag == "BossBullet" || other.gameObject.tag == "Tracking")
        {
            Destroy(other.gameObject);
            HP -= 1;
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        pos = transform.position;
        pos.x += 0.2f;
        transform.position = pos;
    }

    public bool IsChange()
    {
        return isChange;
    }

    private void Break()
    {
        Destroy(gameObject);
    }
}
