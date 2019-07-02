using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnSystem : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject boss;
    private CameraMove cameraMove;
    private bool canInstantiate;
    private bool isVisible;

    public GameObject hpUI;

    void Start()
    {
        cameraMove = mainCamera.GetComponent<CameraMove>();
        canInstantiate = true;
        isVisible = false;
    }

    private void Update()
    {
        if (isVisible && canInstantiate)
            StartCoroutine(BossSpawn());
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }


    IEnumerator BossSpawn()
    {
        hpUI.SetActive(true);

        Instantiate(boss, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(3);

        cameraMove.IsMove(false);
    }
}
