using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    float delay = 0.06f;

    public int playerHP;

    bool invincible = false;
    [SerializeField]
    int invincibleTime;

    [SerializeField]
    float angleSpeed;

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

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Vector2 direction = new Vector3(x, y).normalized;
        Vector3 direction = new Vector3(x, y).normalized;

        if (Input.GetKey("f") || Input.GetButton("Fire1"))
        {
            if (delay >= spaceship.shotDelay)
            {
                spaceship.Shot(transform);
                delay = 0f;
            }

            delay += 0.01f;
        }
        if (Input.GetKeyUp("f") || Input.GetButtonUp("Fire1"))
        {
            delay = spaceship.shotDelay;
        }

        Invincible();

        //移動の制限
        Move(direction);

        if (playerHP <= 0)
        {
            Deth();
        }
        PlayerRotation(y);
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
        if (!invincible)
        {
            if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "BossBullet")
            {
                // 弾の消去
                Destroy(other.gameObject);

                playerHP -= 1;
            }
            if (other.gameObject.tag == "EnemyBullet")
            {
                // 弾の消去
                Destroy(other.gameObject);
                playerHP -= 1;
                invincible = true;
                Debug.Log(playerHP);
            }
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Obstacle")
            {
                playerHP -= 1;
                Debug.Log(playerHP);
                invincible = true;
            }
        }
    }

    void Deth()
    {
        // 爆発する
        spaceship.Explosion();

        // プレイヤーを消去
        Destroy(gameObject);
    }

    void Invincible()
    {
        if (invincible)
        {
            invincibleTime++;
            if(invincibleTime >= 180)
            {
                invincible = false;
                invincibleTime -= invincibleTime;
            }
        }
    }

    void PlayerRotation(float y)
    {
        //Quaternion rotation = transform.rotation;
        //Vector3 rotationAngles = rotation.eulerAngles;
        //rotationAngles.x = Mathf.Clamp(rotationAngles.x + (1.0f * y), -30.0f, 30.0f);
        //rotation = Quaternion.Euler(rotationAngles);
        //transform.rotation = rotation;

        float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        float angleX = Mathf.Clamp(rotateX + y * angleSpeed, -40, 40);
        angleX = (angleX < 0) ? angleX + 360 : angleX;
        transform.rotation = Quaternion.Euler(angleX, 0, 0);

        //float xRotate = 0;
        //xRotate = Mathf.Clamp(xRotate + addRotate * Time.deltaTime * y, -30, 30);
        //transform.eulerAngles = new Vector3(xRotate, 0, 0);

        //if(rotationAngles.x < 40.0f)
        //{
        //    rotation = Quaternion.Euler(rotationAngles);
        //    transform.rotation = rotation;
        //}
        //else if(rotationAngles.x > -40.0f)
        //{
        //    rotation = Quaternion.Euler(rotationAngles);
        //    transform.rotation = rotation;
        //}

        //    transform.rotation = rotation;
        //}

        //Vector3 virtualHeight = transform.position;
        //Vector3 heightCheck = new Vector3(0, 0);
        //if (virtualHeight.y < heightCheck.y)
        //{
        //    transform.Rotate(new Vector3(-30, 0));
        //}
        //if (virtualHeight.y > heightCheck.y)
        //{
        //    transform.Rotate(new Vector3(30, 0));
        //}
        //heightCheck = virtualHeight;

        //if (y < 0)
        //{
        //    transform.rotation = rotation;
        //}
        //if(y > 0)
        //{
        //    transform.rotation = rotation;
        //}
    }
}
