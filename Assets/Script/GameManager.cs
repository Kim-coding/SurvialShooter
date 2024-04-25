using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public bool isGameOver { get; private set; }
    private static GameManager singletonInstance;

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

    public void Awake()
    {
        if(instance != null) 
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //옵션 창 띄우기
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameOver)
        {
            
            score += newScore;
            
            //UIManager.instance.UpdateScoreText(score);
        }
    }

    private void EndGame()
    {
        isGameOver = true;

        // +게임오버 화면으로 이동
    }
}
