using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButtonController : MonoBehaviour {

    public AudioSource buttonClick;

	public void Restart()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Arena");
        Time.timeScale = 1;
        GameManager.Instance.AdvertisementCheck(1);
    }
}
