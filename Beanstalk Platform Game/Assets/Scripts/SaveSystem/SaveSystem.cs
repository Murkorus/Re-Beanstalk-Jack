using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveSystem : MonoBehaviour
{
    public SaveFile save;

    public int unlockedLevelsTest;

    // Start is called before the first frame update
    public void Start()
    {
        save = new SaveFile();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveAll();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Load("Save1");
        }
    }

    public void SaveAll()
    {
        save.unlockedLevels = unlockedLevelsTest;

        WriteSave("Save1");
    }

    public void WriteSave(string saveName)
    {
        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        File.WriteAllText(path, json);
    }



    public void Load(string saveName)
    {
        save = ReadFile(saveName);
        unlockedLevelsTest = save.unlockedLevels;
    }

    public SaveFile ReadFile(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        string json = File.ReadAllText(path);
        Debug.Log($"ReadFile() {json}");
        SaveFile load = JsonUtility.FromJson<SaveFile>(json);
        return load;
    }


    [System.Serializable]
    public class SaveFile
    {
        public int unlockedLevels;
    }
}
