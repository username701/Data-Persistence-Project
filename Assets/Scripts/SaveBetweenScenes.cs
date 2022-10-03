using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveBetweenScenes : MonoBehaviour
{
    public static SaveBetweenScenes Instance;
    private MainManager mainManagerScript;

    public string input;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ReadStringInput(string s)
    {
        input = s;
    }

    [System.Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();

        mainManagerScript = GameObject.Find("MainManager").GetComponent<MainManager>();

        data.bestPlayerName = mainManagerScript.bestPlayerName;
        data.bestScore = mainManagerScript.bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            mainManagerScript = GameObject.Find("MainManager").GetComponent<MainManager>();

            mainManagerScript.bestScore = data.bestScore;
            mainManagerScript.bestPlayerName = data.bestPlayerName;
        }
    }
}
