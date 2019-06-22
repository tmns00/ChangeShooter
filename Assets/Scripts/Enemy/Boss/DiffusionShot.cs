using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffusionShot : MonoBehaviour
{
    public GameObject bullet;
    private BossBullet bBullet;

    public float Interval = 0.1f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        bBullet = bullet.GetComponent<BossBullet>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= Interval)
        {
            Shot();
            Shot();
            timer = 0;
        }    
    }

    void Shot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        bBullet.SetVelocityY(Random.Range(-0.8f, 0.8f));
    }
}
