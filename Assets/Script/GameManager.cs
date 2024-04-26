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
            //�ɼ� â ����
        }
    }

    public void AddScore(int newScore)
    {
        if (!isGameOver)
        {
            score += newScore;
            Debug.Log(score);
            UIManager.instance.UpdateScore(score);
        }
    }

    private void EndGame()
    {
        isGameOver = true;

        // +���ӿ��� ȭ������ �̵�
    }
}
