using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using static SaveSystem;

public class SaveSelector : MonoBehaviour
{
    public SaveSystem savesys;
    public SaveSystem.informationFile infoFile;
    public SaveSystem.SaveFile saveFile;

    public GameObject saveSlot1;
    public GameObject saveSlot2;
    public GameObject saveSlot3;

    public List<string> scenes;

    void Start()
    {
        getSceneNames();

        infoFile = savesys.getInformation();

        foreach (Transform child in saveSlot1.transform)
        {
            if(child.name == "Progress")
            {
                if (infoFile.save1Level_info != 0)
                {
                    child.GetComponent<TextMeshProUGUI>().text = scenes[infoFile.save1Level_info];
                } else
                {
                    child.GetComponent<TextMeshProUGUI>().text = "Empty";
                }
            }
        }
        foreach (Transform child in saveSlot2.transform)
        {   
            if (child.name == "Progress")
            {
                if(infoFile.save2Level_info != 0)
                {
                    child.GetComponent<TextMeshProUGUI>().text = scenes[infoFile.save2Level_info];
                } else
                {
                    child.GetComponent<TextMeshProUGUI>().text = "Empty";
                }
            }
        }
        foreach (Transform child in saveSlot3.transform)
        {
            if (child.name == "Progress")
            {
                if (infoFile.save3Level_info != 0)
                {
                    child.GetComponent<TextMeshProUGUI>().text = scenes[infoFile.save3Level_info];
                }
                else
                {
                    child.GetComponent<TextMeshProUGUI>().text = "Empty";
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getSceneNames()
    {
        scenes.Clear();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenes.Add(GetSceneName(i));
        }
    }


    public static string GetSceneName(int buildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }


    public void loadSave(int saveNumber)
    {
        if(System.IO.File.Exists(Application.persistentDataPath + "/Save" + saveNumber + ".json"))
        {
            string path = Application.persistentDataPath + "/Save" + saveNumber + ".json";
            string json = File.ReadAllText(path);
            Debug.Log($"ReadFile() {json}");
            SaveFile load = JsonUtility.FromJson<SaveFile>(json);
            Debug.Log("Save exists");
            if(load.currentLevel_save < 0)
            {
                Debug.Log("save found");
            } else
            {
                Debug.Log("File doesn't exists");
                savesys.newSave(saveNumber);
            }

        } else
        {
            Debug.Log("File doesn't exists");
            savesys.newSave(saveNumber);
        }
    }
}
