using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] float _maxHealth;
    [SerializeField] float _health;

    [SerializeField] AudioClip _bossHit;
    [SerializeField] AudioClip _playerHit;
    [SerializeField] AudioClip _bossDeath;
    [SerializeField] AudioClip _playerDeath;

    [SerializeField] GameObject _vfxDeath;
    [SerializeField] GameObject _vfxBossHit;

    float _volume = 1f;

    public event Action<float> OnHit;

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
            AudioHelper.PlayClip2D(_bossHit, _volume);
            handCalls();
        }
        if (gameObject.tag == "Player")
        {
            AudioHelper.PlayClip2D(_playerHit, _volume);
        }
        if (_health <= 0)
        {
            //vfx, sound
            if (gameObject.name == "boss")
            {
                Instantiate(_vfxDeath, gameObject.transform.position, gameObject.transform.rotation);
                AudioHelper.PlayClip2D(_bossDeath, _volume);
            }
            if (gameObject.tag == "Player")
            {
                Instantiate(_vfxDeath, gameObject.transform.position, gameObject.transform.rotation);
                AudioHelper.PlayClip2D(_playerDeath, _volume);
            }
            gameObject.SetActive(false);
        }
        OnDamage();
    }

    private void handCalls()
    {
        GameObject[] hands = GameObject.FindGameObjectsWithTag("hand");
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i].GetComponent<Hands>().PhaseCheck();
            if (_health <= 0)
            {
                Instantiate(_vfxDeath, hands[i].transform.position, hands[i].transform.rotation);
                hands[i].SetActive(false);
            }
        }
    }

    public void OnDamage()
    {
        OnHit.Invoke(_health);
    }

}
