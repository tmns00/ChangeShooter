using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samplePlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //Clamp();

        rigidbody.AddForce(new Vector3(h, v, 0) * 250 * Time.deltaTime);
    }

    //void Clamp()
    //{
    //    Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

    //    Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    //    Vector3 pos = transform.position;

    //    pos.x = Mathf.Clamp(pos.x, min.x, max.x);
    //    pos.y = Mathf.Clamp(pos.y, min.y, max.y);

    //    transform.position = pos;
    //}
}
