using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] AudioClip _shieldHit;
    [SerializeField] GameObject _vfxShieldHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand")
        {
            Instantiate(_vfxShieldHit, gameObject.transform.position, gameObject.transform.rotation);
            AudioHelper.PlayClip2D(_shieldHit, 0.5f);
            other.GetComponent<Hands>().stunned = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<playerInputs>().ShieldCoolDown(1f);
        }
    }
}
