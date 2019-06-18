using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot : MonoBehaviour
{
    public GameObject bossBullet;
    private GameObject moveObj;
    public float shotInterval = 5;
    private Vector3 velocity;

    private GameObject player;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        Vector3 pos = transform.position;
        Vector3 playerPos = player.transform.position;

        Debug.Log(timer);
        if(timer >= shotInterval)
        {
            TrackingShot(pos, playerPos);
            timer = 0;
        }

        if (moveObj == null)
            return;

        moveObj.transform.position += velocity * 0.2f;

        if (pos.x < -30f)
            Destroy(moveObj);
    }

    void TrackingShot(Vector3 batteryPos, Vector3 playerPos)
    {
        moveObj = Instantiate(bossBullet, batteryPos, transform.rotation);
        velocity = (playerPos - batteryPos).normalized;
    }
}
