using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickController : MonoBehaviour {

    public GameObject timer;
    public GameObject instructions;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private TargetJoint2D tj2d;
    private Collider2D coll2d;

    private bool firstTouch = false;
    public bool glow = false;

    AudioSource audioClip;

    ArrayList starList = new ArrayList();

    void Start()
    {

        tj2d = transform.GetComponent<TargetJoint2D>();
        coll2d = transform.GetComponent<Collider2D>();

        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        transform.localScale = new Vector2((width * .685f) / GetComponent<SpriteRenderer>().bounds.size.x, 1);
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.281f, .1f, 10));

        audioClip = GetComponent<AudioSource>();

        starList.Add(star1);
        starList.Add(star2);
        starList.Add(star3);

    }

    void Update()
    {
        if (glow)
        {
            GameObject.Find("Pick Glow").transform.position = transform.position;
            GameObject.Find("Pick Glow").transform.localScale = transform.localScale;
        }
    }

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider == coll2d)
        {
            if (!firstTouch)
            {
                timer.GetComponent<TimerController>().startTimer = true;
                if (GameManager.Instance.levelData.ID == "L_1")
                {
                    instructions.SetActive(false);
                    glow = false;
                    Destroy (GameObject.Find("Pick Glow"));
                }
                foreach (GameObject star in starList)
                {
                    star.GetComponent<Image>().enabled = true;
                }
                firstTouch = true;
            }

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = worldPos - transform.position;
            tj2d.enabled = true;
            tj2d.anchor = offset;
        }
    }

    void OnMouseDrag()
    {
        tj2d.target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    void OnMouseUp()
    {
        tj2d.enabled = false;
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.transform.gameObject.tag == "Bottom Tumbler")
        {
            audioClip.Play();
        }
    }




    //void Update()
    //{
    //    foreach (Touch touch in Input.touches)
    //    {

    //        switch (touch.phase)
    //        {
    //            case TouchPhase.Began:
    //                if (!firstTouch)
    //                {
    //                    timer.GetComponent<TimerController>().startTimer = true;
    //                    firstTouch = true;
    //                }
    //                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //                worldPos.z = transform.position.z;
    //                offset = worldPos - transform.position;
    //                break;
    //            case TouchPhase.Moved:
    //                Vector3 curScreenPoint = Input.mousePosition;
    //                Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
    //                curPosition = (curPosition - (transform.position + offset)) * 20;
    //                rb2d.velocity = curPosition;
    //                rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, 20);
    //                break;
    //            case TouchPhase.Ended:

    //                break;
    //        }
    //    }
    //}

}
