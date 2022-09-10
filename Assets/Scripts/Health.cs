using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] float _maxHealth;
    [SerializeField] float _health;

    void Awake()
    {
        _health = _maxHealth;  
    }

    public void takeDamage(float amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            print("die");
        }
    }

}
