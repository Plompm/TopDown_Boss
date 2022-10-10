using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{

    GameObject _player;
    GameObject _boss;
    [SerializeField] float magnitued;
    [SerializeField] float duration;

    [SerializeField] float bossMag;
    [SerializeField] float bossDur;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _player.GetComponent<Health>().OnHit += _playerCamShake;

        _boss = GameObject.FindGameObjectWithTag("Boss");

        _boss.GetComponent<Health>().OnHit += _bossCamShake;
    }

    public void _playerCamShake(float nah)
    {
        StartCoroutine(_pCamShake(duration, magnitued));
    }

    IEnumerator _pCamShake(float _duration, float _magnitued)
    {
        Vector3 orginalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < _duration)
        {
            float x = Random.Range(-1f, 1f) * _magnitued;
            float y = Random.Range(-1f, 1f) * _magnitued;

            transform.localPosition = new Vector3(x, y, orginalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orginalPos;
    }

    public void _bossCamShake(float nah)
    {
        StartCoroutine(_bCamShake(bossDur, bossMag));
    }

    IEnumerator _bCamShake(float _duration, float _magnitued)
    {
        Vector3 orginalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < _duration)
        {

            float x = Random.Range(-1f, 1f) * _magnitued;
            float y = Random.Range(-1f, 1f) * _magnitued;

            transform.localPosition = new Vector3(x, y, orginalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orginalPos;
    }
}
