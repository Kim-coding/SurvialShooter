using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;

    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;

    private PlayerMovement playerMovement;
    //private Gun gun;

    private void Awake()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();

        playerMovement = GetComponent<PlayerMovement>();
        //gun = GetComponent<Gun>();
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        healthSlider.minValue = 0f;
        healthSlider.maxValue = starthealth;
        healthSlider.value = health;

        playerMovement.enabled = true;
        //gun.enabled = true;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) 
        {
            OnDamage(10);
        }
    }

    public override void OnDamage(float damage)
    {
        if(dead) return;

        base.OnDamage(damage);

        healthSlider.value = health;
        playerAudioPlayer.PlayOneShot(hitClip);
    }

    public override void onDie()
    {
        base.onDie();

        playerAnimator.SetTrigger("Death");

        playerMovement.enabled = false;
        //gun.enabled = false;

        Invoke("ReSpawn",5f);
    }

    public void ReSpawn()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
