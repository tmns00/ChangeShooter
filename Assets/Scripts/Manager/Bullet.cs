using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    int bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.right.normalized * bulletSpeed;
    }

    Rect rect = new Rect(0, 0, 1, 1); // 画面内かどうかの判定

    // Update is called once per frame
    void FixedUpdate()
    {
        //位置取得
        Vector2 pos = transform.position;

        var viewportPos = Camera.main.WorldToViewportPoint(pos);

        if (!rect.Contains(viewportPos))
        {
            Destroy(gameObject);
        }
    }
}
