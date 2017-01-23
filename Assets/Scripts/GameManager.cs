using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System;

public class GameManager : MonoBehaviour {

    //Create Singleton Class

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Instantiate (Resources.Load("Game Manager"));
            }
            return instance;
        }
    }

    void Awake()
	{
        instance = this;
        SceneManager.sceneLoaded += LevelSelectLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= LevelSelectLoaded;

        ////Loading Data (Delete before exporting)
        //LevelFileData lfd = new LevelFileData();
        //lfd.saveList.AddRange(openLevels);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Open(Application.persistentDataPath + "/levelData.dat", FileMode.Open);
        //bf.Serialize(file, lfd);
        //file.Close();
    }


    public Dictionary<string, int> openLevels = new Dictionary<string, int>();
    public AudioSource backgroundMusic;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        print("Data Path: " + Application.persistentDataPath);
        
        //Level File Setup

        if (!File.Exists(Application.persistentDataPath + "/levelData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/levelData.dat");
            LevelFileData lfd = new LevelFileData();
            lfd.saveList.Add(new KeyValuePair<string, int>("L_1", 0));
            bf.Serialize(file, lfd);
            file.Close();
            foreach (KeyValuePair<string, int> thing in lfd.saveList)
            {
                openLevels.Add(thing.Key, thing.Value);
            }
        }
        else if (File.Exists(Application.persistentDataPath + "/levelData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/levelData.dat", FileMode.Open);
            LevelFileData lfd = (LevelFileData)bf.Deserialize(file);
            file.Close();
            foreach (KeyValuePair<string, int> thing in lfd.saveList)
            {
                openLevels.Add(thing.Key, thing.Value);
            }
        }
        else
        {
            print("Internal Error: Cannot find levelData.dat");
        }
        backgroundMusic = GetComponent<AudioSource>();
    }

    //Advertisement

    int adCount = 0;

    public void AdvertisementCheck(int i)
    {
        adCount += i;
        if (adCount == 5)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
            adCount = 0;
        }
    }

    //Menu Scene

    public bool hasPickFallen = false;

    //Loading Scene

    public int levelSelectExitCode = 0;
    public TextAsset xmlLevelFile;

    void LevelSelectLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level Select")
        {
            levelData = null;
            xmlLevelFile = GameObject.Find("Back Button").GetComponent<LevelSelectBackButtonController>().xmlFile;
            foreach (KeyValuePair<string, int> kvp in openLevels)
            {
                switch (kvp.Value)
                {
                    case 1:
                        GameObject.Find(kvp.Key + "Star 2").SetActive(false);
                        GameObject.Find(kvp.Key + "Star 3").SetActive(false);
                        break;
                    case 2:
                        GameObject.Find(kvp.Key + "Star 1").SetActive(false);
                        GameObject.Find(kvp.Key + "Star 3").SetActive(false);
                        break;
                    case 3:
                        GameObject.Find(kvp.Key + "Star 1").SetActive(false);
                        GameObject.Find(kvp.Key + "Star 2").SetActive(false);
                        break;
                    default:
                        GameObject.Find(kvp.Key + "Star 1").SetActive(false);
                        GameObject.Find(kvp.Key + "Star 2").SetActive(false);
                        GameObject.Find(kvp.Key + "Star 3").SetActive(false);
                        break;

                }
                GameObject.Find(kvp.Key + "Dark").SetActive(false);
                GameObject.Find(kvp.Key + "Light").GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
            GameObject.Find("Scroll Snap").GetComponent<ScrollSnapRect>().startingPage = levelSelectExitCode;
        }

    }

    //Finish Level, Change Stars, Open Next if Possible

    public void FinishLevel(int stars)
    {
        if (openLevels.ContainsKey(levelData.ID))
        {
            if (openLevels[levelData.ID] < stars)
            {
                openLevels[levelData.ID] = stars;
            }
        }
        else
        {
            if (openLevels[levelData.ID] < stars)
            {
                openLevels.Add(levelData.ID, stars);
            }
        }

        if (openLevels[levelData.ID] >= 1 && !CheckNextLevelOpen() && levelData.ID != "L_24")
        {
            openLevels.Add(NextLevel(levelData.ID), 0);
        }

    }

    //Check For Open Level For Next

    public bool CheckNextLevelOpen()
    {
        if (openLevels.ContainsKey(NextLevel(levelData.ID)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Get Next Level String

    public string NextLevel(string currentID)
    {
        string resultString = Regex.Match(currentID, @"\d+").Value;
        int k = Int32.Parse(resultString);
        ++k;
        string newString = "L_" + k;
        return (newString);
    }

    //Xml

    public LevelData levelData;
    List<string> f1 = new List<string>(new string[] {"1", "2", "3", "4", "5", "6", "7", "8"});
    List<string> f2 = new List<string>(new string[] {"9", "10", "11", "12", "13", "14", "15", "16"});
    List<string> f3 = new List<string>(new string[] { "17", "18", "19", "20", "21", "22", "23", "24" });
    public Color32 chamberColor;
    public Color32 lightColor = new Color32(156, 0, 255, 255);
    public Color32 heavyColor = new Color32(225, 0, 0, 255);

    public void LoadXML(string ID)
    {
        if (xmlLevelFile != null)
        {
            XmlDocument root = new XmlDocument();
            root.LoadXml(xmlLevelFile.text);
            XmlElement elem = root.GetElementById(ID);

            levelData = new LevelData();
            levelData.star1 = float.Parse(elem.Attributes["star1"].Value);
            levelData.star2 = float.Parse(elem.Attributes["star2"].Value);
            levelData.star3 = float.Parse(elem.Attributes["star3"].Value);
            foreach (XmlNode k in elem.ChildNodes)
            {
                Tumbler t = new Tumbler();
                t.ID = k.Attributes["ID"].Value;
                t.x = float.Parse(k.Attributes["x"].Value);
                t.slotY = float.Parse(k.FirstChild.Attributes["y"].Value);
                XmlNode c = k.LastChild;
                t.bottomY = float.Parse(c.PreviousSibling.Attributes["y"].Value);
                t.bottomScale = int.Parse(c.PreviousSibling.Attributes["scale"].Value);
                t.bottomWeight = int.Parse(c.PreviousSibling.Attributes["weight"].Value);
                t.topY = float.Parse(c.Attributes["y"].Value);
                t.topFall = int.Parse(c.Attributes["fall"].Value);

                levelData.tumblers.Add(t);
            }

            foreach (string l in f1)
            {
                if (ID == "L_" + l)
                {
                    chamberColor = new Color32(59, 174, 218, 255);
                    levelSelectExitCode = 0;
                }
            }
            foreach (string l in f2)
            {
                if (ID == "L_" + l)
                {
                    chamberColor = new Color32(40, 185, 94, 255);
                    levelSelectExitCode = 1;
                }
            }
            foreach (string l in f3)
            {
                if (ID == "L_" + l)
                {
                    chamberColor = new Color32(232, 121, 0, 255);
                    levelSelectExitCode = 2;
                }
            }
            levelData.ID = ID;
            SceneManager.LoadScene("Arena");
        }
    }

    void OnApplicationPause()
    {
        LevelFileData lfd = new LevelFileData();
        lfd.saveList.AddRange(openLevels);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/levelData.dat", FileMode.Open);
        bf.Serialize(file, lfd);
        file.Close();
    }

}

//Save Classes

[System.Serializable]
public class Tumbler
{
    public string ID;
    public float x;
    public float slotY;
    public float bottomY;
    public int bottomScale;
    public int bottomWeight;
    public int topFall;
    public float topY;
}

[System.Serializable]
public class LevelData : IEnumerable<Tumbler>
{
    public string ID;
    public float star1;
    public float star2;
    public float star3;
    public List<Tumbler> tumblers = new List<Tumbler>();

    public IEnumerator<Tumbler> GetEnumerator()
    {
        return ((IEnumerable<Tumbler>)tumblers).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Tumbler>)tumblers).GetEnumerator();
    }
}

[System.Serializable]
public class LevelFileData
{
    public List<KeyValuePair<string, int>> saveList = new List<KeyValuePair<string, int>>();


}
