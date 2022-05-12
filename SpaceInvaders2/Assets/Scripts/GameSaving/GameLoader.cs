using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset sr = (TextAsset)Resources.Load("Save");
        string[] fileContent = sr.text.Split('\n');
        int currentLevel = int.Parse(fileContent[0]);
        SceneManager.LoadScene(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
