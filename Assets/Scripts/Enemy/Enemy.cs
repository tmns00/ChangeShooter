using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        //ローカル座標のX軸のマイナス方向に移動する
        spaceship.Move(transform.right * -1);

        //canShotがfalseの場合、ここでコルーチンを終了させる
        if(spaceship.canShot == false)
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
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerBullet")
        {
            // 弾の消去
            Destroy(other.gameObject);

            // 爆発する
            spaceship.Explosion();

            // プレイヤーを消去
            Destroy(gameObject);
        }

        if(other.gameObject.tag=="Obstacle")
        {
            Destroy(gameObject);
        }
    }
}
