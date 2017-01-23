using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HandleController : MonoBehaviour {

    private GameObject chamber;
    public GameObject pick;
    public GameObject timer;
    public GameObject timeText;

    Collider2D coll2d;

    bool onTarget = false;
    Vector2 startPos;
    float comfortZone = 200;
    float swipeMinimum = 100;

    AudioSource audioClip;

    void Start ()
	{
        chamber = GameObject.Find("Chamber");

        transform.localScale = chamber.transform.localScale;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.57f, .82f, 10));

        coll2d = transform.GetComponent<Collider2D>();

        audioClip = GetComponent<AudioSource>();
  }  

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (hit.collider == coll2d)
                    {
                        onTarget = true;
                        startPos = touch.position;
                    }
                    break;

                case TouchPhase.Moved:
                    if (Mathf.Abs(touch.position.x - startPos.x) > comfortZone && onTarget)
                    {
                        onTarget = false;
                    }
                    break;

                case TouchPhase.Ended:
                    if (onTarget)
                    {
                        if (touch.position.y > (startPos.y + swipeMinimum) | touch.position.y < (startPos.y - swipeMinimum))
                        {
                            TurnKnob();
                        }
                    }
                    break;
            }
        }
    }


    // Testing (Delete)

    //void OnMouseDown()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
    //    if (hit.collider == coll2d)
    //    {
    //        TurnKnob();
    //    }
    //}


    public void TurnKnob ()
    {
        if (chamber.GetComponent<ChamberController>().tumblerCheck)
        {
            chamber.GetComponent<ChamberController>().doneCheck = true;
            timer.GetComponent<TimerController>().startTimer = false;
            timeText.GetComponent<Text>().text = timer.GetComponent<TimerController>().timer.ToString("f2") + "s";
            List<GameObject> tg = new List<GameObject>();
            tg.AddRange(GameObject.FindGameObjectsWithTag("Bottom Tumbler"));
            tg.AddRange(GameObject.FindGameObjectsWithTag("Tumbler Slot Animation"));
            tg.Add(chamber);
            pick.SetActive(false);
            audioClip.Play(); //MrAuralization
            foreach (GameObject anim in tg)
            {
                anim.GetComponent<Animator>().SetBool("Fin", true);
            }
        }
    }

}
