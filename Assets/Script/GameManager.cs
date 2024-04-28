using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public bool isGameOver { get; private set; }

    public static GameManager instance
    {
        get
        {
            if(singletonInstance == null)
            {
                singletonInstance = FindAnyObjectByType<GameManager>();
            }

            return singletonInstance;
        }
    }

    private static GameManager singletonInstance;

    private void Awake()
    {
        if (instance != this) 
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameOver)
        {
            score += newScore;

            UIManager.instance.UpdateScore(score);
        }
    }

    private void EndGame()
    {
        isGameOver = true;
    }
}
