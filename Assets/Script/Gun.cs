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


    private void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        bulletLine = GetComponent<LineRenderer>();

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
            

            var hitPoint = Vector3.zero;
            var ray = new Ray(firePoint.position, firePoint.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, fireDistance))
            {
                hitPoint = hitInfo.point;
                var damagable = hitInfo.collider.GetComponent<IDamageable>();
                if (damagable != null)
                {
                    damagable.OnDamage(damage,hitPoint,hitInfo.normal);
                }
                
            }
            else
            {
                hitPoint = firePoint.position + firePoint.forward * fireDistance;
            }
            StartCoroutine(Effect(hitPoint));

            AudioManager.instance.effectPlay(shotClip);

        }

        IEnumerator Effect(Vector3 hitpoint)
        {
            bulletLine.SetPosition(0, firePoint.position);
            bulletLine.SetPosition(1, hitpoint);

            bulletLine.enabled = true;

            fireEffect.Play();

            yield return new WaitForSeconds(0.05f);

            bulletLine.enabled = false;
        }
    }
}
