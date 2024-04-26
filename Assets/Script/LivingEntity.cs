using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float starthealth = 100f;
    public float health {  get; private set; }
    public bool dead {  get; private set; }

    protected virtual void OnEnable()
    {
        dead = false;
        health = starthealth;
    }

    public void ApplyUpdateHealth(float newHealth, bool newDead)
    {
        health = newHealth;
        dead = newDead;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {

        health -= damage;
        if(health <= 0)
        {
            health = 0;
            OnDie();
        }
        ApplyUpdateHealth(health,dead);
    }

    public virtual void OnDie()
    {
        dead = true;
    }

}
