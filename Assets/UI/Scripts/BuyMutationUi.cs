using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public enum MutationStatus
{
    purchased,
    available,
    selected,
    unavailable,
    incompatible
}
public class BuyMutationUi : MonoBehaviour
{
    private Button _exitBuyMutation;
    private Button _buyMutation;

    #region BotoesParaMutacoes
    private Button[] _buttons = new Button[51];
    [SerializeField] private MutationBase[] _mutations;
    private int _selectedButton = 0;
    #endregion

    private Piece _actualPiece;
    private MutationBase _selectedMutation;
    private VisualElement _icon;
    private Label _title;
    private Label _description;

    [SerializeField] private PlayerRaycast _playerRaycast;
    [SerializeField] private InGameUi _ingameUi;
    //som
    [SerializeField] private AudioClip _buttonConfirmation;
    [SerializeField] private AudioClip _buttonDenial;
    [SerializeField] private List<MutationBase> _applied;
    [SerializeField] private List<MutationBase> _incompatible;
    public Piece Piece { get => _actualPiece; set => _actualPiece = value; }

    private void Awake()
    {
        _selectedButton = 0;
        _selectedMutation = Resources.Load<MutationBase>("Mutation/01Herbivore");
    }
    private void SetComponents()
    {
        int counter = 0;
        var rootTest = GetComponent<UIDocument>().rootVisualElement;
        _icon = rootTest.Q<VisualElement>("mutation-icon");
        _title = rootTest.Q<Label>("mutation-title");
        _description = rootTest.Q<Label>("mutation-descrition");
        _buyMutation = rootTest.Q<Button>("buy-mutation-btn");
        VisualElement.Hierarchy mutationHierarchy = rootTest.Q<VisualElement>("testing-scroll").Q<VisualElement>("unity-content-container").hierarchy;
        for (int i = 0; i < 14; i++)
        {
            VisualElement.Hierarchy mutationLine = mutationHierarchy.ElementAt(i).hierarchy;
            for (int j = 0; j < mutationLine.childCount; j++)
            {
                if (mutationLine.ElementAt(j).name.Length > 1)
                {
                    _buttons[counter] = mutationLine.ElementAt(j) as Button;
                    counter++;
                }
            }
        }
        _exitBuyMutation = rootTest.Q<Button>("exit-mutation-btn");
        _buttons[1] = rootTest.Q<Button>("carnivoro-btn");
        _buttons[2] = rootTest.Q<Button>("onivoro-btn");
        _buttons[3] = rootTest.Q<Button>("agressivo-btn");
        _buttons[4] = rootTest.Q<Button>("fugitivo-btn");
        _buttons[5] = rootTest.Q<Button>("social-btn");
        _buttons[6] = rootTest.Q<Button>("necrofago-btn");
        _buttons[7] = rootTest.Q<Button>("porte-grande-btn");
        _buttons[8] = rootTest.Q<Button>("familiar-btn");
        _buttons[9] = rootTest.Q<Button>("parasita-btn");
        _buttons[10] = rootTest.Q<Button>("cavernoso-btn");
        _buttons[11] = rootTest.Q<Button>("escalador-btn");
        _buttons[12] = rootTest.Q<Button>("pe-galinha-btn");
        _buttons[13] = rootTest.Q<Button>("medio-porte-btn");
        _buttons[14] = rootTest.Q<Button>("olho-noturno-btn");
        _buttons[15] = rootTest.Q<Button>("olho-diruno-btn");
        _buttons[16] = rootTest.Q<Button>("agil-btn");
        _buttons[17] = rootTest.Q<Button>("silencioso-btn");
        _buttons[18] = rootTest.Q<Button>("emboscador-btn");
        _buttons[19] = rootTest.Q<Button>("pata-de-urso-btn");
        _buttons[20] = rootTest.Q<Button>("bico-plantas-btn");
        _buttons[21] = rootTest.Q<Button>("bico-plantas-btn");
        _buttons[22] = rootTest.Q<Button>("bico-graos-btn");
        _buttons[23] = rootTest.Q<Button>("serras-btn");
        _buttons[24] = rootTest.Q<Button>("bico-de-caca-pequena-btn");
        _buttons[25] = rootTest.Q<Button>("bico-herbivoro-btn");
        _buttons[26] = rootTest.Q<Button>("bico-fruta-btn");
        _buttons[27] = rootTest.Q<Button>("pata-de-macaco-btn");
        _buttons[28] = rootTest.Q<Button>("quelicera-btn");
        _buttons[29] = rootTest.Q<Button>("bico-de-caca-media-btn");
        _buttons[30] = rootTest.Q<Button>("bico-carnivoro-btn");
        _buttons[31] = rootTest.Q<Button>("bico-btn");
        _buttons[32] = rootTest.Q<Button>("peconha-btn");
        _buttons[33] = rootTest.Q<Button>("bico-caca-grande-btn");
        _buttons[34] = rootTest.Q<Button>("mandibula-btn");
        _buttons[35] = rootTest.Q<Button>("dentes-frutas");
        _buttons[36] = rootTest.Q<Button>("cauda-largato-escamado-btn");
        _buttons[37] = rootTest.Q<Button>("dentes-btn");
        _buttons[38] = rootTest.Q<Button>("dentes-quadrados-btn");
        _buttons[39] = rootTest.Q<Button>("cauda-largato-btn");
        _buttons[40] = rootTest.Q<Button>("cauda-felpuda-btn");
        _buttons[41] = rootTest.Q<Button>("dente-caca-media-btn");
        _buttons[42] = rootTest.Q<Button>("dente-afiado-btn");
        _buttons[43] = rootTest.Q<Button>("carreiras-dentarias-btn");
        _buttons[44] = rootTest.Q<Button>("dentes-plantas-btn");
        _buttons[45] = rootTest.Q<Button>("cauda-felpuda-pequena-btn");
        _buttons[46] = rootTest.Q<Button>("dente-caca-grande-btn");
        _buttons[47] = rootTest.Q<Button>("dente-caca-pequena-btn");
        _buttons[48] = rootTest.Q<Button>("cauda-gato-selvagem-btn");
        _buttons[49] = rootTest.Q<Button>("plumagem-pavao-btn");
    }
    public void OnEnableScreen()
    {
        SetComponents();
        _buttons[0].RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        _buttons[0].RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        _buttons[1].RegisterCallback<ClickEvent>(OnClickCarnivoreBtn);
        _buttons[2].RegisterCallback<ClickEvent>(OnClickOmnivorousBtn);/*
        _buttons[3].RegisterCallback<ClickEvent>();
        _buttons[4].RegisterCallback<ClickEvent>();
        _buttons[5].RegisterCallback<ClickEvent>();
        _buttons[6].RegisterCallback<ClickEvent>();
        _buttons[7].RegisterCallback<ClickEvent>();
        _buttons[8].RegisterCallback<ClickEvent>();
        _buttons[9].RegisterCallback<ClickEvent>();
        _buttons[10].RegisterCallback<ClickEvent>();
        _buttons[11].RegisterCallback<ClickEvent>();
        _buttons[12].RegisterCallback<ClickEvent>();
        _buttons[13].RegisterCallback<ClickEvent>();
        _buttons[14].RegisterCallback<ClickEvent>();
        _buttons[15].RegisterCallback<ClickEvent>();
        _buttons[16].RegisterCallback<ClickEvent>();
        _buttons[17].RegisterCallback<ClickEvent>();
        _buttons[18].RegisterCallback<ClickEvent>();
        _buttons[19].RegisterCallback<ClickEvent>();
        _buttons[20].RegisterCallback<ClickEvent>();
        _buttons[21].RegisterCallback<ClickEvent>();
        _buttons[22].RegisterCallback<ClickEvent>();
        _buttons[23].RegisterCallback<ClickEvent>();
        _buttons[24].RegisterCallback<ClickEvent>();
        _buttons[25].RegisterCallback<ClickEvent>();
        _buttons[26].RegisterCallback<ClickEvent>();
        _buttons[27].RegisterCallback<ClickEvent>();
        _buttons[28].RegisterCallback<ClickEvent>();
        _buttons[29].RegisterCallback<ClickEvent>();
        _buttons[30].RegisterCallback<ClickEvent>();
        _buttons[31].RegisterCallback<ClickEvent>();
        _buttons[32].RegisterCallback<ClickEvent>();
        _buttons[33].RegisterCallback<ClickEvent>();
        _buttons[34].RegisterCallback<ClickEvent>();
        _buttons[35].RegisterCallback<ClickEvent>();
        _buttons[36].RegisterCallback<ClickEvent>();
        _buttons[37].RegisterCallback<ClickEvent>();
        _buttons[38].RegisterCallback<ClickEvent>();
        _buttons[38].RegisterCallback<ClickEvent>();
        _buttons[39].RegisterCallback<ClickEvent>();
        _buttons[40].RegisterCallback<ClickEvent>();
        _buttons[41].RegisterCallback<ClickEvent>();
        _buttons[42].RegisterCallback<ClickEvent>();
        _buttons[43].RegisterCallback<ClickEvent>();
        _buttons[44].RegisterCallback<ClickEvent>();
        _buttons[45].RegisterCallback<ClickEvent>();
        _buttons[46].RegisterCallback<ClickEvent>();
        _buttons[47].RegisterCallback<ClickEvent>();
        _buttons[48].RegisterCallback<ClickEvent>();
        _buttons[49].RegisterCallback<ClickEvent>();*/
        _exitBuyMutation.RegisterCallback<ClickEvent>(ExitBuyMutation);
        _buyMutation.RegisterCallback<ClickEvent>(OnClickbuyMutationBtn);
        VerifyAll();
        _selectedButton = 0;
        _selectedMutation = Resources.Load<MutationBase>("Mutation/01Herbivore");
        SetButtonVisual(_buttons[_selectedButton], MutationStatus.selected);

    }

    private void SetButtonVisual(VisualElement target, MutationStatus stateIndex)
    {
        target.RemoveFromClassList("available-button");
        target.RemoveFromClassList("purchased-button");
        target.RemoveFromClassList("selected-button");
        target.RemoveFromClassList("unavailable-button");
        target.RemoveFromClassList("incompatible-button");
        switch (stateIndex)
        {
            case MutationStatus.purchased:
                target.AddToClassList("purchased-button");
                break;
            case MutationStatus.available:
                target.AddToClassList("available-button");
                break;
            case MutationStatus.selected:
                target.AddToClassList("selected-button");
                break;
            case MutationStatus.incompatible:
                target.AddToClassList("incompatible-button");
                break;
            default:
                target.AddToClassList("unavailable-button");
                break;
        }
    }
    private void VerifyButtonState(VisualElement target, MutationBase inspect)
    {
        foreach(MutationBase muta in _actualPiece.AppliedMutations)
        {
            _applied.Add(muta);
        }
        foreach (MutationBase muta in _actualPiece.IncompatibleMutations)
        {
            _incompatible.Add(muta);
        }
        if (_actualPiece.AppliedMutations.Contains(inspect)) SetButtonVisual(target, MutationStatus.purchased);
        else if (_actualPiece.IncompatibleMutations.Contains(inspect)) SetButtonVisual(target, MutationStatus.incompatible);
        else if (inspect.IsMutationUnlockable(_actualPiece)) SetButtonVisual(target, MutationStatus.available);
        else SetButtonVisual(target, MutationStatus.unavailable);
    }
    private void ExitBuyMutation(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonDenial, transform.position, 1);
        _playerRaycast.DeselectPiece();
        _ingameUi.CreatureInfoNormal();
        _ingameUi.UpdateLifeBarOwnerBase();
        gameObject.SetActive(false);
    }
    private void OnClickHerbivoreBtn(ClickEvent evt)
    {
        ClickSelectButton(0);
    }
    private void OnClickCarnivoreBtn(ClickEvent evt)
    {
        ClickSelectButton(1);
    }
    private void OnClickOmnivorousBtn(ClickEvent evt)
    {
        ClickSelectButton(2);
    }
    private void OnClickbuyMutationBtn(ClickEvent evt)
    {
        if (_actualPiece.AddMutation(_selectedMutation))
        {
            VerifyAll();
            SetButtonVisual(_buttons[_selectedButton], MutationStatus.selected);
        }
    }
    private void OnClickCarnivoreMutationBtn(ClickEvent evt)
    {

    }
    private void OnClickOmnivorousMutationBtn(ClickEvent evt)
    {

    }
    private void ClickSelectButton(int newSelect)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        VerifyButtonState(_buttons[_selectedButton], _selectedMutation);
        _selectedButton = newSelect;
        SetButtonVisual(_buttons[newSelect], MutationStatus.selected);
        _selectedMutation = _mutations[newSelect];
        _title.text = _selectedMutation.Name;
        _description.text = _selectedMutation.Description;
        IStyle style = _icon.style;
        StyleBackground newStyleBG = new StyleBackground(_selectedMutation.Icon);
        style.backgroundImage = newStyleBG;
    }
    private void VerifyAll()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            if (i >= _mutations.Length)
            {
                SetButtonVisual(_buttons[i], MutationStatus.unavailable);
            }
            else
            {
                VerifyButtonState(_buttons[i], _mutations[i]);
            }

        }
    }
}
