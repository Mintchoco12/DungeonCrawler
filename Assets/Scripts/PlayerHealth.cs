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

    protected override void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Kill();
        }
        else
        {
            playerAnimation.SetAnimPlayerState(PlayerState.idle);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public override void Kill()
    {
        base.Kill();
        playerAnimation.SetAnimPlayerState(PlayerState.dead);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("collision");

            playerAnimation.SetAnimPlayerState(PlayerState.hit);
            ChangeHealth(-0.5f);
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
