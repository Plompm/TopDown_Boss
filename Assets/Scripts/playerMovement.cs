using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float _maxSpeed = .25f;
    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    Rigidbody _rb = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveTank();
    }

    public void MoveTank()
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
