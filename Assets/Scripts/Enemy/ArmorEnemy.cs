using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEnemy: MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    //死亡時生成するオブジェクト
    [SerializeField]
    GameObject warpItemPrefab = null;

    //public int enemyHP;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

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
            for (int i = 0; i < transform.childCount; i++)
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

    }

    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody>().velocity = direction * spaceship.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "PlayerBullet")
        //{
        //    // 弾の消去
        //    Destroy(other.gameObject);

        //    enemyHP -= 1;
        //}

        //if (other.gameObject.tag == "Player")
        //{
        //    enemyHP -= 1;
        //}
        if (other.gameObject.tag == "PlayerBullet")
        {
            //弾消去
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Obstacle")
        {
            //Destroy(gameObject);
            Deth();
        }
    }

    void Deth()
    {
        Instantiate(warpItemPrefab, transform.position, transform.rotation);
        // 爆発する
        spaceship.Explosion();

        // エネミーを消去
        Destroy(gameObject);
    }
}
