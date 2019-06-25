using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    private Boss boss;

    [SerializeField]
    private GameObject obstacle;
    private ChangeSystem changeSystem;

    private void Awake()
    {
        boss = GameObject.Find("Boss").GetComponent<Boss>();
        //changeSystem = obstacle.GetComponent<ChangeSystem>();
    }

    private void Update()
    {
        Debug.Log(changeSystem);

        //if (changeSystem != null)
        //    return;

        //changeSystem = GameObject.Find("CubeObject").GetComponent<ChangeSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        //後ろから岩石を当てると装甲が取れる
        if (other.gameObject.tag == "Obstacle")
        {
            changeSystem = other.gameObject.GetComponent<ChangeSystem>();

            if(changeSystem.IsChange())
            {
                boss.ShieldFlag(false);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                Destroy(other.gameObject);
            }

        }

        //弾、岩石をはじく
        if(other.gameObject.tag=="PlayerBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
