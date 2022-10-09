using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{

    GameObject _player;
    [SerializeField] float magnitued;
    [SerializeField] float duration;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _player.GetComponent<Health>().OnHit += CameraShake;
    }

    public void CameraShake(float nah)
    {
        StartCoroutine(_camShake(duration, magnitued));
    }

    IEnumerator _camShake(float duration, float magnitued)
    {
        Vector3 orginalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitued;
            float y = Random.Range(-1f, 1f) * magnitued;

            transform.localPosition = new Vector3(x, y, orginalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = orginalPos;
    }
}
