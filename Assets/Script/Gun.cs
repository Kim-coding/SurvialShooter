using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    private float fireDistance = 50f;

    public Transform firePoint;
    public AudioClip shotClip;
    public ParticleSystem fireEffect;

    private float fireInterval = 0.1f;
    private float lastFireTime;

    public PlayerInput playerInput;
    private LineRenderer bulletLine;
    private AudioSource gunAudioPlayer;


    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        bulletLine = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();

        bulletLine.enabled = false;
        bulletLine.positionCount = 2;
    }

    private void OnEnable()
    {
        lastFireTime = 0f;
    }

    private void Update()
    {
        if (playerInput.fire)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (Time.time > lastFireTime + fireInterval)
        {
            lastFireTime = Time.time;
            gunAudioPlayer.PlayOneShot(shotClip);

            var hitPoint = Vector3.zero;
            var ray = new Ray(firePoint.position, firePoint.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, fireDistance))
            {
                hitPoint = hitInfo.point;
                var damagable = hitInfo.collider.GetComponent<IDamageable>();
                if (damagable != null)
                {
                    damagable.OnDamage(damage);
                }
            }
            else
            {
                hitPoint = firePoint.position + firePoint.forward * fireDistance;
                //hitPoint�� ������ ������
            }
            StartCoroutine(Effect(hitPoint));

        }

        IEnumerator Effect(Vector3 hitpoint)
        {
            bulletLine.SetPosition(0, firePoint.position);
            bulletLine.SetPosition(1, hitpoint);

            Debug.Log(firePoint.position);
            Debug.Log(hitpoint);
            bulletLine.enabled = true;

            fireEffect.Play();

            yield return new WaitForSeconds(0.03f);

            bulletLine.enabled = false;
        }
    }
}