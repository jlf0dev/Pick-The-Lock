using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChamberController : MonoBehaviour {

    public bool tumblerCheck = false;
    public bool pauseCheck = false;
    public bool doneCheck = false;

    public GameObject end;
    public GameObject next;
    public GameObject timer;
    public GameObject handle;
    public GameObject instructions;
    public GameObject pick;

    public AudioSource endAudio0;
    public AudioSource endAudio1;
    public AudioSource endAudio2;
    public AudioSource endAudio3;
    void Awake()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Camera.main.aspect;

        transform.localScale = new Vector2((width * .3878f) / GetComponent<SpriteRenderer>().bounds.size.x, 1);

        Vector3 k = GetComponent<SpriteRenderer>().bounds.size;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, 10));
        transform.position = new Vector3(transform.position.x - (k.x / 2), transform.position.y + (k.y / 2));
    }

    void Start()
    {
        foreach (Tumbler tumbler in GameManager.Instance.levelData)
        {
            GameObject slot = (GameObject)Instantiate(Resources.Load("Tumbler Slot"), new Vector3(tumbler.x, tumbler.slotY, 0), Quaternion.identity);
            slot.name = "Slot " + tumbler.ID;
            slot.transform.parent = GameObject.Find("Tumbler Slots").transform;

            GameObject anim = (GameObject)Instantiate(Resources.Load("Tumbler Slot Animation"), new Vector3(tumbler.x, tumbler.slotY, 0), Quaternion.identity);
            anim.name = "Animation " + tumbler.ID;
            anim.transform.parent = slot.transform.parent;

            string botInstance = "";
            switch (tumbler.bottomScale)
            {
                case 0:
                    botInstance = "Bottom Tumbler";
                    break;
                case 1:
                    botInstance = "Bottom Tumbler X.5";
                    break;
                case 2:
                    botInstance = "Bottom Tumbler X.25";
                    break;
            }
            GameObject bottom = (GameObject)Instantiate(Resources.Load(botInstance), new Vector3(tumbler.x, tumbler.bottomY, 0), Quaternion.identity);
            bottom.GetComponent<Animator>().SetInteger("Scale", tumbler.bottomScale);
            switch (tumbler.bottomWeight)
            {
                case 0:
                    bottom.GetComponent<SpriteRenderer>().color = GameManager.Instance.lightColor;
                    bottom.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
                    break;
                case 2:
                    bottom.GetComponent<SpriteRenderer>().color = GameManager.Instance.heavyColor;
                    bottom.GetComponent<Rigidbody2D>().gravityScale = 50f;
                    break;
            }
            bottom.name = "Bottom Tumbler " + tumbler.ID;
            bottom.transform.parent = GameObject.Find("Bottom Tumblers").transform;

            GameObject top = (GameObject)Instantiate(Resources.Load("Top Tumbler"), new Vector3(tumbler.x, tumbler.topY, 0), Quaternion.identity);
            top.name = "Top Tumbler  " + tumbler.ID;
            if (tumbler.topFall == 1)
            {
                top.GetComponent<TopTumblerDetection>().topFall = true;
            }
            top.transform.SetParent(GameObject.Find("Top Tumblers").transform);
        }
        gameObject.GetComponent<SpriteRenderer>().color = GameManager.Instance.chamberColor;
        handle.GetComponent<SpriteRenderer>().color = GameManager.Instance.chamberColor;
        if (GameManager.Instance.levelData.ID == "L_1")
        {
            instructions.SetActive(true);
            GameObject pickGlow = (GameObject)Instantiate(Resources.Load("Pick Glow"), pick.transform.position, Quaternion.identity);
            pickGlow.name = "Pick Glow";
            pick.GetComponent<PickController>().glow = true;
        }
    }

    public void End()
    {
        end.SetActive(true);
        Time.timeScale = 0;

        int stars = timer.GetComponent<TimerController>().stars;
        switch (stars)
        {
            case 0:
                endAudio0.Play();
                GameObject.Find("Star 1").SetActive(false);
                GameObject.Find("Star 2").SetActive(false);
                GameObject.Find("Star 3").SetActive(false);
                if (GameManager.Instance.CheckNextLevelOpen() && GameManager.Instance.levelData.ID != "L_24")
                {
                    GameObject.Find("Next Dark").SetActive(false);
                    GameObject.Find("Next").GetComponent<Button>().interactable = true;
                }
                break;

            case 1:
                endAudio1.Play();
                GameObject.Find("Star 1").SetActive(true);
                GameObject.Find("Star 2").SetActive(false);
                GameObject.Find("Star 3").SetActive(false);
                if (GameManager.Instance.levelData.ID != "L_24")
                {
                    GameObject.Find("Next Dark").SetActive(false);
                    GameObject.Find("Next").GetComponent<Button>().interactable = true;
                }
                GameManager.Instance.FinishLevel(1);
                break;

            case 2:
                endAudio2.Play();
                GameObject.Find("Star 1").SetActive(true);
                GameObject.Find("Star 2").SetActive(true);
                GameObject.Find("Star 3").SetActive(false);
                if (GameManager.Instance.levelData.ID != "L_24")
                {
                    GameObject.Find("Next Dark").SetActive(false);
                    GameObject.Find("Next").GetComponent<Button>().interactable = true;
                }
                GameManager.Instance.FinishLevel(2);
                break;

            case 3:
                endAudio3.Play();
                GameObject.Find("Star 1").SetActive(true);
                GameObject.Find("Star 2").SetActive(true);
                GameObject.Find("Star 3").SetActive(true);
                if (GameManager.Instance.levelData.ID != "L_24")
                {
                    GameObject.Find("Next Dark").SetActive(false);
                    GameObject.Find("Next").GetComponent<Button>().interactable = true;
                }
                GameManager.Instance.FinishLevel(3);
                break;
        }
    }
}
