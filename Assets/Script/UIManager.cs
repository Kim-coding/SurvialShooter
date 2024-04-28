using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image option;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
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

    public void ReSume()
    {
        option.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        
        #endif
        Application.Quit();
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = "Score : " + newScore;
    }

}
