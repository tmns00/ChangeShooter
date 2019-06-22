using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        //velocity = new Vector3(-1, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * 0.2f;

        if (transform.position.x < -30f)
            Destroy(gameObject);
    }

    public void SetVelocityY(float velocityY)
    {
        velocity = new Vector3(-1, velocityY, 0).normalized;
    }
}
