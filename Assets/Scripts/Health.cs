using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth = 3;
    [SerializeField] protected float currentHealth;

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void ChangeHealth(float amount)
    {
        currentHealth = currentHealth + amount;
        CheckHealth();
    }

    protected virtual void CheckHealth()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Kill();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public virtual void Kill()
    {

    }
}
