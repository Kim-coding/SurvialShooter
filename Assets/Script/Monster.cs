using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : LivingEntity
{
    public LayerMask target;

    public ParticleSystem hitEffect;
    public AudioClip hitSound;
    public AudioClip deathSound;

    private LivingEntity targetEntity;
    private Animator animator;
    private AudioSource audioSource;

    private NavMeshAgent nav;
    
    public float damage;
    public float AttackTime = 0.5f;
    private float lastAttckTime;

    private bool hasTarget
    {
        get
        {
            if(targetEntity != null && !targetEntity.dead)
            {
                return true;
            }
            return false;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        targetEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingEntity>();
        
    }

    private void Start()
    {
        StartCoroutine(UdatePath());
    }

    private void Update()
    {
        animator.SetBool("HasTarget", true);
    }

    private IEnumerator UdatePath()
    {
        while(!dead) 
        {
            if(hasTarget) 
            {
                nav.isStopped = false;
                nav.SetDestination(targetEntity.transform.position);
                
            }
            else
            {
                nav.isStopped=true;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (dead)
            return;

        if(Time.time > AttackTime + lastAttckTime)
        {
            LivingEntity entity = other.GetComponent<LivingEntity>();
            if (entity == targetEntity && entity != null)
            {
                var pos = transform.position;
                pos.y += 1f;
                var hitPoint = other.ClosestPoint(pos);
                var hitNomal = transform.position - other.transform.position;

                entity.OnDamage(damage, hitPoint, hitNomal.normalized);

                lastAttckTime = Time.time;
            }
        }
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);

        hitEffect.transform.position = hitPoint;
        hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
        hitEffect.Play();

        audioSource.PlayOneShot(hitSound);
    }

    public override void onDie()
    {
        base.onDie();
        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }

        nav.isStopped = true;
        audioSource.PlayOneShot(deathSound);
        animator.SetTrigger("Death");
    }

    void StartSinking()
    {
        StartCoroutine(Sink());
    }

    IEnumerator Sink()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
