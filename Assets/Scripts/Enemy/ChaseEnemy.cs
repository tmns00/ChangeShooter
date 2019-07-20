using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    public int enemyHP;

    SpawmPoint enemySpawner;
    Rigidbody rb;

    [SerializeField]
    GameObject hpUpItemPrefab = null;

    //サウンド再生用変数
    SoundManager soundManager;
    [SerializeField]
    AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        rb = GetComponent<Rigidbody>();

        GameObject gameObject = GameObject.Find("SpawnPoint");
        enemySpawner = gameObject.GetComponent<SpawmPoint>();

        GameObject soundManagerObject = GameObject.Find("SoundManager");
        soundManager = soundManagerObject.GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyHP <= 0)
        {
            Deth();
        }

        //プレイヤーの方向に向かって移動していく
        //プレイヤーとの差分を取得
        Vector3 diff = (enemySpawner.target.transform.position - transform.position); 
        rb.velocity = new Vector3(diff.x * spaceship.speed, diff.y * spaceship.speed);
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
