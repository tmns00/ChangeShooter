using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField]
    private Vector3 velocity;

    private Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        //velocity = new Vector3(-1, 0, 0);
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity.normalized * 0.2f;

        if (transform.position.x < -30f)
            Destroy(gameObject);

        Vector3 nowPos = transform.position;
        transform.rotation = Quaternion.LookRotation(nowPos - originPos);
    }

    public void SetVelocityY(float velocityY)
    {
        velocity = new Vector3(-1, velocityY, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacles")
            Destroy(gameObject);
    }
}
