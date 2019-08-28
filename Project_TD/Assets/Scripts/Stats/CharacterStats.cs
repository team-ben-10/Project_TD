using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    protected float health;
    public float MaxHealth;
    public float Health { get => health; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = MaxHealth;
    }

    public virtual bool TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    public virtual void Die()
    {

    }

    public virtual void Heal(float amount)
    {
        health = Mathf.Min(health + amount, MaxHealth);
    }
}
