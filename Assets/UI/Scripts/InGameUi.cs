using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine.Audio;
public class InGameUi : MonoBehaviour
{
    #region Variaveis Dia e Noite
    public GameObject gameManager;
    private DayNightCycle dayNight;
    #endregion
    public static InGameUi Instance;
    public int ActualTurn = 1;
    [SerializeField] private int _progressBarMargin;
    [SerializeField] private RoundManager _roundManager;
    private Button _pauseBtn;
    private Button _nextTurnBtn;
    private Button _resumeNextTurnBtn;
    private Button _optionPauseBtn;
    private Button _menuPauseBtn;
    private Button _resumePauseBtn;
    private Button _optionResumeBtn;
    private VisualElement _pauseContainer;
    private VisualElement _optionContainer;
    private VisualElement _nextTurnAdvice;
    public VisualElement _NextTurnAdvice { get => _nextTurnAdvice; }
    private VisualElement[] _barPositions = new VisualElement[9];
    private VisualElement _lifeProgressBar;
    private VisualElement _dnaContainer;
    private Label _lifeText;
    private Label _dnaCountText;
    //VolumeConfig
    private SliderInt _globalVolumeSlider;
    private SliderInt _sfxVolumeSlider;
    private SliderInt _bgmVolumeSlider;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioClip _buttonConfirmation;
    [SerializeField] private AudioClip _buttonDenial;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
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
    private void OnEnable()
    {
        //Adição de todos botões e telas que se movem ou tem interação por código
        #region Capitacao De Elmentos
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _pauseBtn = root.Q<Button>("pause-button");
        _nextTurnBtn = root.Q<Button>("next-turn-button");
        _resumeNextTurnBtn = root.Q<Button>("turn-change-confirmation-btn");
        _optionPauseBtn = root.Q<Button>("option-pause-btn");
        _menuPauseBtn = root.Q<Button>("menu-pause-btn");
        _resumePauseBtn = root.Q<Button>("back-pause-btn");
        _optionResumeBtn = root.Q<Button>("save-option-btn");
        _pauseContainer = root.Q<VisualElement>("pause-container");
        _optionContainer = root.Q<VisualElement>("options-container");
        _nextTurnAdvice = root.Q<VisualElement>("turn-change-container");
        for (int i = 1; i <= 9; i++)
        {
            _barPositions[i - 1] = root.Q<VisualElement>("bar-space_" + i);
        }
        _lifeProgressBar = root.Q<VisualElement>("life-bar-progress");
        _dnaContainer = root.Q<VisualElement>("dna-container");
        _lifeText = root.Q<Label>("life-txt");
        _dnaCountText = root.Q<Label>("dna-count-txt");
        #endregion
        //Adição das funções no clique dos botões
        #region Adicao da Funcao Aos Botoes
        _pauseBtn.RegisterCallback<ClickEvent>(OnClickPause);
        _nextTurnBtn.RegisterCallback<ClickEvent>(OnClickTurnChange);
        _resumeNextTurnBtn.RegisterCallback<ClickEvent>(OnResumeTurnChange);
        _optionPauseBtn.RegisterCallback<ClickEvent>(OnClickOptionPause);
        _menuPauseBtn.RegisterCallback<ClickEvent>(OnClickMenuPause);
        _resumePauseBtn.RegisterCallback<ClickEvent>(OnClickResumePause);
        _optionResumeBtn.RegisterCallback<ClickEvent>(OnClickResumeOption);
        #endregion

        #region Som
        _globalVolumeSlider = root.Q<SliderInt>("global-volume-slider");
        _sfxVolumeSlider = root.Q<SliderInt>("sfx-volume-slider");
        _bgmVolumeSlider = root.Q<SliderInt>("bgm-volume-slider");
        _globalVolumeSlider.RegisterCallback<ChangeEvent<int>>(GlobalVolumeSliderValueChange);
        _sfxVolumeSlider.RegisterCallback<ChangeEvent<int>>(SFXVolumeSliderValueChange);
        _bgmVolumeSlider.RegisterCallback<ChangeEvent<int>>(BGMVolumeSliderValueChange);
        #endregion


        //Seta as variaveis para controlar o dia e a noite
        #region Setando Variaveis para Dia e Noite
        dayNight = gameManager.GetComponent<DayNightCycle>();
        #endregion
        UpdateLifeBarOwnerBase();
        UpdateMutationPointText();
    }
    // A unica função que precisa ser alterada é a de OnClickTurnChange para adicionar a chamada do evento e tirar a tela temporária
    #region Funcao Dos Botoes
    private void OnClickPause(ClickEvent evt)
    {
        _pauseContainer.AddToClassList("turn-screen-open");
    }
    private void OnClickTurnChange(ClickEvent evt)
    {
        
        if (!GameObject.FindAnyObjectByType<RoundManager>().VerificarSeExisteAction())
        {
            #region Controle Dia e Noite
            dayNight.ChangeDayNight();
            #endregion

            _nextTurnAdvice.AddToClassList("turn-screen-open");
            _roundManager.PassarTurno();
            ActualTurn = _roundManager._CurrentTurno;
            for (int i = 0; i < 9; i++)
            {
                if ((ActualTurn - 4 + i) < 0)
                {
                    _barPositions[i].RemoveFromClassList("big-bar__red");
                    _barPositions[i].RemoveFromClassList("big-bar__orange");
                    _barPositions[i].RemoveFromClassList("little-bar");
                    _barPositions[i].AddToClassList("no-bar");
                }
                else if ((ActualTurn - 4 + i) % 15 == 0)
                {
                    _barPositions[i].RemoveFromClassList("big-bar__orange");
                    _barPositions[i].RemoveFromClassList("no-bar");
                    _barPositions[i].RemoveFromClassList("little-bar");
                    _barPositions[i].AddToClassList("big-bar__red");
                }
                else if ((ActualTurn - 4 + i) % 5 == 0)
                {
                    _barPositions[i].RemoveFromClassList("big-bar__red");
                    _barPositions[i].RemoveFromClassList("no-bar");
                    _barPositions[i].RemoveFromClassList("little-bar");
                    _barPositions[i].AddToClassList("big-bar__orange");
                }
                else
                {
                    _barPositions[i].RemoveFromClassList("big-bar__red");
                    _barPositions[i].RemoveFromClassList("big-bar__orange");
                    _barPositions[i].RemoveFromClassList("no-bar");
                    _barPositions[i].AddToClassList("little-bar");
                }
            }
        }
    }
    private void OnResumeTurnChange(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _nextTurnAdvice.RemoveFromClassList("turn-screen-open");
    }
    private void OnClickOptionPause(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _pauseContainer.RemoveFromClassList("turn-screen-open");
        _optionContainer.RemoveFromClassList("option-closed");
    }
    private void OnClickMenuPause(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        SceneManager.LoadScene("MainMenu");
    }
    private void OnClickResumePause(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _pauseContainer.RemoveFromClassList("turn-screen-open");
    }
    private void OnClickResumeOption(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        _optionContainer.AddToClassList("option-closed");
    }
    #endregion
    public void AnimateLifeBar(float actualValue, float maxValue)
    {
        //Pega o valor da barra para animar diminuindo a margem
        float endWidth = _lifeProgressBar.parent.worldBound.width - _progressBarMargin;
        _lifeText.text = $"{actualValue}/{maxValue}";
        DOTween.To(() => _lifeProgressBar.worldBound.width, x => _lifeProgressBar.style.width = x, (endWidth * (actualValue / maxValue)), 2F).SetEase(Ease.Linear);
    }
    public void CreatureInfoChange()
    {
        _pauseBtn.AddToClassList("pause-button-ocult");
        _nextTurnBtn.AddToClassList("pause-button-ocult");
        _dnaContainer.AddToClassList("pause-button-ocult");
    }
    public void CreatureInfoNormal()
    {
        _pauseBtn.RemoveFromClassList("pause-button-ocult");
        _nextTurnBtn.RemoveFromClassList("pause-button-ocult");
        _dnaContainer.RemoveFromClassList("pause-button-ocult");
    }
    public void UpdateLifeBarOwnerBase()
    {
        float maxLife = 0;
        float actualLife = 0;
        switch (_roundManager.RoundOwner)
        {
            case Owner.P1:
                foreach (GameObject p in _roundManager._P1Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P2:
                foreach (GameObject p in _roundManager._P2Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P3:
                foreach (GameObject p in _roundManager._P3Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P4:
                foreach (GameObject p in _roundManager._P4Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P5:
                foreach (GameObject p in _roundManager._P5Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
        }
        AnimateLifeBar(actualLife, maxLife);
    }
    public void UpdateMutationPointText()
    {
        int points = 0;
        switch (_roundManager.RoundOwner)
        {
            case Owner.P1:
                points += _roundManager.PontosP1Mutacoes;
                break;
            case Owner.P2:
                points += _roundManager.PontosP2Mutacoes;
                break;
            case Owner.P3:
                points += _roundManager.PontosP3Mutacoes;
                break;
            case Owner.P4:
                points += _roundManager.PontosP4Mutacoes;
                break;
            case Owner.P5:
                points += _roundManager.PontosP5Mutacoes;
                break;
        }
        _dnaCountText.text = "= " + points.ToString();
    }
    //Som
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