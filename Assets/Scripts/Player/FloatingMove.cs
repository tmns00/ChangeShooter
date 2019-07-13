using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingMove : MonoBehaviour
{
    [SerializeField]
    GameObject animObj;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = animObj.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("Fire1"))
        {
            animator.SetTrigger("WarpTrigger");
            Invoke("DoInvizible", 1);
        }
    }

    void DoInvizible()
    {
        var renderComponet = transform.gameObject.GetComponent<Renderer>();
        renderComponet.enabled = !renderComponet.enabled;
    }
}
