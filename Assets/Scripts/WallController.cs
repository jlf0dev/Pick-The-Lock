using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	void Start ()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        transform.localScale = new Vector2(width / (18.79f - 1f), 1); //-1 is for size of walls
    }
	
}
