using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
class SaveData
{
    public string playerName;
    public int score;
}
public class GameScore : MonoBehaviour
{
    public static GameScore instance;
    public string playerName {get;  set;}
    public string activePlayer;
    public int score {get;  set;}
    // Start is called before the first frame update
    private void Awake() {
        score = 0;
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveDataPlayer()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.score = score;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath+ "/saveData.json", json);
    }
    public void LoadDataPlayer()
    {
        string path = Application.persistentDataPath + "/saveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playerName = data.playerName;
            score = data.score;
        }
    }
}
