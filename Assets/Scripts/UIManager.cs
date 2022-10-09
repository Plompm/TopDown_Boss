using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    float _shieldCoolDown;
    float _redUICoolDown;
    float _resetTime = 0.2f;

   GameObject _player;
    GameObject _boss;

    [SerializeField] GameObject _redUI;
    [SerializeField] GameObject _UIplayerHealth;
    [SerializeField] GameObject _UIbossHealth;

    [SerializeField] GameObject _UIbossBox;
    [SerializeField] GameObject _UIplayerBox;
    [SerializeField] GameObject _UIshieldBox;

    [SerializeField] Texture[] _bossImages;
    [SerializeField] Texture[] _playerImages;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _boss = GameObject.FindGameObjectWithTag("Boss");

        _player.GetComponent<Health>().OnHit += OnPlayerDamage;
        _player.GetComponent<playerInputs>().OnShieldCoolDown += OnShield;
        _boss.GetComponent<Health>().OnHit += OnBossDamage;
    }

    void OnPlayerDamage(float _health)
    {
        Image _toRemove = _UIplayerHealth.GetComponentInChildren<Image>();
        Destroy(_toRemove);


        int _intHealth = (int)_health;
        _UIplayerBox.GetComponent<RawImage>().texture = _playerImages[_intHealth];

        _redUI.SetActive(true);
        _redUICoolDown = Time.time + _resetTime;
    }

    void OnBossDamage(float _health)
    {
        Image _toRemove = _UIbossHealth.GetComponentInChildren<Image>();

        Destroy(_toRemove);

        int _intHealth = (int)_health;
        _UIbossBox.GetComponent<RawImage>().texture = _bossImages[_intHealth];
    }

    void OnShield(float _coolDownTime)
    {
        _UIshieldBox.SetActive(true);
        _shieldCoolDown = Time.time + _coolDownTime;
    }



    private void Update()
    {
        if (_shieldCoolDown <= Time.time)
        {
            _UIshieldBox.SetActive(false);
        }

        if (_redUICoolDown <= Time.time)
        {
            _redUI.SetActive(false);
        }
    }
}
