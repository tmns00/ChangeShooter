using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int BossHP = 100;

    public GameObject shield;
    public float shieldInterval = 5;
    private bool isShield;
    

    // Start is called before the first frame update
    void Start()
    {
        isShield = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHP <= 0)
            Dead();

        Debug.Log(isShield);

        if (!isShield)
        {
            StartCoroutine(Guard());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);

            BossHP -= 1;
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }

    IEnumerator Guard()
    {
        isShield = true;
        yield return new WaitForSeconds(shieldInterval);
        Instantiate(shield, transform.position, Quaternion.identity);
    }

    public void ShieldFlag(bool flag)
    {
        isShield = flag;
    }
}
