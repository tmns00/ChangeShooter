using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlagManager : MonoBehaviour
{
    public static bool isMove;

    private void Start()
    {
        isMove = true;
    }

    public static void IsMove()
    {
        isMove = !isMove;
    }

    public static void IsMove(bool flag)
    {
        isMove = flag;
    }
}
