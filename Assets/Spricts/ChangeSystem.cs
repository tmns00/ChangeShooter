using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSystem : MonoBehaviour
{
    //private Renderer renderer;

    private Vector3 pos;
    private bool isVisible;
    private bool isChange;
    private bool posChanging;
    private bool negChanging;

    // Start is called before the first frame update
    void Start()
    {
        //renderer = GetComponent<Renderer>();
        isVisible = false;
        isChange = false;
        posChanging = false;
        negChanging = false;
    }

    // Update is called once per frame
    void Update()
    {      
        if (!isVisible)
            return;

        pos = transform.position;

        if (Input.GetKeyDown(KeyCode.C))
            isChange = true;
        
        if (!isChange)
            return;

        if (pos.z <= 0f && !negChanging)
        {
            posChanging = true;
            pos.z += 0.5f;
        }

        if (pos.z >= 0f && !posChanging)
        {
            negChanging = true;
            pos.z -= 0.5f;
        }

        if(pos.z >= 0f && posChanging)
            pos.z += 0.5f;

        if(pos.z <= 0f && negChanging)
            pos.z -= 0.5f;

        if (pos.z <= -5f || pos.z >= 5f)
        {
            isChange = false;
            posChanging = false;
            negChanging = false;
            return;
        }

        transform.position = pos;
    }

    private void OnWillRenderObject()
    {
#if UNITY_EDITOR
        if(Camera.current.name!="SceneCamera" && Camera.current.name != "Preview Camera")
#endif
        {
            isVisible = true;
        }
    }
}
