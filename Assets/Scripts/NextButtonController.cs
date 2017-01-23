using UnityEngine;


public class NextButtonController : MonoBehaviour {

    public AudioSource buttonClick;

    public void NextStage()
    {
        buttonClick.Play();
        GameManager.Instance.LoadXML(GameManager.Instance.NextLevel(GameManager.Instance.levelData.ID));
        Time.timeScale = 1;
        GameManager.Instance.AdvertisementCheck(1);
    }
}
