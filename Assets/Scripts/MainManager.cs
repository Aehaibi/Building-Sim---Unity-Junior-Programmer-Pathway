using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }

    public Color TeamColor;

    private void Awake()
    {
        //this pattern is called a "singleton". Use it to ensure that only a single instance of the MainManager
        //can ever exist, acting as a central point of access
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //you can call MainManager.Instance from any other script, no ref needed like when assigning
        //a GameObject to script properties in the Inspector
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    [System.Serializable]
    class SaveData 
    {
        public Color TeamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    //this method is a reversal of the SaveColor method
    public void LoadColor() 
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)) 
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}
