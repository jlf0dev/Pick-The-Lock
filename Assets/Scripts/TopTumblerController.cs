using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopTumblerController : MonoBehaviour
{

    GameObject chamber;
    public List<Transform> children;
    public List<Transform> tumblersUp;

    public float maxSpeedUp;
    public Coroutine co;
    public bool isDone = false;


    void Start()
    {
        chamber = GameObject.Find("Chamber");

        transform.localScale = chamber.transform.localScale;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.53f, .4963f, 10));

        children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        tumblersUp = new List<Transform>();
    }

    public void TumblerCheck(Transform target, bool topFall)
    {
        if (tumblersUp.Contains(target))
        {
            tumblersUp.Remove(target);
            chamber.GetComponent<ChamberController>().tumblerCheck = false;
            if (topFall)
            {
                StopCoroutine(co);
            }
        }
        else
        {
            tumblersUp.Add(target);
            if (topFall)
            {
                co = StartCoroutine(fallTimer(target));
            }
            if (tumblersUp.Count == children.Count)
            {
                chamber.GetComponent<ChamberController>().tumblerCheck = true;

            }

        }
    }

    public IEnumerator fallTimer(Transform target)
    {
        yield return new WaitForSeconds(3);
        if (!chamber.GetComponent<ChamberController>().doneCheck)
        {
            tumblersUp.Remove(target);
            chamber.GetComponent<ChamberController>().tumblerCheck = false;
            target.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }


}