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
        float velocityY = Random.Range(-0.8f, 0.8f);
        //Vector3 vel = new Vector3(-0.1f, velocityY, 0);
        //Quaternion rot = Quaternion.FromToRotation(Vector3.forward, vel);
        Instantiate(bullet, transform.position, Quaternion.identity);
        bBullet.SetVelocityY(velocityY);
    }
}
