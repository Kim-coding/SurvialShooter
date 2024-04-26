using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;

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

    protected override void OnEnable()
    {
        base.OnEnable();
        nav.enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        StartCoroutine(UdatePath());
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
        
        hitEffect.Play();
        audioSource.PlayOneShot(hitSound);
    }

    public override void OnDie()
    {
        base.OnDie();
        var cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols)
        {
            col.enabled = false;
        }

        nav.isStopped = true;
        audioSource.PlayOneShot(deathSound);

        animator.SetTrigger("Death");
        UIManager.instance.AddScore(10);
    }

    void StartSinking()
    {
        StartCoroutine(Sink());
    }

    IEnumerator Sink()
    {
        nav.enabled = false ;

        float speed = 0.5f;
        float time = 2f;

        float timer = 0;
        while (timer < time)
        {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
