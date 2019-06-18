using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    private Boss boss;

    private void Awake()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            boss.ShieldFlag(false);
            Destroy(gameObject);
        }

        if(other.gameObject.tag=="PlayerBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
