using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputs : Movement, IInstantiater
{
    float _timer;
    float _maxSpeedNorm;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] shieldPoints;

    private void Start()
    {
        _maxSpeedNorm = _maxSpeed;
    }

    private void Update()
    {
        Dash();
        Block();
        shoot();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _timer = Time.time + 0.1f;
            _maxSpeed += .5f;
        }
        if (_timer <= Time.time) 
        {
            _maxSpeed = _maxSpeedNorm;
        }
    }

    void Block()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            spawnObject(shield, shieldPoints[0]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spawnObject(shield, shieldPoints[1]);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            spawnObject(shield, shieldPoints[2]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spawnObject(shield, shieldPoints[3]);
        }
    }

    void shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
