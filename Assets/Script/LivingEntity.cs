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

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            onDie();
        }
    }

    public virtual void onDie()
    {
        dead = true;
    }

}
