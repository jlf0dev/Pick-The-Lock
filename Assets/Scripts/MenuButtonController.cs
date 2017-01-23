using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtonController : MonoBehaviour {

    private Vector3 position;
    private Quaternion rot;
    AudioSource buttonClick;

    void Awake ()
    {
        position = this.gameObject.GetComponent<RectTransform>().position;
        rot = this.gameObject.GetComponent<RectTransform>().rotation;
    }

    void Start()
    {
        if (GameManager.Instance.hasPickFallen == true)
        {
            GameObject.Find("Pick").transform.localPosition = new Vector3(-4.077226f, -203.8734f, -3293.159f);
            GameObject.Find("Pick").transform.rotation = Quaternion.identity;
        }
        buttonClick = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        if (position != this.gameObject.GetComponent<RectTransform>().position)
        {
            this.gameObject.GetComponent<RectTransform>().position = position;
        }
        if (rot != this.gameObject.GetComponent<RectTransform>().rotation)
        {
            this.gameObject.GetComponent<RectTransform>().rotation = rot;
        }
    }

    public void LoadStage ()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Level Select");
        GameManager.Instance.levelSelectExitCode = 0;
        GameManager.Instance.hasPickFallen = true;
        GameManager.Instance.AdvertisementCheck(1);
    }
}
