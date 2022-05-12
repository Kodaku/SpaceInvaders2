using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/Resources/Save.txt");
        streamWriter.WriteLine(2); // Level
        streamWriter.WriteLine(0); // Score
        streamWriter.WriteLine(10); // Lives
        print("Written on file");
        streamWriter.Close();
        AssetDatabase.ImportAsset("Assets/Resources/Save.txt");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        TextAsset sr = (TextAsset)Resources.Load("Save");
        string[] fileContent = sr.text.Split('\n');
        int currentLevel = int.Parse(fileContent[0]);
        //print(currentLevel);
        if(currentLevel < SceneManager.sceneCountInBuildSettings)
        {
            print(SceneManager.sceneCountInBuildSettings);
            print("OK");
            SceneManager.LoadScene(currentLevel);
        }
        else
        {
            NewGame();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
