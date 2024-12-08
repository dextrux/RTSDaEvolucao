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

    #region BotoesParaMutacoes
    private Button[] _buttons = new Button[51];
    [SerializeField] private MutationBase[] _mutations;
    //private int _selectedButton = 0;
    #endregion

    private Piece _actualPiece;
    //private MutationBase _selectedMutation;

    [SerializeField] private PlayerRaycast _playerRaycast;
    [SerializeField] private InGameUi _ingameUi;
    //som
    [SerializeField] private AudioClip _buttonConfirmation;
    [SerializeField] private AudioClip _buttonDenial;
    public Piece Piece { get => _actualPiece; set => _actualPiece = value; }

    private void SetComponents()
    {
        int counter = 0;
        var rootTest = GetComponent<UIDocument>().rootVisualElement;
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
        Debug.Log(counter);
        _exitBuyMutation = rootTest.Q<Button>("exit-mutation-btn");
        Button testButton = rootTest.Q<Button>("test-button");
        Debug.Log(testButton.name);
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
    private void OnEnable()
    {
        SetComponents();
        if (_buttons[0] != null)
            _buttons[0].RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        else
            Debug.LogError("Button herbivore-btn is null before registering callback.");
        /*_buttons[0].RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        _buttons[1].RegisterCallback<ClickEvent>(OnClickCarnivoreBtn);
        _buttons[2].RegisterCallback<ClickEvent>(OnClickOmnivorousBtn);
        _exitBuyMutation.RegisterCallback<ClickEvent>(ExitBuyMutation);*/
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
        /*SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        MutationBase herbivoro = Resources.Load<MutationBase>("Mutation/01Herbivore");
        if (_actualPiece.AppliedMutations.Pesquisar(herbivoro))
        {

        } else
        {
            _actualPiece.AppliedMutations.Inserir(herbivoro);
        }*/
    }
    private void OnClickCarnivoreBtn(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        MutationBase carnivore = Resources.Load<MutationBase>("Mutation/02Carnivore");
        if (_actualPiece.AppliedMutations.Pesquisar(carnivore))
        {

        }
        else
        {
            _actualPiece.AppliedMutations.Inserir(carnivore);
        }
    }
    private void OnClickOmnivorousBtn(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        MutationBase Omnivorous = Resources.Load<MutationBase>("Mutation/03Omnivorous");
        if (_actualPiece.AppliedMutations.Pesquisar(Omnivorous))
        {

        }
        else
        {
            _actualPiece.AppliedMutations.Inserir(Omnivorous);
        }
    }
    private void OnClickHerbivoreMutationBtn(ClickEvent evt)
    {

    }
    private void OnClickCarnivoreMutationBtn(ClickEvent evt)
    {

    }
    private void OnClickOmnivorousMutationBtn(ClickEvent evt)
    {

    }
}
