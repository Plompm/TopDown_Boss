using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Movement
{
    bool _isPaused;

    bool _movingRight =  true;



    // Update is called once per frame
    void Update()
    {
        _isPaused = GameObject.Find("GameController").GetComponent<GameController>().isPaused;
        if (_isPaused == false)
        {
            BossMovement();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (_isPaused == true)
        {
            transform.rotation = Quaternion.Euler(50, 0, 0);
        }
    }

    void BossMovement()
    {
        if (gameObject.GetComponent<Health>()._currentHealth == 3 || gameObject.GetComponent<Health>()._currentHealth == 1)
        {
            if (transform.position.x >= 2)
            {
                _movingRight = false;
            }
            if (transform.position.x <= -2)
            {
                _movingRight = true;
            }

            if (_movingRight == true)
            {
                Vector3 SidemoveOffset = transform.right * _maxSpeed;

                _rb.MovePosition(_rb.position + SidemoveOffset);
            }

            if (_movingRight == false)
            {
                Vector3 SidemoveOffset = -transform.right * _maxSpeed;

                _rb.MovePosition(_rb.position + SidemoveOffset);
            }
        }
    }
}
