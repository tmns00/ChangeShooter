using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    //移動スピード
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //機体の移動
    public void Move(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction * speed;
    }
}
