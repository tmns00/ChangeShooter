using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    IEnumerator Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        
        // ショット
        while (true)
        {
            // 弾をプレイヤーと同じ位置&角度で作成
            spaceship.Shot(transform);
            //0.05秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Vector2 direction = new Vector3(x, y).normalized;
        Vector3 direction = new Vector3(x, y).normalized;

        //移動の制限
        Move(direction);
    }

    Rect rect = new Rect(0, 0, 1, 1); // 画面内かどうかの判定

    void Move(Vector3 direction)
    {
        //プレイヤーの座標を取得
        //Vector2 pos = transform.position;
        Vector3 pos = transform.position;

        //移動量を加える
        pos += direction * spaceship.speed * Time.deltaTime;

        var viewportPos = Camera.main.WorldToViewportPoint(pos);

        if (rect.Contains(viewportPos))
        {
            //移動
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag =="EnemyBullet")
        {
            // 弾の消去
            Destroy(other.gameObject);

            // 爆発する
            spaceship.Explosion();

            // プレイヤーを消去
            Destroy(gameObject);
        }
    }
}
