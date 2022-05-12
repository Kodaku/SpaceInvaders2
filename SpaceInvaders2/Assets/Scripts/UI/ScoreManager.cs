using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager m_instance;
    public Text scoreText;
    private int score = 0;

    public static ScoreManager Instance { get => m_instance; set => m_instance = value; }

    public int Score { get => score; set => score = value; }

    private void Awake()
    {
        if(m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
}
