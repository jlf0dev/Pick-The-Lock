using UnityEngine;
using System.Collections;

public class ResumeButtonController : MonoBehaviour {

    public GameObject pause;
    public GameObject timer;
    public GameObject chamber;
    public GameObject topTumbler;

    public AudioSource buttonClick;

    public void ClosePause()
    {
        buttonClick.Play();
        pause.SetActive(false);
        timer.GetComponent<TimerController>().paused = false;
        chamber.GetComponent<ChamberController>().pauseCheck = false;
        foreach (Transform child in topTumbler.GetComponent<TopTumblerController>().children)
        {
            if (!topTumbler.GetComponent<TopTumblerController>().tumblersUp.Contains(child))
            {
                child.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
        Time.timeScale = 1;
    }
}
