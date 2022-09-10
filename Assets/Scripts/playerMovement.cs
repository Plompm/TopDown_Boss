using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : Movement
{
    float _timer;
    float _maxSpeedNorm;

    private void Start()
    {
        _maxSpeedNorm = _maxSpeed;
    }

    private void Update()
    {
        Dash();
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
}
