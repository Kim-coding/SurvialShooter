using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;
    public Image gameOverImage;
    public TextMeshProUGUI gameOverText;

    public AudioClip deathClip;
    public AudioClip hitClip;

    private Animator playerAnimator;

    private PlayerMovement playerMovement;
    private Gun gun;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        gun = GetComponentInChildren<Gun>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        healthSlider.minValue = 0f;
        healthSlider.maxValue = starthealth;
        healthSlider.value = health;

        gameOverImage.color = new Color(gameOverImage.color.r,gameOverImage.color.g,gameOverImage.color.b,0);
        gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b,0);

        playerMovement.enabled = true;
        gun.enabled = true;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if(dead) return;

        base.OnDamage(damage, hitPoint, hitNormal);

        healthSlider.value = health;
        AudioManager.instance.effectPlay(hitClip);
    }

    public override void OnDie()
    {
        base.OnDie();

        playerAnimator.SetTrigger("Death");
        AudioManager.instance.effectPlay(deathClip);

        playerMovement.enabled = false;
        gun.enabled = false;
    }

    void RestartLevel()
    {
        StartCoroutine(GameOverUI());   
    }

    private IEnumerator GameOverUI()
    {
        float alpha = 0f;
        float maxAlpha = 1f;

        while (alpha < maxAlpha)
        {
            alpha += Time.deltaTime;
            gameOverImage.color = new Color(gameOverImage.color.r, gameOverImage.color.g, gameOverImage.color.b, alpha);
            gameOverText.color = new Color(gameOverText.color.r, gameOverText.color.g, gameOverText.color.b, alpha);

            yield return null;
        }

        Invoke("ReStart", 3f);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Main");
    }
}
