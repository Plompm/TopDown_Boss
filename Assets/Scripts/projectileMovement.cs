using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : Movement
{
    public GameObject targetPos;

    //ask about having awake 2 in inherited class
    private void Start()
    {
       // gameObject.transform.LookAt(targetPos.transform.position);
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
}
