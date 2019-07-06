using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MidBoss : MonoBehaviour
{
    public int midBossHP = 50;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        //slider.maxValue = midBossHP;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = midBossHP;

        if (midBossHP <= 0)
            Dead();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="PlayerBullet")
        {
            Destroy(other.gameObject);
            midBossHP -= 1;
        }
    }

    void Dead()
    {
        Destroy(gameObject);

        var trackingMissiles = GameObject.FindGameObjectsWithTag("Tracking");

        if (trackingMissiles == null)
            return;

        foreach(var missile in trackingMissiles)
        {
            Destroy(missile);
        }
    }


    public void SetSlider(Slider slider)
    {
        this.slider = slider;
        this.slider.maxValue = midBossHP;
    }
}
