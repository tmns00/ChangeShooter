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

    // Start is called before the first frame update
    void Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        rb = GetComponent<Rigidbody>();

        GameObject gameObject = GameObject.Find("SpawnPoint");
        enemySpawner = gameObject.GetComponent<SpawmPoint>();
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

        // エネミーを消去
        Destroy(gameObject);
    }
}
