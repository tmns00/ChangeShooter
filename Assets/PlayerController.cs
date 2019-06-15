﻿using System.Collections;
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

        Vector2 direction = new Vector3(x, y).normalized;

        //移動の制限
        Move(direction);
    }

    Rect rect = new Rect(0, 0, 1, 1); // 画面内かどうかの判定

    void Move(Vector2 direction)
    {
        //プレイヤーの座標を取得
        Vector2 pos = transform.position;

        //移動量を加える
        pos += direction * spaceship.speed * Time.deltaTime;

        var viewportPos = Camera.main.WorldToViewportPoint(pos);

        if (rect.Contains(viewportPos))
        {
            //移動
            transform.position = pos;
        }
    }
}
