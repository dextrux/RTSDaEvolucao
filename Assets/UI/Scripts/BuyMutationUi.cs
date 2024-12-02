using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuyMutationUi : MonoBehaviour
{
    private Button _exitBuyMutation;
    private Button _herbivoreBtn;
    private Button _carnivoreBtn;
    private Button _omnivoreBtn;
    private Button _herbivoreMutationBtn;
    private Button _carnivoreMutationBtn;
    private Button _omnivoreMutationBtn;
    private Piece _actualPiece;
    private MutationBase _selectedMutation;
    private int _mutationIndex;
    public Piece Piece { get => _actualPiece; set => _actualPiece = value; }
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _exitBuyMutation = root.Q<Button>("exit-mutation-btn");
        _herbivoreBtn = root.Q<Button>("herbivore-btn");
        _carnivoreBtn = root.Q<Button>("carnivore-btn");
        _omnivoreBtn = root.Q<Button>("omnivorous-btn");
        _herbivoreMutationBtn = root.Q<Button>("herbivore-mutation-btn");
        _carnivoreMutationBtn = root.Q<Button>("carnivore-mutation-btn");
        _omnivoreMutationBtn = root.Q<Button>("omnivorous-mutation-btn");
        _herbivoreBtn.RegisterCallback<ClickEvent>(OnClickHerbivoreBtn);
        _carnivoreBtn.RegisterCallback<ClickEvent>(OnClickCarnivoreBtn);
        _omnivoreBtn.RegisterCallback<ClickEvent>(OnClickOmnivorousBtn);
        _herbivoreMutationBtn.RegisterCallback<ClickEvent>(OnClickHerbivoreMutationBtn);
        _carnivoreMutationBtn.RegisterCallback<ClickEvent>(OnClickCarnivoreMutationBtn);
        _omnivoreMutationBtn.RegisterCallback<ClickEvent>(OnClickOmnivorousMutationBtn);
    _exitBuyMutation.RegisterCallback<ClickEvent>(ExitBuyMutation);
    }
    private void ExitBuyMutation(ClickEvent evt)
    {
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
        _herbivoreBtn.AddToClassList("purchased-button");
        _carnivoreBtn.RemoveFromClassList("purchased-button");
        _omnivoreBtn.RemoveFromClassList("purchased-button");
        _herbivoreMutationBtn.AddToClassList("unavailable-button");
        _carnivoreMutationBtn.AddToClassList("unavailable-button");
        _omnivoreMutationBtn.AddToClassList("unavailable-button");
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
        _carnivoreBtn.AddToClassList("purchased-button");
        _omnivoreBtn.RemoveFromClassList("purchased-button");
        _herbivoreMutationBtn.AddToClassList("unavailable-button");
        _carnivoreMutationBtn.AddToClassList("unavailable-button");
        _omnivoreMutationBtn.AddToClassList("unavailable-button");
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
        _omnivoreBtn.AddToClassList("purchased-button");
        _herbivoreMutationBtn.AddToClassList("unavailable-button");
        _carnivoreMutationBtn.AddToClassList("unavailable-button");
        _omnivoreBtn.RemoveFromClassList("unavailable-button");
        _omnivoreMutationBtn.AddToClassList("available-button");
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
