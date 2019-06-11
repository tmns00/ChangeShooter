using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Spaceshipコンポーネント
    Spaceship spaceship;

    void Start()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y).normalized;

        //移動の制限
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        //画面左下のワールド座標をビューポートから取得
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));

        //画面右上のワールド座標をビューポートから取得
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        //プレイヤーの座標を取得
        Vector2 pos = transform.position;

        //移動量を加える
        pos += direction * spaceship.speed * Time.deltaTime;

        //プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //制限をかけた値をプレイヤーの位置とする
        transform.position = pos;
    }
}
