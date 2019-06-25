using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    bool isMove = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            Camera camera = Camera.main;
            camera.gameObject.transform.Translate(new Vector3(0.05f, 0.0f, 0.0f));
        }
    }
}
