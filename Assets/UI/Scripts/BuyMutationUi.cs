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
    private Label _dnaCount;

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
        _dnaCount = rootTest.Q<Label>("dna-count-txt");
        _buttons[0] = rootTest.Q<Button>("herbivoro-btn");
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
        _buttons[20] = rootTest.Q<Button>("pequeno-porte-btn");
        _buttons[21] = rootTest.Q<Button>("bico-herbivoro-btn");
        _buttons[22] = rootTest.Q<Button>("bico-graos-btn");
        _buttons[23] = rootTest.Q<Button>("serras-btn");
        _buttons[24] = rootTest.Q<Button>("bico-de-caca-pequena-btn");
        _buttons[25] = rootTest.Q<Button>("bico-plantas-btn");
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
        _buttons[39] = rootTest.Q<Button>("dentes-graos-btn");
        _buttons[40] = rootTest.Q<Button>("cauda-largato-btn");
        _buttons[41] = rootTest.Q<Button>("cauda-felpuda-btn");
        _buttons[42] = rootTest.Q<Button>("dente-caca-grande-btn");
        _buttons[43] = rootTest.Q<Button>("dente-afiado-btn");
        _buttons[44] = rootTest.Q<Button>("carreiras-dentarias-btn");
        _buttons[45] = rootTest.Q<Button>("dentes-plantas-btn");
        _buttons[46] = rootTest.Q<Button>("cauda-felpuda-pequena-btn");
        _buttons[47] = rootTest.Q<Button>("dente-caca-media-btn");
        _buttons[48] = rootTest.Q<Button>("dente-caca-pequena-btn");
        _buttons[49] = rootTest.Q<Button>("cauda-gato-selvagem-btn");
        _buttons[50] = rootTest.Q<Button>("plumagem-pavao-btn");
    }
    public void OnEnableScreen()
    {
        FindAnyObjectByType<PlayerRaycast>().rayPossible = false;
        SetComponents();
        _buttons[0].RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        _buttons[1].RegisterCallback<ClickEvent>(OnClickCarnivoreBtn);
        _buttons[2].RegisterCallback<ClickEvent>(OnClickOmnivorousBtn);
        _buttons[3].RegisterCallback<ClickEvent>(OnClickAggressiveBtn);
        _buttons[4].RegisterCallback<ClickEvent>(OnClickFugitiveBtn);
        _buttons[5].RegisterCallback<ClickEvent>(OnClickSocialBtn);
        _buttons[6].RegisterCallback<ClickEvent>(OnClickNecrophageBtn);
        _buttons[7].RegisterCallback<ClickEvent>(OnClickLargeSizeBtn);
        _buttons[8].RegisterCallback<ClickEvent>(OnClickFamiliarBtn);
        _buttons[9].RegisterCallback<ClickEvent>(OnClickParasiteBtn);
        _buttons[10].RegisterCallback<ClickEvent>(OnClickCavernousBtn);
        _buttons[11].RegisterCallback<ClickEvent>(OnClickClimberBtn);
        _buttons[12].RegisterCallback<ClickEvent>(OnClickChickenFootBtn);
        _buttons[13].RegisterCallback<ClickEvent>(OnClickMediumSizeBtn);
        _buttons[14].RegisterCallback<ClickEvent>(OnClickNightVisionBtn);
        _buttons[15].RegisterCallback<ClickEvent>(OnClickDayVisionBtn);
        _buttons[16].RegisterCallback<ClickEvent>(OnClickAgileBtn);
        _buttons[17].RegisterCallback<ClickEvent>(OnClickSilentBtn);
        _buttons[18].RegisterCallback<ClickEvent>(OnClickAmbusherBtn);
        _buttons[19].RegisterCallback<ClickEvent>(OnClickBearPawBtn);
        _buttons[20].RegisterCallback<ClickEvent>(OnClickPlantBeakBtn);
        _buttons[21].RegisterCallback<ClickEvent>(OnClickGrainBeakBtn);
        _buttons[22].RegisterCallback<ClickEvent>(OnClickSawBtn);
        _buttons[23].RegisterCallback<ClickEvent>(OnClickSmallHuntingBeakBtn);
        _buttons[24].RegisterCallback<ClickEvent>(OnClickHerbivorousBeakBtn);
        _buttons[25].RegisterCallback<ClickEvent>(OnClickFruitBeakBtn);
        _buttons[26].RegisterCallback<ClickEvent>(OnClickMonkeyPawBtn);
        _buttons[27].RegisterCallback<ClickEvent>(OnClickCheliceraBtn);
        _buttons[28].RegisterCallback<ClickEvent>(OnClickMediumHuntingBeakBtn);
        _buttons[29].RegisterCallback<ClickEvent>(OnClickCarnivorousBeakBtn);
        _buttons[30].RegisterCallback<ClickEvent>(OnClickGenericBeakBtn);
        _buttons[31].RegisterCallback<ClickEvent>(OnClickVenomBtn);
        _buttons[32].RegisterCallback<ClickEvent>(OnClickLargeHuntingBeakBtn);
        _buttons[33].RegisterCallback<ClickEvent>(OnClickMandibleBtn);
        _buttons[34].RegisterCallback<ClickEvent>(OnClickFruitTeethBtn);
        _buttons[35].RegisterCallback<ClickEvent>(OnClickLizardTailBtn);
        _buttons[36].RegisterCallback<ClickEvent>(OnClickGenericTeethBtn);
        _buttons[37].RegisterCallback<ClickEvent>(OnClickSquareTeethBtn);
        _buttons[38].RegisterCallback<ClickEvent>(OnClickFurryTailBtn);
        _buttons[39].RegisterCallback<ClickEvent>(OnClickMediumHuntingTeethBtn);
        _buttons[40].RegisterCallback<ClickEvent>(OnClickSharpTeethBtn);
        _buttons[41].RegisterCallback<ClickEvent>(OnClickDentalRowsBtn);
        _buttons[42].RegisterCallback<ClickEvent>(OnClickPlantTeethBtn);
        _buttons[43].RegisterCallback<ClickEvent>(OnClickSmallFurryTailBtn);
        _buttons[44].RegisterCallback<ClickEvent>(OnClickLargeHuntingTeethBtn);
        _buttons[45].RegisterCallback<ClickEvent>(OnClickSmallHuntingTeethBtn);
        _buttons[46].RegisterCallback<ClickEvent>(OnClickWildcatTailBtn);
        _buttons[47].RegisterCallback<ClickEvent>(OnClickPeacockFeathersBtn);

        _exitBuyMutation.RegisterCallback<ClickEvent>(ExitBuyMutation);
        _buyMutation.RegisterCallback<ClickEvent>(OnClickbuyMutationBtn);
        VerifyAll();
        _selectedButton = 0;
        _selectedMutation = Resources.Load<MutationBase>("Mutation/01Herbivore");
        SetButtonVisual(_buttons[_selectedButton], MutationStatus.selected);
        _dnaCount.text = GameObject.FindAnyObjectByType<RoundManager>().GetMutationPointOwnerBased(_actualPiece.Owner).ToString();
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
        foreach (MutationBase muta in _actualPiece.AppliedMutations)
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
        FindAnyObjectByType<PlayerRaycast>().rayPossible = true;
        SoundManagerSO.PlaySoundFXClip(_buttonDenial, transform.position, 1);
        _playerRaycast.DeselectPiece();
        _ingameUi.CreatureInfoNormal();
        _ingameUi.UpdateLifeBarOwnerBase();
        gameObject.SetActive(false);
    }
    #region Adicionar função aos botões
    private void OnClickHerbivoreBtn(ClickEvent evt) { ClickSelectButton(0); }
    private void OnClickCarnivoreBtn(ClickEvent evt) { ClickSelectButton(1); }
    private void OnClickOmnivorousBtn(ClickEvent evt) { ClickSelectButton(2); }
    private void OnClickAggressiveBtn(ClickEvent evt) { ClickSelectButton(3); }
    private void OnClickFugitiveBtn(ClickEvent evt) { ClickSelectButton(4); }
    private void OnClickSocialBtn(ClickEvent evt) { ClickSelectButton(5); }
    private void OnClickNecrophageBtn(ClickEvent evt) { ClickSelectButton(6); }
    private void OnClickLargeSizeBtn(ClickEvent evt) { ClickSelectButton(7); }
    private void OnClickFamiliarBtn(ClickEvent evt) { ClickSelectButton(8); }
    private void OnClickParasiteBtn(ClickEvent evt) { ClickSelectButton(9); }
    private void OnClickCavernousBtn(ClickEvent evt) { ClickSelectButton(10); }
    private void OnClickClimberBtn(ClickEvent evt) { ClickSelectButton(11); }
    private void OnClickChickenFootBtn(ClickEvent evt) { ClickSelectButton(12); }
    private void OnClickMediumSizeBtn(ClickEvent evt) { ClickSelectButton(13); }
    private void OnClickNightVisionBtn(ClickEvent evt) { ClickSelectButton(14); }
    private void OnClickDayVisionBtn(ClickEvent evt) { ClickSelectButton(15); }
    private void OnClickAgileBtn(ClickEvent evt) { ClickSelectButton(16); }
    private void OnClickSilentBtn(ClickEvent evt) { ClickSelectButton(17); }
    private void OnClickAmbusherBtn(ClickEvent evt) { ClickSelectButton(18); }
    private void OnClickBearPawBtn(ClickEvent evt) { ClickSelectButton(19); }
    private void OnClickPlantBeakBtn(ClickEvent evt) { ClickSelectButton(20); }
    private void OnClickGrainBeakBtn(ClickEvent evt) { ClickSelectButton(21); }
    private void OnClickSawBtn(ClickEvent evt) { ClickSelectButton(22); }
    private void OnClickSmallHuntingBeakBtn(ClickEvent evt) { ClickSelectButton(23); }
    private void OnClickHerbivorousBeakBtn(ClickEvent evt) { ClickSelectButton(24); }
    private void OnClickFruitBeakBtn(ClickEvent evt) { ClickSelectButton(25); }
    private void OnClickMonkeyPawBtn(ClickEvent evt) { ClickSelectButton(26); }
    private void OnClickCheliceraBtn(ClickEvent evt) { ClickSelectButton(27); }
    private void OnClickMediumHuntingBeakBtn(ClickEvent evt) { ClickSelectButton(28); }
    private void OnClickCarnivorousBeakBtn(ClickEvent evt) { ClickSelectButton(29); }
    private void OnClickGenericBeakBtn(ClickEvent evt) { ClickSelectButton(30); }
    private void OnClickVenomBtn(ClickEvent evt) { ClickSelectButton(31); }
    private void OnClickLargeHuntingBeakBtn(ClickEvent evt) { ClickSelectButton(32); }
    private void OnClickMandibleBtn(ClickEvent evt) { ClickSelectButton(33); }
    private void OnClickFruitTeethBtn(ClickEvent evt) { ClickSelectButton(34); }
    private void OnClickLizardTailBtn(ClickEvent evt) { ClickSelectButton(35); }
    private void OnClickGenericTeethBtn(ClickEvent evt) { ClickSelectButton(36); }
    private void OnClickSquareTeethBtn(ClickEvent evt) { ClickSelectButton(37); }
    private void OnClickFurryTailBtn(ClickEvent evt) { ClickSelectButton(38); }
    private void OnClickMediumHuntingTeethBtn(ClickEvent evt) { ClickSelectButton(39); }
    private void OnClickSharpTeethBtn(ClickEvent evt) { ClickSelectButton(40); }
    private void OnClickDentalRowsBtn(ClickEvent evt) { ClickSelectButton(41); }
    private void OnClickPlantTeethBtn(ClickEvent evt) { ClickSelectButton(42); }
    private void OnClickSmallFurryTailBtn(ClickEvent evt) { ClickSelectButton(43); }
    private void OnClickLargeHuntingTeethBtn(ClickEvent evt) { ClickSelectButton(44); }
    private void OnClickSmallHuntingTeethBtn(ClickEvent evt) { ClickSelectButton(45); }
    private void OnClickWildcatTailBtn(ClickEvent evt) { ClickSelectButton(46); }
    private void OnClickPeacockFeathersBtn(ClickEvent evt) { ClickSelectButton(47); }
    #endregion

    private void OnClickbuyMutationBtn(ClickEvent evt)
    {
        if (_actualPiece.AddMutation(_selectedMutation))
        {
            VerifyAll();
            SetButtonVisual(_buttons[_selectedButton], MutationStatus.selected);
            GameObject.FindAnyObjectByType<RoundManager>().AddMutationPoint(_actualPiece.Owner, (_selectedMutation.Cost *-1));
            _dnaCount.text = GameObject.FindAnyObjectByType<RoundManager>().GetMutationPointOwnerBased(_actualPiece.Owner).ToString();
        }
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
