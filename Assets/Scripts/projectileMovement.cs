using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : Movement
{
    public GameObject targetPos;
    float _timer;

    [SerializeField] AudioClip _badHit;
    [SerializeField] GameObject _vfxBadHit;
    [SerializeField] GameObject _vfxBossHit;

    float _volume = 1f;

    //ask about having awake 2 in inherited class
    private void Start()
    {
        // gameObject.transform.LookAt(targetPos.transform.position);
        _timer = Time.time + 3f;
    }

    private void FixedUpdate()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        //direction to be added later
        Vector3 FowardmoveOffset = transform.forward * _maxSpeed;

        _rb.MovePosition(_rb.position + FowardmoveOffset);
    }

    private void Update()
    {
        DestroyByTime();
    }

    void DestroyByTime()
    {
        if (_timer <= Time.time)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            Instantiate(_vfxBossHit, gameObject.transform.position, gameObject.transform.rotation);
            other.GetComponent<IDamageable>().takeDamage(1);
        }
        if (other.name != "boss")
        {
            Instantiate(_vfxBadHit, gameObject.transform.position, gameObject.transform.rotation);
            AudioHelper.PlayClip2D(_badHit, _volume);
        }
        Destroy(gameObject);
    }
}
