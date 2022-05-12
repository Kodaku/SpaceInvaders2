using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    private static CompleteLevel instance;
    public GameObject winText;
    public GameObject loseText;
    public GameObject gameCompleteText;

    public static CompleteLevel Instance { get => instance; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        gameCompleteText.SetActive(false);
    }

    public void OnLevelComplete()
    {
        if (GameManager.IsFinalLevel)
        {
            gameCompleteText.SetActive(true);
        }
        else
        {
            winText.SetActive(true);
        }
    }

    public void OnLose()
    {
        loseText.SetActive(true);
        StartCoroutine(BackToMenu());
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
