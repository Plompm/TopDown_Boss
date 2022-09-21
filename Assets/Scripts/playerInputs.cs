using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputs : Movement, IInstantiater
{
    float _dashCoolDown;
    float _shieldUpTime;
    float _shieldCoolDown;
    float _maxSpeedNorm;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] shieldPoints;
    int shieldPos = 0;

    [SerializeField] AudioClip _dashSound;
    [SerializeField] AudioClip _shieldUp;
    [SerializeField] AudioClip _fireProjectile;

    [SerializeField] GameObject _vfxDash;

    float _volume = 1f;

    private void Start()
    {
        _maxSpeedNorm = _maxSpeed;
    }

    private void Update()
    {
        Dash();
        Block(0.25f, 1f);
        shoot();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Instantiate(_vfxDash, gameObject.transform.position, gameObject.transform.rotation);
            AudioHelper.PlayClip2D(_dashSound, _volume);
            _dashCoolDown = Time.time + 0.1f;
            _maxSpeed += .5f;
        }
        if (_dashCoolDown <= Time.time) 
        {
            _maxSpeed = _maxSpeedNorm;
        }
    }

    void Block(float _upTime, float _coolDown)
    {
        if (_shieldCoolDown <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                AudioHelper.PlayClip2D(_shieldUp, _volume);
                shield.transform.position = shieldPoints[0].transform.position;
                shield.transform.rotation = shieldPoints[0].transform.rotation;
                shieldPos = 0;
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<BoxCollider>().enabled = true;
                _shieldUpTime = Time.time + _upTime;
                _shieldCoolDown = Time.time + _coolDown;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                AudioHelper.PlayClip2D(_shieldUp, _volume);
                shield.transform.position = shieldPoints[1].transform.position;
                shield.transform.rotation = shieldPoints[1].transform.rotation;
                shieldPos = 1;
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<BoxCollider>().enabled = true;
                _shieldUpTime = Time.time + _upTime;
                _shieldCoolDown = Time.time + _coolDown;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AudioHelper.PlayClip2D(_shieldUp, _volume);
                shield.transform.position = shieldPoints[2].transform.position;
                shield.transform.rotation = shieldPoints[2].transform.rotation;
                shieldPos = 2;
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<BoxCollider>().enabled = true;
                _shieldUpTime = Time.time + _upTime;
                _shieldCoolDown = Time.time + _coolDown;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                AudioHelper.PlayClip2D(_shieldUp, _volume);
                shield.transform.position = shieldPoints[3].transform.position;
                shield.transform.rotation = shieldPoints[3].transform.rotation;
                shieldPos = 3;
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<BoxCollider>().enabled = true;
                _shieldUpTime = Time.time + _upTime;
                _shieldCoolDown = Time.time + _coolDown;
            }
        }

        if (_shieldUpTime <= Time.time)
        {
            shield.GetComponent<MeshRenderer>().enabled = false;
            shield.GetComponent<BoxCollider>().enabled = false;
            
        }

        shield.transform.position = shieldPoints[shieldPos].transform.position;
    }

    public void ShieldCoolDown(float _coolDown)
    {
        shield.GetComponent<MeshRenderer>().enabled = false;
        shield.GetComponent<BoxCollider>().enabled = false;
        _shieldCoolDown = Time.time + _coolDown;
    }

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioHelper.PlayClip2D(_fireProjectile, _volume);
            spawnObject(projectile, shieldPoints[2]);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        // calculate the move amount
        float moveAmountThisFrameFowrad = Input.GetAxis("Vertical") * _maxSpeed;
        float moveAmountThisFrameSide = Input.GetAxis("Horizontal") * _maxSpeed;
        // create a vector from amount and direction
        Vector3 FowardmoveOffset = transform.forward * moveAmountThisFrameFowrad;
        Vector3 SidemoveOffset = transform.right * moveAmountThisFrameSide;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + FowardmoveOffset + SidemoveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }


    public void spawnObject(GameObject spawn, Transform spawnPoint)
    {
        GameObject spawned = Instantiate(spawn, spawnPoint.position, spawnPoint.rotation);
        spawned.transform.parent = gameObject.transform;
    }
}
