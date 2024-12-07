using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Sound/Sound Manager", fileName = "Sound Manager")]
public class SoundManagerSO : ScriptableObject
{
    private static SoundManagerSO instance;
    public static SoundManagerSO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<SoundManagerSO>("Sound Manager");
            }
            return instance;
        }
    }
    public AudioSource SoundFXObject;
    public AudioSource BGMusicObject;
    private static MusicDestroyer _activeBGMusic;
    private static bool _isPlayingBGM = false;
    private static float _volumeChangeMultiplier = 0.15f;
    private static float _pitchChangeMultiplier = 0.1f;
    private static void ChangeBGMusic()
    {
        _activeBGMusic.ReturnToPoolMusic();
        _isPlayingBGM = false;
    }
    public static void PlayBGMusicClip(AudioClip clip, Vector3 soundPos, float volume)
    {
        if (_isPlayingBGM) ChangeBGMusic();
        float randVolume = Random.Range(1 - _volumeChangeMultiplier, _volumeChangeMultiplier + 1);
        float randPitch = Random.Range(1 - _pitchChangeMultiplier, _pitchChangeMultiplier + 1);

        GameObject gmObj = ObjectPoolManager.SpawnObject(Instance.BGMusicObject.gameObject, soundPos, Quaternion.identity);
        AudioSource a = gmObj.GetComponent<AudioSource>();

        a.clip = clip;
        a.volume = randVolume;
        a.pitch = randPitch;
        _activeBGMusic = a.GetComponent<MusicDestroyer>();
        a.Play();
        _isPlayingBGM = true;
    }
    public static void PlayBGMusicClip(AudioClip[] clip, Vector3 soundPos, float volume)
    {
        if (_isPlayingBGM) ChangeBGMusic();
        int randClip = Random.Range(0, clip.Length - 1);
        float randVolume = Random.Range(1 - _volumeChangeMultiplier, _volumeChangeMultiplier + 1);
        float randPitch = Random.Range(1 - _pitchChangeMultiplier, _pitchChangeMultiplier + 1);

        GameObject gmObj = ObjectPoolManager.SpawnObject(Instance.BGMusicObject.gameObject, soundPos, Quaternion.identity);
        AudioSource a = gmObj.GetComponent<AudioSource>();

        a.clip = clip[randClip];
        a.volume = randVolume;
        a.pitch = randPitch;
        _activeBGMusic = a.GetComponent<MusicDestroyer>();
        a.Play();
        _isPlayingBGM = true;
    }

    public static void PlaySoundFXClip(AudioClip clip, Vector3 soundPos, float volume)
    {
        float randVolume = Random.Range(1 - _volumeChangeMultiplier, _volumeChangeMultiplier + 1);
        float randPitch = Random.Range(1 - _pitchChangeMultiplier, _pitchChangeMultiplier + 1);

        GameObject gmObj = ObjectPoolManager.SpawnObject(Instance.SoundFXObject.gameObject, soundPos, Quaternion.identity);
        AudioSource a = gmObj.GetComponent<AudioSource>();

        a.clip = clip;
        a.volume = randVolume;
        a.pitch = randPitch;
        a.GetComponent<SoundDestroyer>().SetClipLenght(clip.length);
        a.Play();
    }

    public static void PlaySoundFXClip(AudioClip[] clip, Vector3 soundPos, float volume)
    {
        int randClip = Random.Range(0, clip.Length - 1);
        float randVolume = Random.Range(1 - _volumeChangeMultiplier, _volumeChangeMultiplier + 1);
        float randPitch = Random.Range(1 - _pitchChangeMultiplier, _pitchChangeMultiplier + 1);

        GameObject gmObj = ObjectPoolManager.SpawnObject(Instance.SoundFXObject.gameObject, soundPos, Quaternion.identity);
        AudioSource a = gmObj.GetComponent<AudioSource>();

        a.clip = clip[randClip];
        a.volume = randVolume;
        a.pitch = randPitch;
        a.GetComponent<SoundDestroyer>().SetClipLenght(a.clip.length);
        a.Play();
    }
}
