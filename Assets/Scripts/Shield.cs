using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand")
        {
            //audio / visual
            other.GetComponent<Hands>().stunned = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<playerInputs>().ShieldCoolDown(1f);
        }
    }
}
