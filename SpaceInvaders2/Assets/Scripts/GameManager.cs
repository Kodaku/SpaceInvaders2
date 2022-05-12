using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies;
}

[System.Serializable]
public class WavesList
{
    public List<Wave> waves;
}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public GameObject player;
    public GameObject lifeObject;
    public GameObject machineGunObject;
    public bool isFinalLevel;
    private float objectTimer = 5.0f;
    private float currentObjectTimer = 0.0f;
    public WavesList waves = new WavesList();
    private Wave currentWave;
    private int currentLevel;
    private Player playerScript;

    private static bool isMovementEnabled;
    private static bool staticIsFinalLevel;

    public static bool IsMovementEnabled { get => isMovementEnabled; set => isMovementEnabled = value; }
    public static bool IsFinalLevel { get => staticIsFinalLevel; set => staticIsFinalLevel = value; }

    public static GameManager Instance { get => instance; }

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
    // Start is called before the first frame update
    void Start()
    {
        staticIsFinalLevel = isFinalLevel;
        isMovementEnabled = false;
        TextAsset sr = (TextAsset)Resources.Load("Save");
        string[] fileContent = sr.text.Split('\n');
        currentLevel = int.Parse(fileContent[0]);
        print(currentLevel);
        int currentScore = int.Parse(fileContent[1]);
        int playerLives = int.Parse(fileContent[2]);
        ScoreManager.Instance.Score = currentScore;
        ScoreManager.Instance.UpdateScore(0);
        playerScript = Instantiate(player, new Vector2(14, 2), Quaternion.identity).GetComponent<Player>();
        playerScript.Lives = playerLives;

        currentWave = waves.waves[0];
        waves.waves.RemoveAt(0);
        HideWaves();
    }

    private void OnEnable()
    {
        EventHandler.DestroyEnemyEvent += DestroyEnemy;
    }

    private void OnDisable()
    {
        EventHandler.DestroyEnemyEvent -= DestroyEnemy;
    }

    public void LoseGame()
    {
        CompleteLevel.Instance.OnLose();
    }

    private void DestroyEnemy(GameObject enemy)
    {
        currentWave.enemies.Remove(enemy);
        if (currentWave.enemies.Count == 0)
        {
            if (waves.waves.Count == 0)
            {
                CompleteLevel.Instance.OnLevelComplete();
                StartCoroutine(NextLevel());
            }
            else
            {
                currentWave = waves.waves[0];
                waves.waves.RemoveAt(0);
                ShowNextWave();
            }
        }
    }

    private void HideWaves()
    {
        for(int i = 0; i < waves.waves.Count; i++)
        {
            Wave wave = waves.waves[i];
            for(int j = 0; j < wave.enemies.Count; j++)
            {
                wave.enemies[j].SetActive(false);
            }
        }
    }

    private void ShowNextWave()
    {
        foreach(GameObject enemy in currentWave.enemies)
        {
            enemy.SetActive(true);
        }
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2.0f);
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        print(currentLevel);
        StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/Resources/Save.txt");
        streamWriter.WriteLine(currentLevel.ToString());
        streamWriter.WriteLine(ScoreManager.Instance.Score.ToString());
        streamWriter.WriteLine(playerScript.Lives.ToString());
        print("Written on file");
        streamWriter.Close();
        AssetDatabase.ImportAsset("Assets/Resources/Save.txt");
        if (currentLevel < SceneManager.sceneCountInBuildSettings)
        {
            print("OK");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentObjectTimer += Time.deltaTime;
        if(currentObjectTimer >= objectTimer)
        {
            currentObjectTimer = 0.0f;
            int spawnX = Random.Range(2, 28);
            int spawnY = 18;
            Vector2 spawnPoint = new Vector2(spawnX, spawnY);
            int choice = Random.Range(0, 2);
            if(choice == 0)
            {
                Instantiate(lifeObject, spawnPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(machineGunObject, spawnPoint, Quaternion.identity);
            }
        }
    }
}
