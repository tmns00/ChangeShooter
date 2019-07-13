using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMove : MonoBehaviour
{
    //移動速度
    Vector3 move = new Vector3(0.05f, 0.0f);

    void Update()
    {
        if (MoveFlagManager.isMove)
        {
            transform.Translate(move);
        }
    }
}
