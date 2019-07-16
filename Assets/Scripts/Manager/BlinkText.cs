using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkText : MonoBehaviour
{
    byte alpha;
    bool isMax = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().color = new Color32(255, 255, 255, (byte)Mathf.Clamp(alpha, 0,200));
        alpha = isMax ? (byte)(alpha - 2) : (byte)(alpha + 2);
        if (alpha >= 200)
            isMax = true;
        if (alpha <= 0)
            isMax = false;
    }
}
