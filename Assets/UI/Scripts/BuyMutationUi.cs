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
    private Button _herbivoreBtn;
    private Button _carnivoreBtn;
    private Button _omnivoreBtn;


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

    private void SetComponents()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _exitBuyMutation = root.Q<Button>("exit-mutation-btn");
        _herbivoreBtn = root.Q<Button>("herbivore-btn");
        _carnivoreBtn = root.Q<Button>("carnivore-btn");
        _omnivoreBtn = root.Q<Button>("omnivorous-btn");
    }
    private void OnEnable()
    {
        SetComponents();
        _herbivoreBtn.RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        _carnivoreBtn.RegisterCallback<ClickEvent>(OnClickCarnivoreBtn);
        _omnivoreBtn.RegisterCallback<ClickEvent>(OnClickOmnivorousBtn);
        _exitBuyMutation.RegisterCallback<ClickEvent>(ExitBuyMutation);
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
        MutationBase herbivoro = Resources.Load<MutationBase>("Mutation/Herbivore");
        if (_actualPiece.AppliedMutations.Pesquisar(herbivoro))
        {

        } else
        {
            _actualPiece.AppliedMutations.Inserir(herbivoro);
        }
    }
    private void OnClickCarnivoreBtn(ClickEvent evt)
    {
        MutationBase carnivore = Resources.Load<MutationBase>("Mutation/Carnivore");
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
        MutationBase Omnivorous = Resources.Load<MutationBase>("Mutation/Omnivorous");
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
