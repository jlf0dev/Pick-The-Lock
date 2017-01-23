using UnityEngine;
using System.Collections;

public class TumblerSlotController : MonoBehaviour {
    private GameObject chamber;

	void Start ()
    {
        chamber = GameObject.Find("Chamber");

        transform.localScale = chamber.transform.localScale;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.53f, .4963f, 10));

    }

}
