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
    float angleSpeed = 0;

    bool isChange = false;
    bool negChanging = false;
    bool posChanging = false;
    Vector3 cPos;

    CameraMove cameraMove;

    Rigidbody rigidbody;

    void Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        rigidbody = GetComponent<Rigidbody>();

        cPos = transform.position;

        GameObject gameObject = GameObject.Find("Main Camera");
        cameraMove = gameObject.GetComponent<CameraMove>();
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

        //裏側へ移動
        Change();

        //移動の制限
        Move(x, y);

        //Mpos.z = Cpos.z;
        //transform.position = Mpos;

        if (playerHP <= 0)
        {
            Deth();
        }
        PlayerRotation(y);
    }

    Rect rect = new Rect(0, 0, 1, 1); // 画面内かどうかの判定

    void Move(float x, float y)
    {
        rigidbody.velocity = new Vector3(x, y, 0.0f) * spaceship.speed;
        //プレイヤーの座標を取得
        Vector3 pos = transform.position;

        float distance = pos.z - Camera.main.transform.position.z;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

        //移動量を加える
        //pos += direction * spaceship.speed * Time.deltaTime + cameraMove.cameraMove;
        Vector3 rigidbodySpeed = Time.fixedDeltaTime * rigidbody.velocity;
        rigidbody.position = new Vector3(
        Mathf.Clamp(rigidbody.position.x + cameraMove.cameraMove.x, min.x - rigidbodySpeed.x, max.x - rigidbodySpeed.x),
        Mathf.Clamp(rigidbody.position.y + cameraMove.cameraMove.y, min.y - rigidbodySpeed.y, max.y - rigidbodySpeed.y),
        0.0f
        );
        //次に移動する位置が画面内かどうか
        //var viewportPos = Camera.main.WorldToViewportPoint(pos);
        //画面内であれば
        //if (rect.Contains(viewportPos))
        //{
        //    pos.z = cPos.z;
        //    //移動
        //    transform.position = pos;
        //}
        //pos.z = cPos.z;
        //transform.position = pos;
        //Mpos = pos;
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

            GameObject worpHole = GameObject.Find("WarpHole");
            if(other.gameObject == worpHole)
            {
                isChange = true;
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
        //Mathf.Clampを使うために0～360を-180～180に変換
        float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        //スピードと方向を追加
        float angleX = Mathf.Clamp(rotateX + y * angleSpeed, -40, 40);
        //0～360に戻す
        angleX = (angleX < 0) ? angleX + 360 : angleX;
        //回転
        transform.rotation = Quaternion.Euler(angleX, 0, 0);
    }

    void Change()
    {
        Vector3 pos = transform.position;
     
        //ガード説、切り替えキーが押されてなければ下の処理はしない
        if (!isChange)
            return;

        //裏でマイナス移動でなければ表へ
        if (pos.z <= 0f && !negChanging)
        {
            posChanging = true;
            pos.z += 0.5f;
        }

        //表でプラス移動でなければ裏へ
        if (pos.z >= 0f && !posChanging)
        {
            negChanging = true;
            pos.z -= 0.5f;
        }

        //プラス移動しつづけるための処理
        if (pos.z >= 0f && posChanging)
            pos.z += 0.5f;

        //マイナス移動しつづけるための処理
        if (pos.z <= 0f && negChanging)
            pos.z -= 0.5f;

        //表か裏についたら
        if (pos.z <= -5f || pos.z >= 5f)
        {
            isChange = false;
            negChanging = false;
            posChanging = false;
            return;
        }
        cPos = pos;
    }
}
