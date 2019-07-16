using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSystem : MonoBehaviour
{
    public int HP = 5;

    private Vector3 pos; //オブジェクトの位置取得
    private bool isVisible; //画面内かどうか
    private bool isVisibleBreak; //オブジェクト消去のためのフラグ
    private bool isChange; //切り替えボタンのフラグ
    private bool posChanging; //表に移動中か
    private bool negChanging; //裏に移動中か

    //表に表示する照準
    public GameObject reticule;
    private GameObject reticuleClone;
    private GameObject reticuleUI;

    //サウンド再生用変数
    [SerializeField]
    AudioClip changeSound;
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
        isVisibleBreak = false;
        isChange = false;
        posChanging = false;
        negChanging = false;

        reticuleUI = GameObject.Find("ObstacleUIs");

        GameObject soundManagerObject = GameObject.Find("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position; //位置取得

        //Move(pos); //左へ移動していく処理,前後移動との競合をさけるためメソッド化

        //右へ切れていくか、耐久がなくなったら消去
        if ((isVisibleBreak && !isVisible) || HP <= 0)
            Break();

        //照準の移動
        ReticuleMove(pos);

        //ガード説、見えていなければ下の処理はしない
        if (!isVisible)
            return;

        transform.Rotate(0, 0, 3); //オブジェクトを回転

        //Cキーを押したら切り替え
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("Change"))
        {
            isChange = true;
            soundManager.PlaySE(changeSound);
        }

        //isVisible = false;

        //ガード説、切り替えキーが押されてなければ下の処理はしない
        if (!isChange)
            return;

        //裏でマイナス移動でなければ表へ
        if (pos.z <= 0f && !negChanging)
        {
            posChanging = true;
            pos.z += 2.0f;
        }

        //表でプラス移動でなければ裏へ
        if (pos.z >= 0f && !posChanging)
        {
            negChanging = true;
            pos.z -= 2.0f;
        }

        //プラス移動しつづけるための処理
        if (pos.z >= 0f && posChanging)
            pos.z += 2.0f;

        //マイナス移動しつづけるための処理
        if (pos.z <= 0f && negChanging)
            pos.z -= 2.0f;

        //表か裏についたら
        if (pos.z <= -20f || pos.z >= 20f)
        {
            isChange = false;
            posChanging = false;
            negChanging = false;
            return;
        }

        transform.position = pos; //設定した位置を代入       
    }

    private void FixedUpdate()
    {
        Move(pos);
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

        reticuleClone = Instantiate(reticule);
        reticuleClone.transform.SetParent(reticuleUI.transform,false);
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
        if(other.gameObject.tag=="Player" || other.gameObject.tag == "BossSheild")
        {
            Break();
        }

        if (other.gameObject.tag == "PlayerBullet" || other.gameObject.tag == "EnemyBullet" ||
            other.gameObject.tag == "BossBullet" || other.gameObject.tag == "Tracking")
        {
            Destroy(other.gameObject);
            HP -= 1;
        }

        if(other.gameObject.tag=="Enemy")
        {
            HP -= 10;
        }
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move(Vector3 position)
    {
        Vector3 move = position;
        move.x += 0.2f;
        transform.position = move;
    }

    private void ReticuleMove(Vector3 position)
    {
        if (reticuleClone == null)
            return;

        Vector3 reticuleMove = position;
        reticuleMove.z = -20;
        reticuleClone.transform.position = reticuleMove;
    }

    public bool IsChange()
    {
        return isChange;
    }

    private void Break()
    {
        Destroy(reticuleClone);
        Destroy(gameObject);
    }
}
