using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //カメラ移動速度
    Vector3 cameraMove = new Vector3(0.05f, 0.0f);
    //カメラ止めるとき用
    [HideInInspector]
    public bool isMove = true;

    void Update()
    {
        if (isMove)
        {
            Camera camera = Camera.main;
            camera.gameObject.transform.Translate(cameraMove);
        }
    }

    /// <summary>
    /// カメラ移動値取得
    /// </summary>
    /// <returns></returns>
    public Vector3 GetCameraMove()
    {
        return cameraMove;
    }
}
