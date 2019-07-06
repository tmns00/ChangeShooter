using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int BossHP = 100; //HP

    public GameObject shield; //装甲
    public float shieldInterval = 5; //装甲がはがれてからの再生までの時間
    private bool isShield; //装甲があるかどうか

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        isShield = false;
        slider.maxValue = BossHP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = BossHP;

        if (BossHP <= 0)
            Dead();

        //Debug.Log(isShield);

        //装甲がなければ、生成のコルーチン開始
        if (!isShield)
        {
            StartCoroutine(Guard());
        }
    }

    /// <summary>
    /// オブジェクトが侵入してきたときのメソッド
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーの弾
        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);

            BossHP -= 1;
        }
    }

    /// <summary>
    /// 死亡時のメソッド
    /// </summary>
    void Dead()
    {
        Destroy(gameObject);

        var trackingMissiles = GameObject.FindGameObjectsWithTag("Tracking");

        if (trackingMissiles == null)
            return;

        foreach (var missile in trackingMissiles)
        {
            Destroy(missile);
        }
    }

    /// <summary>
    /// 装甲の再生コルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator Guard()
    {
        isShield = true;
        //フラグを反転させてから２秒後に装甲を張る
        //先に反転させないと多重に装甲が張られるため注意
        yield return new WaitForSeconds(shieldInterval);
        Instantiate(shield, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// フラグの反転メソッド
    /// </summary>
    /// <param name="flag"></param>
    public void ShieldFlag(bool flag)
    {
        isShield = flag;
    }


    //public void SetSlider(Slider slider)
    //{
    //    this.slider = slider;
    //    this.slider.maxValue = BossHP;
    //}
}
