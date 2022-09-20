using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Movement
{
    [SerializeField] GameObject _boss;
    [SerializeField] GameObject _player;

    [SerializeField] GameObject[] _handsList;


    Quaternion startRotation;

    float _bossHealth;
    int phase = 3;

    public bool stunned = false;

    // Start is called before the first frame update
    void Start()
    {
        _bossHealth = _boss.GetComponent<Health>()._currentHealth;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (stunned == false)
        {
            transform.LookAt(_player.transform);

            if (gameObject.name == "Hand_L1")
            {
                HandL1();
            }
            if (gameObject.name == "Hand_L2")
            {
                HandL2();
            }
            if (gameObject.name == "Hand_R1")
            {
                HandR1();
            }
            if (gameObject.name == "Hand_R2")
            {
                HandR2();
            }
        }
        else
        {
            StunnedState();
        }
    }

    public void PhaseCheck()
    {
        _bossHealth = _boss.GetComponent<Health>()._currentHealth;

        stunned = false;
        transform.rotation = startRotation;

        if (_bossHealth == 3)
        {
            phase = 3;
        }
        else if (_bossHealth == 2)
        {
            phase = 2;
        }
        else if (_bossHealth == 1)
        {
            phase = 1;
        }
        else if (_bossHealth == 0)
        {
            phase = 0;
        }
    }

    void StunnedState()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -180);
    }

    void HandL1()
    {
        if (phase == 3)
        {
            if (_handsList[1].GetComponent<Hands>().stunned == false)
            {
                transform.position = new Vector3(_boss.transform.position.x + 2.56f, 3, _boss.transform.position.z -5.5f);
            }
            else
            {
                moveHands();
            }
        }
        if (phase == 2)
        {
            moveHands();
        }
        if (phase == 1)
        {
            if (_handsList[1].GetComponent<Hands>().stunned == false)
            {
                transform.position = new Vector3(_boss.transform.position.x + 2.56f, 3, _boss.transform.position.z - 5.5f);
            }
            else
            {
                moveHands();
            }
        }
        if (phase == 0)
        {
            stunned = true;
        }
    }
    void HandL2()
    {
        if (phase == 3)
        {
            moveHands();
        }
        if (phase == 2)
        {
            moveHands();
        }
        if (phase == 1)
        {
            moveHands();
        }
        if (phase == 0)
        {
            stunned = true;
        }
    }
    void HandR1()
    {
        if (phase == 3)
        {
            if (_handsList[3].GetComponent<Hands>().stunned == false)
            {
                transform.position = new Vector3(_boss.transform.position.x - 2.56f, 3, _boss.transform.position.z - 5.5f);
            }
            else
            {
                moveHands();
            }
        }
        if (phase == 2)
        {
            moveHands();
        }
        if (phase == 1)
        {
            if (_handsList[3].GetComponent<Hands>().stunned == false)
            {
                transform.position = new Vector3(_boss.transform.position.x - 2.56f, 3, _boss.transform.position.z - 5.5f);
            }
            else
            {
                moveHands();
            }
        }
        if (phase == 0)
        {
            stunned = true;
        }
    }
    void HandR2()
    {
        if (phase == 3)
        {
            moveHands();
        }
        if (phase == 2)
        {
            moveHands();
        }
        if (phase == 1)
        {
            moveHands();
        }
        if (phase == 0)
        {
            stunned = true;
        }
    }

    void moveHands()
    {
        Vector3 FowardmoveOffset = transform.forward * _maxSpeed;

        _rb.MovePosition(_rb.position + FowardmoveOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().takeDamage(1);
        }
    }
}
