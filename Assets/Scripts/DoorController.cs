using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {

    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        transform.localScale = new Vector2((width * .4f) / GetComponent<SpriteRenderer>().bounds.size.x, 1);
        Vector3 k = GetComponent<SpriteRenderer>().bounds.size;

        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1f, .5f, 10));
        transform.position = new Vector3(transform.position.x - (k.x / 2), transform.position.y);
    }

}
