using UnityEngine;
using System.Collections;

public class LevelSelectButtonController : MonoBehaviour {

    public string ID;
    AudioSource buttonClick;

    void Start()
    {
        buttonClick = GameObject.Find("Back Button").GetComponent<AudioSource>();
    }

    public void LoadStage()
    {
        buttonClick.Play();
        GameManager.Instance.LoadXML(ID);
        GameManager.Instance.AdvertisementCheck(1);
        GameManager.Instance.backgroundMusic.Stop();
    }
}
