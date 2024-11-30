using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script original por: https://www.youtube.com/@SmallHedgeHQ
public enum SoundType
{
    birdChirps,
    confirm,
    exit,
    bigDesaster,
    smallDesaster,
    grassSteps,
    dryGrassSteps,
    dryGrassSteps2,
    getHit,
    getHit2,
    lettuceEating,
    vegetablesEating,
    creatureDies,
    creatureIsBorn,
    creatureShivering,
    runningWater
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource audioSource;
    [SerializeField] AudioClip[] soundList;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    public static void PlaySoundByIndex(int soundIndex, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[soundIndex], volume);
    }
}
