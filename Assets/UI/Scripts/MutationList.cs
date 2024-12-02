using UnityEngine.UIElements;
using UnityEngine;
using ArvoreAVL;

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
        _ingameUi.UpdateLifeBarOwnerBase(GameObject.Find("Manager").GetComponent<RoundManager>());
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
        _actualTile.ColorirTilesDuranteSeleção();
        _ingameUi.UpdateLifeBarOwnerBase(GameObject.Find("Manager").GetComponent<RoundManager>());
        gameObject.SetActive(false);
    }
    public void SetPieceMutationScreen(Piece piece)
    {
        _actualPiece = piece;
        _actualTile = _actualPiece.PieceRaycastForTile();
        SetCreatureMutationUi(piece);
    }
    private void SetCreatureMutationUi(Piece piece)
    {
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
