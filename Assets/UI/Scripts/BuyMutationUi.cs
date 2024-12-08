using System.Collections;
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
    private Button[] _buttons;
    [SerializeField] private MutationBase[] _mutations;
    private int _selectedButton = 0;
    #endregion

    private Piece _actualPiece;
    private MutationBase _selectedMutation;
    private int _mutationIndex;

    [SerializeField] private PlayerRaycast _playerRaycast;
    [SerializeField] private InGameUi _ingameUi;
    //som
    [SerializeField] private AudioClip _buttonConfirmation;
    [SerializeField] private AudioClip _buttonDenial;
    public Piece Piece { get => _actualPiece; set => _actualPiece = value; }

    private void Start()
    {
        _buttons = new Button[50]; //Mutacoes identificadas pelo Id-1
        SetComponents();
    }

    private void SetComponents()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _exitBuyMutation = root.Q<Button>("exit-mutation-btn");
        _buttons[0] = root.Q<Button>("herbivore-btn");
        _buttons[1] = root.Q<Button>("carnivore-btn");
        _buttons[2] = root.Q<Button>("omnivorous-btn");
        _buttons[3] = root.Q<Button>("agressivo-btn");
        _buttons[4] = root.Q<Button>("fugitivo-btn");
        _buttons[5] = root.Q<Button>("social-btn");
        _buttons[6] = root.Q<Button>("necrofago-btn");
        _buttons[7] = root.Q<Button>("porte-grande-btn");
        _buttons[8] = root.Q<Button>("familiar-btn");
        _buttons[9] = root.Q<Button>("parasita-btn");
        _buttons[10] = root.Q<Button>("cavernoso-btn");
        _buttons[11] = root.Q<Button>("escalador-btn");
        _buttons[12] = root.Q<Button>("pe-galinha-btn");
        _buttons[13] = root.Q<Button>("medio-porte-btn");
        _buttons[14] = root.Q<Button>("olho-noturno-btn");
        _buttons[15] = root.Q<Button>("olho-diruno-btn");
        _buttons[16] = root.Q<Button>("agil-btn");
        _buttons[17] = root.Q<Button>("silencioso-btn");
        _buttons[18] = root.Q<Button>("emboscador-btn");
        _buttons[19] = root.Q<Button>("pata-de-urso-btn");
        _buttons[20] = root.Q<Button>("bico-plantas-btn");
        _buttons[21] = root.Q<Button>("bico-plantas-btn");
        _buttons[22] = root.Q<Button>("bico-graos-btn");
        _buttons[23] = root.Q<Button>("serras-btn");
        _buttons[24] = root.Q<Button>("bico-de-caca-pequena-btn");
        _buttons[25] = root.Q<Button>("bico-herbivoro-btn");
        _buttons[26] = root.Q<Button>("bico-fruta-btn");
        _buttons[27] = root.Q<Button>("pata-de-macaco-btn");
        _buttons[28] = root.Q<Button>("quelicera-btn");
        _buttons[29] = root.Q<Button>("bico-de-caca-media-btn");
        _buttons[30] = root.Q<Button>("bico-carnivoro-btn");
        _buttons[31] = root.Q<Button>("bico-btn");
        _buttons[32] = root.Q<Button>("peconha-btn");
        _buttons[33] = root.Q<Button>("bico-caca-grande-btn");
        _buttons[34] = root.Q<Button>("mandibula-btn");
        _buttons[35] = root.Q<Button>("dentes-frutas");
        _buttons[36] = root.Q<Button>("cauda-largato-escamado-btn");
        _buttons[37] = root.Q<Button>("dentes-btn");
        _buttons[38] = root.Q<Button>("dentes-quadrados-btn");
        _buttons[39] = root.Q<Button>("cauda-largato-btn");
        _buttons[40] = root.Q<Button>("cauda-felpuda-btn");
        _buttons[41] = root.Q<Button>("dente-caca-media-btn");
        _buttons[42] = root.Q<Button>("dente-afiado-btn");
        _buttons[43] = root.Q<Button>("carreiras-dentarias-btn");
        _buttons[44] = root.Q<Button>("dentes-plantas-btn");
        _buttons[45] = root.Q<Button>("cauda-felpuda-pequena-btn");
        _buttons[46] = root.Q<Button>("dente-caca-grande-btn");
        _buttons[47] = root.Q<Button>("dente-caca-pequena-btn");
        _buttons[48] = root.Q<Button>("cauda-gato-selvagem-btn");
        _buttons[49] = root.Q<Button>("plumagem-pavao-btn");
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
