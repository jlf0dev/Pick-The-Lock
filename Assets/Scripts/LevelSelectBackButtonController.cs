using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectBackButtonController : MonoBehaviour {

    public TextAsset xmlFile;
    AudioSource buttonClick;
	
    void Start()
    {
        buttonClick = GetComponent<AudioSource>();
    }

    public void LoadStage()
    {
        buttonClick.Play();
        SceneManager.LoadScene("Menu");
    }
}
