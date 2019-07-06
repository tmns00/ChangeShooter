using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawnSystem : MonoBehaviour
{
    public GameObject mainCamera;
    private CameraMove cameraMove;

    public GameObject boss;
    //private Boss bossScrp;
    private MidBoss mBossScrp;

    private bool canInstantiate;
    private bool isVisible;

    public GameObject hpUI;
    private Slider slider; 

    void Start()
    {
        cameraMove = mainCamera.GetComponent<CameraMove>();

        mBossScrp = boss.GetComponent<MidBoss>();

        canInstantiate = true;
        isVisible = false;

        slider = hpUI.transform.Find("BossHPBar").gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        if (isVisible && canInstantiate)
        {
            StartCoroutine(BossSpawn());
            canInstantiate = false;
        }
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }


    IEnumerator BossSpawn()
    {
        hpUI.SetActive(true);

        Instantiate(boss, transform.position, Quaternion.identity);
        mBossScrp.SetSlider(slider);

        yield return new WaitForSeconds(3);

        cameraMove.IsMove(false);
    }
}
