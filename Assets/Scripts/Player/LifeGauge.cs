using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGauge : MonoBehaviour
{
    [SerializeField]
    private GameObject lifeObj;
    
    // ライフゲージ全消去＆HP分作成
    public void SetLifeGauge(int life)
    {
        //体力を一旦全消去
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < life; i++)
        {
            Instantiate(lifeObj, transform);
        }
    }
}
