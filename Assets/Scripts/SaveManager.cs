using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public string username;
    public string highscoreName;
    public int highscore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveScore(string name, int score)
    {
        highscore = score;
        highscoreName = name;
    }

    public void SaveInfo()
    {
        SystemSave data = new SystemSave();
        data.highscore = highscore;
        data.highscoreName = highscoreName;
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadInfo()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SystemSave data = JsonUtility.FromJson<SystemSave>(json);

            highscore = data.highscore;
            highscoreName = data.highscoreName;
        }
    }
}

[System.Serializable]
class SystemSave
{
    public string highscoreName;
    public int highscore;
}
