using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgGameSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _bgSounds;
    private void Awake()
    {
        foreach (AudioClip sound in _bgSounds)
        {
            SoundManagerSO.PlayBGSoundClip(sound, transform.position, 1);
        }
    }
}
