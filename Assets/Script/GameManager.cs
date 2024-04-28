using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public Image option;
    public bool isGameOver { get; private set; }
    public AudioClip bgm;
    private AudioSource audioSource;

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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.loop = true; // 반복 재생 설정
        audioSource.Play(); // BGM 재생 시작
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f)
            {
                option.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                option.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
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

        // +게임오버 화면으로 이동
    }
}
