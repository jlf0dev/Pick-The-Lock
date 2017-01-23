using UnityEngine;
using System.Collections;

public class TopTumblerDetection : MonoBehaviour {

    Rigidbody2D rb2d;
    TopTumblerController top = null;

    public bool topFall = false; 

    void Start()
    {
        top = transform.GetComponentInParent<TopTumblerController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerExit2D(Collider2D other)
    {

        if (rb2d.velocity.y < top.maxSpeedUp && rb2d.velocity.y > 0)
        {
            rb2d.isKinematic = true;
            top.TumblerCheck(transform, topFall);
        }
    }
    

    void OnCollisionEnter2D(Collision2D other)
    {
        if (GetComponent<Rigidbody2D>().isKinematic)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            top.TumblerCheck(transform, topFall);
        }
    }
}
