﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    float delay = 0.06f;

    public int playerHP;

    void Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        
        //// ショット
        //while (true)
        //{
        //    // 弾をプレイヤーと同じ位置&角度で作成
        //    spaceship.Shot(transform);
        //    //0.05秒待つ
        //    yield return new WaitForSeconds(spaceship.shotDelay);
        //}
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Vector2 direction = new Vector3(x, y).normalized;
        Vector3 direction = new Vector3(x, y).normalized;

        if (Input.GetKey("f"))
        {
            if (delay >= spaceship.shotDelay)
            {
                spaceship.Shot(transform);
                delay = 0f;
            }

            delay += 0.01f;
        }
        if (Input.GetKeyUp("f"))
        {
            delay = spaceship.shotDelay;
        }

        //移動の制限
        Move(direction);

        if (playerHP <= 0)
        {
            Deth();
        }
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
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "BossBullet")
        {
            // 弾の消去
            Destroy(other.gameObject);

            playerHP -= 1;
        }

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log(playerHP);
            playerHP -= 1;
        }
        
        if(other.gameObject.tag=="Obstacle")
        {
            playerHP -= 1;
        }
    }

    void Deth()
    {
        // 爆発する
        spaceship.Explosion();

        // プレイヤーを消去
        Destroy(gameObject);
    }
}
