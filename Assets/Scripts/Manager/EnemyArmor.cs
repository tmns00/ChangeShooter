using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmor: MonoBehaviour
{
    void FixedUpdate()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            //弾消去
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
