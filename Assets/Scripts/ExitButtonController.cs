using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitButtonController : MonoBehaviour {

    public AudioSource buttonClick;

	public void LoadStage()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Level Select");
        Time.timeScale = 1;
        GameManager.Instance.AdvertisementCheck(1);
        GameManager.Instance.backgroundMusic.Play();
    }
}
