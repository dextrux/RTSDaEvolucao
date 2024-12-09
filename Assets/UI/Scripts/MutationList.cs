using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;

public class MutationList : MonoBehaviour
{
    [SerializeField] private GameObject _creatureScreen;
    [SerializeField] private GameObject _mutationScreen;
    [SerializeField] private GameObject _buyMutationScreen;
    [SerializeField] private InGameUi _ingameUi;
    [SerializeField] private PlayerRaycast _playerRaycast;
    private Piece _actualPiece;
    private Tile _actualTile;
    private Button _creatureInfoBtn;
    private Button _actionBtn;
    private Button _buyMutationBtn;
    private Button _exitInfoBtn;

    private void OnEnable()
    {
        SetReferences();
        _creatureInfoBtn.RegisterCallback<ClickEvent>(OnClickCreatureInfo);
        _creatureInfoBtn.RegisterCallback<ClickEvent>(OnClickCreatureInfo);
        _actionBtn.RegisterCallback<ClickEvent>(OnClickAction);
        _exitInfoBtn.RegisterCallback<ClickEvent>(OnClickExitInfo);
        _buyMutationBtn.RegisterCallback<ClickEvent>(OnClickBuyMutation);
    }
    private void OnClickCreatureInfo(ClickEvent evt)
    {
        _mutationScreen.SetActive(false);
        _creatureScreen.SetActive(true);
        _creatureScreen.GetComponent<CreatureInfo>().SetPiece(_actualPiece);
    }
    private void OnClickExitInfo(ClickEvent evt)
    {
        _playerRaycast.DeselectPiece();
        _ingameUi.CreatureInfoNormal();
        _ingameUi.UpdateLifeBarOwnerBase();
        _creatureScreen.SetActive(false);
        _mutationScreen.SetActive(false);
    }
    private void OnClickBuyMutation(ClickEvent evt)
    {
        _playerRaycast.DeselectPiece();
        _creatureScreen.SetActive(false);
        _buyMutationScreen.SetActive(true);
        _buyMutationScreen.GetComponent<BuyMutationUi>().Piece = _actualPiece;
        _mutationScreen.SetActive(false);
    }
    private void OnClickAction(ClickEvent evt)
    {
        _ingameUi.CreatureInfoNormal();
        FindAnyObjectByType<PlayerRaycast>().isBlinking = true;
        _ingameUi.UpdateLifeBarOwnerBase();
        gameObject.SetActive(false);
    }
    public void SetPieceMutationScreen(Piece piece)
    {
        _actualPiece = piece;
        _actualTile = _actualPiece.PieceRaycastForTile();
        SetCreatureMutationUi();
    }
    private void SetCreatureMutationUi()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        VisualElement.Hierarchy mutationHierarchy = root.Q<VisualElement>("mutation-list").Q<VisualElement>("unity-content-container").hierarchy;
        List<Nodo<MutationBase>> list = _actualPiece.AppliedMutations.Nodos();
        Debug.Log(mutationHierarchy.childCount);
        Debug.Log(list.Count);
        for (int i = 0; i < mutationHierarchy.childCount; i++)
        {
            VisualElement.Hierarchy mutateHierarchy = mutationHierarchy.ElementAt(i).hierarchy;
            mutationHierarchy.ElementAt(i).AddToClassList("base-mutation-closed");
            mutationHierarchy.ElementAt(i).RemoveFromClassList("base-mutation-closed");
            if (i < list.Count) {
                VisualElement iconMutation = mutateHierarchy.ElementAt(0);
                IStyle style = iconMutation.style;
                StyleBackground newStyleBG = new StyleBackground(list[i].valor.Icon);
                style.backgroundImage = newStyleBG;
                VisualElement.Hierarchy textsMutation = mutateHierarchy.ElementAt(1).hierarchy;
                Label newText = textsMutation.ElementAt(0) as Label;
                newText.text = list[i].valor.Name;
                newText = textsMutation.ElementAt(1) as Label;
                newText.text = list[i].valor.Description;
            }
            else
            {
                mutationHierarchy.ElementAt(i).AddToClassList("base-mutation-closed");
            }
        }

        foreach(Nodo<MutationBase> n in _actualPiece.AppliedMutations.Nodos())
        {
            Debug.Log(n.valor.name);
        }
    }
    private void SetReferences()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _creatureInfoBtn = root.Q<Button>("floating-info-btn");
        _actionBtn = root.Q<Button>("floating-action-btn");
        _buyMutationBtn = root.Q<Button>("floating-buy-btn");
        _exitInfoBtn = root.Q<Button>("exit-info-btn");
    }
}
