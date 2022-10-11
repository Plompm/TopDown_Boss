using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : Movement
{
    [SerializeField] GameObject _boss;
    [SerializeField] GameObject _player;

    [SerializeField] GameObject[] _handsList;
    [SerializeField] GameObject _handsArt;
    [SerializeField] GameObject _handShield;

    [SerializeField] Material _handsMaterial;

    public GameObject HandShield => _handShield;

    float _randX;
    float _randZ;
    float _randY;

    public float _L2handPhase = 0;
    float _R1handPhase = 0;
    float _R2handPhase = 0;
    float _baseMaxSpeed;

    Quaternion startRotation;

    float _bossHealth;
    public int phase = 3;

    public bool stunned = false;

    bool _isPaused;
    // Start is called before the first frame update
    void Start()
    {
        _baseMaxSpeed = _maxSpeed;
        _bossHealth = _boss.GetComponent<Health>()._currentHealth;
        startRotation = transform.rotation;

        _randX = Random.Range(-10, 10);
        _randZ = Random.Range(-10, 10);
        _randY = Random.Range(-10, 10);


        Color BaseColor = _handsMaterial.color;
        BaseColor.a = 1f;
        _handsMaterial.color = BaseColor;
    }

    private void Update()
    {
        _isPaused = GameObject.Find("GameController").GetComponent<GameController>().isPaused;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isPaused == false)
        {

            Color color = _handsMaterial.color;
            color.a = 1f;
            _handsMaterial.color = color;


            if (stunned == false)
            {
                if (phase != 2)
                {
                    transform.LookAt(_player.transform);
                }
                _handsArt.transform.rotation = gameObject.transform.rotation;

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

        if (_isPaused == true)
        {
            stunned = false;
            Color color = _handsMaterial.color;
            color.a = 0.25f;
            _handsMaterial.color = color;

            if (gameObject.name == "Hand_L1")
            {
                HandL1PhaseTransition();
            }
            if (gameObject.name == "Hand_L2")
            {
                HandL2PhaseTransition();
            }
            if (gameObject.name == "Hand_R1")
            {
                HandR1PhaseTransition();
            }
            if (gameObject.name == "Hand_R2")
            {
                HandR2PhaseTransition();
            }
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
        Quaternion turnOffset = Quaternion.Euler(_randX, _randZ, _randY);
        _handsArt.transform.rotation = _handsArt.transform.rotation * turnOffset;
        KnockBack();
    }

    #region hands in phase
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
                _handShield.SetActive(false);
            }
        }
        if (phase == 2)
        {
            transform.LookAt(_player.transform);
            if (_handsList[1].GetComponent<Hands>()._L2handPhase != 3)
            {
                _handShield.SetActive(true);
                transform.position = new Vector3(_boss.transform.position.x, 3, _boss.transform.position.z - 5.5f);
            }
            else
            {
                _handShield.SetActive(false);
                moveHands();
            }
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
    void HandL2()
    {
        if (phase == 3)
        {
            moveHands();
        }
        if (phase == 2)
        {
            L2Targeting();
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
            R1Targeting();
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
    void HandR2()
    {
        if (phase == 3)
        {
            moveHands();
        }
        if (phase == 2)
        {
            R2Targeting();
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
    #endregion; 

    #region hands out of phase
    void HandL1PhaseTransition()
    {
        if (phase == 2)
        {
            transform.LookAt(new Vector3(_boss.transform.position.x, 3, _boss.transform.position.z - 5.5f));
            moveHands();
        }
        if (phase == 1)
        {
            transform.LookAt(new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + 10));
            moveHands();
        }
    }
    void HandL2PhaseTransition()
    {
        if (phase == 2)
        {
            transform.LookAt(new Vector3(_player.transform.position.x + 20, _player.transform.position.y, _player.transform.position.z+8));
            moveHands();
        }
        if (phase == 1)
        {
            transform.LookAt(new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z - 10));
            moveHands();
        }
    }
    void HandR1PhaseTransition()
    {
        if (phase == 2)
        {
            transform.LookAt(new Vector3(_player.transform.position.x + 20, _player.transform.position.y, _player.transform.position.z));
            moveHands();
        }
        if (phase == 1)
        {
            transform.LookAt(new Vector3(_player.transform.position.x + 10, _player.transform.position.y, _player.transform.position.z));
            moveHands();
        }
    }
    void HandR2PhaseTransition()
    {
        if (phase == 2)
        {
            transform.LookAt(new Vector3(_player.transform.position.x + 20, _player.transform.position.y, _player.transform.position.z-8));
            moveHands();
        }
        if (phase == 1)
        {
            transform.LookAt(new Vector3(_player.transform.position.x - 10, _player.transform.position.y, _player.transform.position.z));
            moveHands();
        }
    }
    #endregion

    void moveHands()
    {
        if (_isPaused == false)
        {
            Vector3 FowardmoveOffset = transform.forward * _maxSpeed;

            _rb.MovePosition(_rb.position + FowardmoveOffset);
        }
        if (_isPaused == true)
        {
            Vector3 FowardmoveOffset = transform.forward * _maxSpeed * 20;

            _rb.MovePosition(_rb.position + FowardmoveOffset);
        }
    }

    void KnockBack()
    {
        float _reverseSpeed = _maxSpeed * -2.5f;

        Vector3 FowardmoveOffset = transform.forward * _reverseSpeed;

        _rb.MovePosition(_rb.position + FowardmoveOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isPaused == false)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Health>().takeDamage(1);
            }
        }
    }

    #region targeting for hands
    void L2Targeting()
    {
        if (_L2handPhase != 3)
        {
            _handShield.SetActive(true);
        }
        if (transform.position.x <= -20 && _L2handPhase == 0)
        {
            _L2handPhase = 1;
        }
        if (transform.position.x >= 20 && _L2handPhase == 1)
        {
            _L2handPhase = 2;
        }
        if (transform.position.x <= -20 && _L2handPhase == 2)
        {
            _L2handPhase = 3;
        }

        if (_L2handPhase == 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _maxSpeed = .25f;
        }
        if (_L2handPhase == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            _maxSpeed = 0.4f;
        }
        if (_L2handPhase == 2)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _maxSpeed = 0.6f;
        }
        if (_L2handPhase == 3)
        {
            _maxSpeed = _baseMaxSpeed;
            transform.LookAt(_player.transform);
            _handShield.SetActive(false);
        }
    }
    void R1Targeting()
    {
        if (_R1handPhase != 2)
        {
            _handShield.SetActive(true);
        }
        if (transform.position.x <= -20 && _R2handPhase == 0)
        {
            _R2handPhase = 1;
        }
        if (transform.position.x >= 20 && _R2handPhase == 1)
        {
            _R2handPhase = 2;
        }

        if (_R2handPhase == 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _maxSpeed = .25f;
        }
        if (_R2handPhase == 1)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            _maxSpeed = 0.3f;
        }
        if (_R2handPhase == 2)
        {
            _maxSpeed = _baseMaxSpeed;
            transform.LookAt(_player.transform);
            _handShield.SetActive(false);
        }
    }
    void R2Targeting()
    {
        if (_R2handPhase != 1)
        {
            _handShield.SetActive(true);
        }
        if (transform.position.x <= -20 && _R1handPhase == 0)
        {
            _R1handPhase = 1;
        }

        if (_R1handPhase == 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            _maxSpeed = .15f;
        }
        if (_R1handPhase == 1)
        {
            _maxSpeed = _baseMaxSpeed;
            transform.LookAt(_player.transform);
            _handShield.SetActive(false);
        }
    }
    #endregion 
}
