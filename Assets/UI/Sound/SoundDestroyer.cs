using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDestroyer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private float _clipLength;
    private Coroutine _returnToPoolTimerCoroutine;
    public void SetClipLenght(float value)
    {
        _clipLength = value;
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnToPoolAfetTime());
    }
    private IEnumerator ReturnToPoolAfetTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _clipLength)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }
}
