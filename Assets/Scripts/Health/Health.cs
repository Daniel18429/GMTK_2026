using UnityEngine;
using System;
public class Health
{
    private float _currentHealth;
    private float _maxHealth;

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    protected virtual void Die()
    {
        
    }
}
