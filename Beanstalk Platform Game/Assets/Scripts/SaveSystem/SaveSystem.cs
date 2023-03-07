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
            SaveAll(1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Load("Save1");
        }
    }

    public void SaveAll(int saveNum)
    {
        save.currentSave_save = currentSave;
        save.currentLevel_save= currentLevel;

        WriteSave(saveNum);
    }

    public void WriteSave(int saveNum)
    {
        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
        string path = Application.persistentDataPath + "/Save" + saveNum + ".json";
        File.WriteAllText(path, json);
    }



    public void Load(string saveName)
    {
        save = ReadFile(saveName);
    }

    public SaveFile ReadFile(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        string json = File.ReadAllText(path);
        Debug.Log($"ReadFile() {json}");
        SaveFile load = JsonUtility.FromJson<SaveFile>(json);
        return load;
    }



    public informationFile getInformation()
    {
        //Save 1
        string path = Application.persistentDataPath + "/Save1.json";

        string json = File.ReadAllText(path);
        SaveFile information = JsonUtility.FromJson<SaveFile>(json);
        infoFile.save1Level_info = information.currentLevel_save;

        //Save 2
        path = Application.persistentDataPath + "/Save2.json";
        if(File.Exists(path))
        {
            json = File.ReadAllText(path);
            information = JsonUtility.FromJson<SaveFile>(json);
            infoFile.save2Level_info = information.currentLevel_save;
        } else
        {
            infoFile.save2Level_info = 0;
        }

        //Save 3
        path = Application.persistentDataPath + "/Save3.json";
        if (System.IO.File.Exists(path))
        {
            json = File.ReadAllText(path);
            information = JsonUtility.FromJson<SaveFile>(json);
            infoFile.save3Level_info = information.currentLevel_save;
        }
        else
        {
            infoFile.save3Level_info = 0;
        }

        Debug.Log(infoFile);
        return infoFile;
    }



    [System.Serializable]
    public class SaveFile
    {
        public int currentSave_save;
        public int currentLevel_save;
        public Vector3 playerPos_sace;
    }

    [System.Serializable]
    public class informationFile
    {
        public int save1Level_info;

        public int save2Level_info;

        public int save3Level_info;

    }
}
