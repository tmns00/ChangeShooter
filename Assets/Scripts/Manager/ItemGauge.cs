using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGauge : MonoBehaviour
{
    [SerializeField]
    private GameObject itemObj;

    // アイテムゲージ全消去＆残り分作成
    public void SetItemGauge(int item)
    {
        //アイテムを一旦全消去
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < item; i++)
        {
            Instantiate(itemObj, transform);
        }
    }
}
