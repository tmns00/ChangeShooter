using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;
    //プレイヤーショットディレイ
    float delay = 0.06f;
    //プレイヤーHP
    public int playerHP;
    //ダメージ後無敵処理用
    bool invizible = false;
    [SerializeField]
    int invizibleTime;
    int invizibleCheckCount = 0;
    //移動時モデル回転速度
    [SerializeField]
    float angleSpeed = 0;
    [SerializeField]
    float returnAngleSpeed = 0;
    //カメラ追従用
    CameraMove cameraMove;
    //rigidbody取得用
    Rigidbody rigidbodyComponent;
    //コルーチンストップ用
    Coroutine retC;
    //HPUI用
    [SerializeField]
    private LifeGauge lifeGauge;
    //ワープアイテム用変数
    [SerializeField]
    int itemMaxCount;
    [HideInInspector]
    public int currentItemCount = 0;
    [SerializeField]
    int warpTime;

    void Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
        //Rigidbodyコンポーネントを取得
        rigidbodyComponent = GetComponent<Rigidbody>();
        //メインカメラ取得
        GameObject gameObject = GameObject.Find("Main Camera");
        cameraMove = gameObject.GetComponent<CameraMove>();
        // HPをUIに反映
        lifeGauge.SetLifeGauge(playerHP);
    }

    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y).normalized;
        WarpTrigger();
        PlayerShot();
        Invincible();
        Change();
        PlayerRotation(direction.y);
        Move(direction.x, direction.y);
        // HP0で死亡
        if (playerHP <= 0)
        {
            Deth();
        }
    }

    /// <summary>
    /// プレイヤー移動処理
    /// </summary>
    /// <param name="x">プレイヤー入力座標X</param>
    /// <param name="y">プレイヤー入力座標Y</param>
    void Move(float x, float y)
    {
        //プレイヤー移動
        rigidbodyComponent.velocity = new Vector3(x, y) * spaceship.speed;
        //プレイヤーの座標を取得
        Vector3 pos = transform.position;
        //カメラの画面端の座標取得
        float distance = pos.z - Camera.main.transform.position.z;
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));
        //移動制限
        Vector3 rigidbodySpeed = Time.fixedDeltaTime * rigidbodyComponent.velocity;
        rigidbodyComponent.position = new Vector3(
        Mathf.Clamp(rigidbodyComponent.position.x + cameraMove.GetCameraMove().x, min.x - rigidbodySpeed.x, max.x - rigidbodySpeed.x),
        Mathf.Clamp(rigidbodyComponent.position.y + cameraMove.GetCameraMove().y, min.y - rigidbodySpeed.y, max.y - rigidbodySpeed.y),
        Mathf.Clamp(rigidbodyComponent.position.z + cameraMove.GetCameraMove().z, min.z - rigidbodySpeed.z, max.z - rigidbodySpeed.z)
        );
    }

    //プレイヤー表裏入れ替え用
    bool isChange = false;
    bool negChanging = false;
    bool posChanging = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!invizible)
        {
            if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "BossBullet")
            {
                // 弾の消去
                Destroy(other.gameObject);
                Damage(1);
                Debug.Log(playerHP);
                //無敵エフェクト用コルーチン
                retC = StartCoroutine(InvizibleCoroutine());
                Invoke("DoStopCoroutine", invizibleTime);
            }
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Obstacle")
            {
                Damage(1);
                Debug.Log(playerHP);
                //無敵エフェクト用コルーチン
                retC = StartCoroutine(InvizibleCoroutine());
                Invoke("DoStopCoroutine", invizibleTime);
            }

            if(other.gameObject.tag == "Warphole")
            {
                AddWarpItem();
            }
        }
    }

    /// <summary>
    /// プレイヤー死亡時処理
    /// </summary>
    void Deth()
    {
        // 爆発する
        spaceship.Explosion();

        SceneManager.LoadScene("Ending");

        //// プレイヤーを消去
        //Destroy(gameObject);
    }

    /// <summary>
    /// ダメージ後無敵処理
    /// </summary>
    void Invincible()
    {
        if (invizible)
        {
            invizibleCheckCount++;
            if(invizibleCheckCount >= 180)
            {
                invizible = false;
                invizibleCheckCount -= invizibleCheckCount;
            }
        }
    }
    
    /// <summary>
    /// プレイヤー移動時モデルを回転
    /// </summary>
    /// <param name="y">プレイヤー入力座標Y</param>
    void PlayerRotation(float y)
    {
        //Mathf.Clampを使うために0～360を-180～180に変換
        float rotateX = (transform.eulerAngles.x > 180) ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        //非操作時に元の態勢にもどす
        if (y == 0)
            rotateX = (rotateX >= 0) ? rotateX - returnAngleSpeed : rotateX + returnAngleSpeed;
        //回転制限
        float angleX = Mathf.Clamp(rotateX + y * angleSpeed, -40, 40);
        //0～360に戻す
        angleX = (angleX < 0) ? angleX + 360 : angleX;
        //回転
        transform.rotation = Quaternion.Euler(angleX, 0, 0);
    }

    /// <summary>
    /// プレイヤー表裏入れ替え処理
    /// </summary>
    void Change()
    {
        //ガード説、ワープホールにあたってなければ下の処理はしない
        if (!isChange)
            return;

        //プレイヤー現在位置取得
        Vector3 pos = transform.position;

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

        transform.position = pos;
    }

    /// <summary>
    /// プレイヤーショット
    /// </summary>
    void PlayerShot()
    {
        //ショットボタンを押している間中ショット
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            if (delay >= spaceship.shotDelay)
            {
                spaceship.Shot(transform);
                delay = 0f;
            }
            delay += 0.01f;
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1"))
        {
            //ショットディレイ用カウント初期化
            delay = spaceship.shotDelay;
        }
    }

    /// <summary>
    /// 無敵エフェクトコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator InvizibleCoroutine()
    {
        while (true)
        {
            var renderComponet = transform.GetChild(0).gameObject.GetComponent<Renderer>();
            renderComponet.enabled = !renderComponet.enabled;
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// 無敵エフェクトを時間差で止めるためのInvoke用関数
    /// </summary>
    void DoStopCoroutine()
    {
        StopCoroutine(retC);
        var renderComponet = transform.GetChild(0).gameObject.GetComponent<Renderer>();
        renderComponet.enabled = true;
    }

    /// <summary>
    /// ダメージを受けた際に呼び出すメソッド
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        playerHP -= damage;
        playerHP = Mathf.Max(0, playerHP);

        if(playerHP >= 0)
        {
            lifeGauge.SetLifeGauge(playerHP);
        }

        invizible = true;
    }

    /// <summary>
    /// アイテム追加処理
    /// </summary>
    void AddWarpItem()
    {
        if(currentItemCount < itemMaxCount)
        {
            currentItemCount++;
        }
    }

    /// <summary>
    /// アイテムを消費してワープ処理
    /// </summary>
    void WarpTrigger()
    {
        if(Input.GetKey(KeyCode.V) && currentItemCount > 0)
        {
            currentItemCount--;
            isChange = true;
            Invoke("DoChange", warpTime);
        }
    }

    void DoChange()
    {
        isChange = true;
    }
}
