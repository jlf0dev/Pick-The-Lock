using UnityEngine;
using System.Collections;

public class PauseButtonController : MonoBehaviour {

    public GameObject pause;
    public GameObject timer;
    public GameObject chamber;
    public GameObject topTumbler;

	public void OpenPause()
    {
        GetComponent<AudioSource>().Play();
        pause.SetActive(true);
        timer.GetComponent<TimerController>().paused = true;
        chamber.GetComponent<ChamberController>().pauseCheck = true;
        foreach (Transform child in topTumbler.GetComponent<TopTumblerController>().children)
        {
            child.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        Time.timeScale = 0;

    }
}
