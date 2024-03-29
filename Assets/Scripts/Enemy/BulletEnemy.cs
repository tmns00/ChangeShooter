﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy: MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;
    
    public int enemyHP;

    [SerializeField]
    GameObject hpUpItemPrefab = null;

    //サウンド再生用変数
    SoundManager soundManager;
    [SerializeField]
    AudioClip explosionSound;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        GameObject soundManagerObject = GameObject.Find("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();

        //ローカル座標のX軸のマイナス方向に移動する
        Move(transform.right * -1);

        //canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            //子要素を全て取得する
            for (int i = 0; i < transform.childCount - 2; i++)
            {
                Transform shotPosition = transform.GetChild(i);

                // ShotPoisitionの位置&角度で弾を撃つ
                spaceship.Shot(shotPosition);

                // shotDelay秒待つ
                yield return new WaitForSeconds(spaceship.shotDelay);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyHP <= 0)
        {
            Deth();
        }
    }

    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody>().velocity = direction * spaceship.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            // 弾の消去
            Destroy(other.gameObject);

            enemyHP -= 1;
        }

        if (other.gameObject.tag == "Player")
        {
            enemyHP -= 1;
        }

        if (other.gameObject.tag == "Obstacle")
        {
            Deth();
        }
    }

    void Deth()
    {
        // 爆発する
        spaceship.Explosion();
        soundManager.PlaySE(explosionSound);

        GameObject player = GameObject.Find("Player2");
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.killCount++;
        if (playerController.killCount >= playerController.MaxKillCount)
        {
            Quaternion prefabRotate = transform.rotation;
            prefabRotate = Quaternion.Euler(-90, 0, 0);
            Instantiate(hpUpItemPrefab, transform.position, prefabRotate);
            playerController.killCount = 0;
        }

        // エネミーを消去
        Destroy(gameObject);
    }
}
