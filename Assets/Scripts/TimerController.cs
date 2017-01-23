using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerController : MonoBehaviour {

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public bool startTimer = false;
    public bool paused = false;

    float star3Remove;
    float star2Remove;
    float star1Remove;
    public int stars = 3;

    private Text text;
    public float timer = 0;

	void Start () 
	{
        text = GetComponent<Text>();
        star3Remove = GameManager.Instance.levelData.star3;
        star2Remove = GameManager.Instance.levelData.star2;
        star1Remove = GameManager.Instance.levelData.star1;
	}
	
	void Update () 
	{
        if (!paused)
        {
            if (startTimer)
            {
                if (timer < star2Remove && timer > star3Remove)
                {
                    star3.SetActive(false);
                    stars = 2;
                }
                else if (timer < star1Remove && timer > star2Remove)
                {
                    star2.SetActive(false);
                    stars = 1;
                }
                else if (timer > star1Remove)
                {
                    star1.SetActive(false);
                    stars = 0;
                }
                timer += Time.deltaTime;
                text.text = timer.ToString("f2");

            }
        }
	}
}
