using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance;

    public void AddScore(int newscore)
    {
        score += newscore;
        UpdateScore(score);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

}
