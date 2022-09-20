using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] float _maxHealth;
    [SerializeField] float _health;

    public float _currentHealth => _health;

    void Awake()
    {
        _health = _maxHealth;  
    }

    public void takeDamage(float amount)
    {
        _health -= amount;

        if (gameObject.name == "boss")
        {
            handCalls();
        }
        if (_health <= 0)
        {
            //vfx, sound
            print("die");
        }
    }

    private void handCalls()
    {
        GameObject[] hands = GameObject.FindGameObjectsWithTag("hand");
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i].GetComponent<Hands>().PhaseCheck();
        }
    }

}
