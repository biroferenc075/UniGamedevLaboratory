

using System.Collections.Generic;
using UnityEngine;

public abstract class Destructible : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int health;

    public virtual void Awake() {
        health = maxHealth;
    }

    public void SetMaxHealth(int amount) => maxHealth = amount;

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }

    }

    public void AddHealth(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public int GetHealth() => health;

    protected abstract void Die();

    protected abstract void LeaveBounds();

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 11) // Bounds
            LeaveBounds();
    }
}
