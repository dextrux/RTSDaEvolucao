using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private Button _playBtn;
    private Button _configBtn;
    private Button _configSaveBtn;
    private Button _creditsBtn;
    private VisualElement _optionsContainer;
    [SerializeField] GameObject _creditsScreen;
    [SerializeField] private AudioClip _bgmusic;
    [SerializeField] private AudioClip _buttonConfirmation;
    [SerializeField] private AudioMixer _audioMixer;

    //VolumeConfig
    private SliderInt _globalVolumeSlider;
    private SliderInt _sfxVolumeSlider;
    private SliderInt _bgmVolumeSlider;


    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _playBtn = root.Q<Button>("play-btn");
        _configBtn = root.Q<Button>("option-btn");
        _configSaveBtn = root.Q<Button>("save-option-btn");
        _creditsBtn = root.Q<Button>("credit-btn");
        _optionsContainer = root.Q<VisualElement>("options-container");
        _playBtn.RegisterCallback<ClickEvent>(OnClickPlay);
        _configBtn.RegisterCallback<ClickEvent>(OnClickConfig);
        _configSaveBtn.RegisterCallback<ClickEvent>(OnClickConfigSave);
        _creditsBtn.RegisterCallback<ClickEvent>(OnClickCredits);
        SoundManagerSO.PlayBGMusicClip(_bgmusic, transform.position, 1);

        _globalVolumeSlider = root.Q<SliderInt>("global-volume-slider");
        _sfxVolumeSlider = root.Q<SliderInt>("sfx-volume-slider");
        _bgmVolumeSlider = root.Q<SliderInt>("bgm-volume-slider");
        _globalVolumeSlider.RegisterCallback<ChangeEvent<int>>(GlobalVolumeSliderValueChange);
        _sfxVolumeSlider.RegisterCallback<ChangeEvent<int>>(SFXVolumeSliderValueChange);
        _bgmVolumeSlider.RegisterCallback<ChangeEvent<int>>(BGMVolumeSliderValueChange);

        if (PlayerPrefs.HasKey("globalVolume"))
        {
            LoadVolume();
        }
        else
        {
            PlayerPrefs.SetInt("globalVolume", (-5));
            PlayerPrefs.SetInt("sfxVolume", (-5));
            PlayerPrefs.SetInt("bgmVolume", (-5));
        }
    }

    private void OnClickPlay(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        SceneManager.LoadScene("ColorPicker");
    }
    private void OnClickConfig(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _optionsContainer.AddToClassList("option-open");
    }
    private void OnClickConfigSave(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _optionsContainer.RemoveFromClassList("option-open");
    }
    private void OnClickCredits(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _creditsScreen.SetActive(true);
    }
    private void GlobalVolumeSliderValueChange(ChangeEvent<int> value)
    {
        _audioMixer.SetFloat("GeneralVolume", value.newValue);
        PlayerPrefs.SetInt("globalVolume", value.newValue);
    }
    private void SFXVolumeSliderValueChange(ChangeEvent<int> value)
    {
        _audioMixer.SetFloat("SfxVolume", value.newValue);
        PlayerPrefs.SetInt("sfxVolume", value.newValue);
    }
    private void BGMVolumeSliderValueChange(ChangeEvent<int> value)
    {
        _audioMixer.SetFloat("BgMusicVolume", value.newValue);
        PlayerPrefs.SetInt("bgmVolume", value.newValue);
    }
    private void LoadVolume()
    {
        _globalVolumeSlider.value = PlayerPrefs.GetInt("globalVolume");
        _sfxVolumeSlider.value = PlayerPrefs.GetInt("sfxVolume");
        _bgmVolumeSlider.value = PlayerPrefs.GetInt("bgmVolume");
    }
}
