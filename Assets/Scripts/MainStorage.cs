using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class MainStorage : MonoBehaviour
{

    public static MainStorage Instance;
    public string playerName;
    public string bestPlayer;
    public int bestScore;

    private void Awake()
    {
        LoadScore();

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class BestScore
    {
        public string player;
        public int score;
    }

    [System.Serializable]
    class SaveData
    {
        public BestScore bestScore;
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();
        data.bestScore = new BestScore();
        data.bestScore.player = bestPlayer;
        data.bestScore.score = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.bestScore.player;
            bestScore = data.bestScore.score;
        }
    }
}
