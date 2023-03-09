using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public SaveFile save;
    public informationFile infoFile;

    public int currentSave;
    public int currentLevel;
    public Vector3 playerPos;

    public string[] sceneNames;


    // Start is called before the first frame update


    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void Start()
    {
        save = new SaveFile();
        getInformation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveAll(currentSave);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Load(currentSave);
        }
    }

    public void SaveAll(int saveNum)
    {
        save.currentSave_save = currentSave;
        save.currentLevel_save = currentLevel;
        save.playerPos_save = GameObject.Find("Player").transform.position;

        WriteSave(saveNum);
    }

    public void WriteSave(int saveNum)
    {
        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
        string path = Application.persistentDataPath + "/Save" + saveNum + ".json";
        File.WriteAllText(path, json);
    }



    public void Load(int saveNum)
    {
        save = ReadFile(saveNum);

        SceneManager.LoadScene(save.currentLevel_save);
        currentLevel = save.currentLevel_save;
        currentSave = saveNum;
    }

    public SaveFile ReadFile(int saveNum)
    {
        string path = Application.persistentDataPath + "/Save" + saveNum + ".json";
        string json = File.ReadAllText(path);
        Debug.Log($"ReadFile() {json}");
        SaveFile load = JsonUtility.FromJson<SaveFile>(json);
        return load;
    }



    public informationFile getInformation()
    {
        string path = Application.persistentDataPath;
        string json;
        SaveFile information;

        if(File.Exists(path.ToString() + "/Save1.json")) 
        {
            json = File.ReadAllText(path + "/Save1.json");
            information = JsonUtility.FromJson<SaveFile>(json);
            infoFile.save1Level_info = information.currentLevel_save;
        }

        if (File.Exists(path.ToString() + "/Save2.json"))
        {
            json = File.ReadAllText(path + "/Save2.json");
            information = JsonUtility.FromJson<SaveFile>(json);
            infoFile.save2Level_info = information.currentLevel_save;
        }

        if (File.Exists(path.ToString() + "/Save3.json"))
        {
            json = File.ReadAllText(path + "/Save3.json");
            information = JsonUtility.FromJson<SaveFile>(json);
            infoFile.save3Level_info = information.currentLevel_save;
        }
        Debug.Log(infoFile);
        return infoFile;
    }


    public void newSave(int saveNum)
    {
        save.currentSave_save = saveNum;
        save.currentLevel_save = 3;
        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
        string path = Application.persistentDataPath + "/Save" + saveNum + ".json";
        File.WriteAllText(path, json);

        Load(saveNum);
    }



    [System.Serializable]
    public class SaveFile
    {
        public int currentSave_save;
        public int currentLevel_save;
        public Vector3 playerPos_save;
    }

    [System.Serializable]
    public class informationFile
    {
        public int save1Level_info;

        public int save2Level_info;

        public int save3Level_info;

    }
}
