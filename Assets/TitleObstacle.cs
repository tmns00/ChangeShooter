using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleObstacle : MonoBehaviour
{
    private Vector3 pos; //オブジェクトの位置取得
    private bool isVisible; //画面内かどうか
    private bool isVisibleBreak; //オブジェクト消去のためのフラグ

    void Start()
    {
        isVisible = false;
        isVisibleBreak = false;
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position; //位置取得

        Move(pos); //左へ移動していく処理,前後移動との競合をさけるためメソッド化

        //右へ切れていったら消去
        if ((isVisibleBreak && !isVisible))
            Break();

        //ガード説、見えていなければ下の処理はしない
        if (!isVisible)
            return;

        transform.Rotate(0, 0, 2); //オブジェクトを回転
    }

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
    /// 移動処理
    /// </summary>
    private void Move(Vector3 position)
    {
        Vector3 move = position;
        move.x += 0.1f;
        transform.position = move;
    }

    private void Break()
    {
        Destroy(gameObject);
    }
}
