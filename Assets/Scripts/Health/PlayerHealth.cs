using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    PlayerAnimation playerAnimation;

    public override void Start()
    {
        base.Start();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    public override void ChangeHealth(float amount)
    {
        base.ChangeHealth(amount);
    }

    //Checks hp of player
    protected override void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }

        //If hp is bigger then allowed
        if (currentHealth > maxHealth)
        {
            //Reset hp
            currentHealth = maxHealth;
        }
    }

    public override void Kill()
    {
        base.Kill();
        playerAnimation.TriggerDeathAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAnimation.Hit();
            ChangeHealth(-0.5f);
        }
    }

    //Returns max health
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    //Returns current health
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
