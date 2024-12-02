using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using DG.Tweening;
public class InGameUi : MonoBehaviour
{
    public int ActualTurn = 1; // substituir todas pelo controle de turnos
    [SerializeField] private int _progressBarMargin;
    [SerializeField] private RoundManager _roundManager;
    private Button _pauseBtn;
    private Button _nextTurnBtn;
    private Button _resumeNextTurnBtn; // Tempor�rio para marcar a troca
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

    private void OnEnable()
    {
        //Adi��o de todos bot�es e telas que se movem ou tem intera��o por c�digo
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
        #endregion
        //Adi��o das fun��es no clique dos bot�es
        #region Adicao da Funcao Aos Botoes
        _pauseBtn.RegisterCallback<ClickEvent>(OnClickPause);
        _nextTurnBtn.RegisterCallback<ClickEvent>(OnClickTurnChange);
        _resumeNextTurnBtn.RegisterCallback<ClickEvent>(OnResumeTurnChange);
        _optionPauseBtn.RegisterCallback<ClickEvent>(OnClickOptionPause);
        _menuPauseBtn.RegisterCallback<ClickEvent>(OnClickMenuPause);
        _resumePauseBtn.RegisterCallback<ClickEvent>(OnClickResumePause);
        _optionResumeBtn.RegisterCallback<ClickEvent>(OnClickResumeOption);
        #endregion

    }
    // A unica fun��o que precisa ser alterada � a de OnClickTurnChange para adicionar a chamada do evento e tirar a tela tempor�ria
    #region Funcao Dos Botoes
    private void OnClickPause(ClickEvent evt)
    {
        _pauseContainer.AddToClassList("turn-screen-open");
    }
    private void OnClickTurnChange(ClickEvent evt)
    {
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
    private void OnResumeTurnChange(ClickEvent evt)
    {
        _nextTurnAdvice.RemoveFromClassList("turn-screen-open");
    }
    private void OnClickOptionPause(ClickEvent evt)
    {
        _pauseContainer.RemoveFromClassList("turn-screen-open");
        _optionContainer.RemoveFromClassList("option-closed");
    }
    private void OnClickMenuPause(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void OnClickResumePause(ClickEvent evt)
    {
        _pauseContainer.RemoveFromClassList("turn-screen-open");
    }
    private void OnClickResumeOption(ClickEvent evt)
    {
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
    public void UpdateLifeBarOwnerBase(RoundManager roundManager)
    {
        float maxLife = 0;
        float actualLife = 0;
        switch (roundManager.RoundOwner)
        {
            case Owner.P1:
                foreach (GameObject p in roundManager._P1Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P2:
                foreach (GameObject p in roundManager._P2Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P3:
                foreach (GameObject p in roundManager._P3Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P4:
                foreach (GameObject p in roundManager._P4Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
            case Owner.P5:
                foreach (GameObject p in roundManager._P5Pieces)
                {
                    maxLife += p.GetComponent<Piece>().Health.MaxBarValue;
                    actualLife += p.GetComponent<Piece>().Health.CurrentBarValue;
                }
                break;
        }
        AnimateLifeBar(actualLife, maxLife);
    }
    public static void UpdateMutationPointText(RoundManager roundManager)
    {
        int points = 0;
        switch (roundManager.RoundOwner)
        {
            case Owner.P1:
                foreach (GameObject p in roundManager._P1Pieces)
                {
                    points += p.GetComponent<Piece>().PontosMutagenicos;
                }
                break;
            case Owner.P2:
                foreach (GameObject p in roundManager._P2Pieces)
                {
                    points += p.GetComponent<Piece>().PontosMutagenicos;
                }
                break;
            case Owner.P3:
                foreach (GameObject p in roundManager._P3Pieces)
                {
                    points += p.GetComponent<Piece>().PontosMutagenicos;
                }
                break;
            case Owner.P4:
                foreach (GameObject p in roundManager._P4Pieces)
                {
                    points += p.GetComponent<Piece>().PontosMutagenicos;
                }
                break;
            case Owner.P5:
                foreach (GameObject p in roundManager._P5Pieces)
                {
                    points += p.GetComponent<Piece>().PontosMutagenicos;
                }
                break;
        }
    }
}