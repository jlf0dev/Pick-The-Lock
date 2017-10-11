using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	void Start ()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        transform.localScale = new Vector2(width / GetComponent<SpriteRenderer>().bounds.size.x, 1);
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, 10));
    }

}
