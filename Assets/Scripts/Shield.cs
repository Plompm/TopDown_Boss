using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] AudioClip _shieldHit;
    [SerializeField] AudioClip _badHit;
    [SerializeField] GameObject _vfxBadHit;
    [SerializeField] GameObject _vfxShieldHit;

    bool _isPaused;

    private void OnTriggerEnter(Collider other)
    {
        _isPaused = GameObject.Find("GameController").GetComponent<GameController>().isPaused;
        if (_isPaused == false)
        {
            if (other.tag == "hand")
            {

                Instantiate(_vfxShieldHit, gameObject.transform.position, gameObject.transform.rotation);
                AudioHelper.PlayClip2D(_shieldHit, 0.5f);
                if (other.GetComponent<Hands>().phase != 2)
                {
                    other.GetComponent<Hands>().stunned = true;
                }
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<playerInputs>().ShieldCoolDown(1f);
            }
            if (other.tag == "handShield")
            {

                Instantiate(_vfxBadHit, gameObject.transform.position, gameObject.transform.rotation);
                AudioHelper.PlayClip2D(_badHit, 0.5f);
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<playerInputs>().ShieldCoolDown(1f);
            }
        }
    }
}
