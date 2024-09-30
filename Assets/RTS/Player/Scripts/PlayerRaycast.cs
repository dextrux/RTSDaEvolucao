using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    // Atributos da classe
    public Camera mainCamera;
    public float rayDistance = 100f;
    public LayerMask layerMask;
    public GameObject UIManagerObject;
    private UIManager uiManager;
    public GameObject gameManagerObject;
    private GameManager gameManager;
    private PlayerActions playerActions; // Referência ao PlayerActions
    private GameObject firstTile = null; // Armazena o primeiro tile
    private GameObject secondTile = null; // Armazena o segundo tile
    private Jubileuson selectedPiece = null; // Armazena a peça selecionada (Jubileuson)

    // Métodos
    private void Start()
    {
        uiManager = UIManagerObject.GetComponent<UIManager>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        playerActions = GetComponent<PlayerActions>(); // Obter o componente PlayerActions
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseRaycast();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.OpenMenuPanel();
        }
    }

    private void MouseRaycast()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
        {
            Debug.Log("Hit: " + hit.collider.name); // Adicionando log para verificar o que foi clicado
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f);
            HandleRaycastHit(hit);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 2f);
        }
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        // Se o menu não estiver ativado
        if (!uiManager.menuPanelIsActivated)
        {
            if (hit.collider.CompareTag("Tile"))
            {
                HandleTileSelection(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Piece"))
            {
                HandlePieceSelection(hit.collider.gameObject.GetComponent<Jubileuson>());
            }
        }
    }

    private void HandleTileSelection(GameObject tile)
    {
        if (firstTile == null && selectedPiece == null)
        {
            // Primeiro tile selecionado
            Debug.Log("Primeiro tile selecionado: " + tile.name);
            firstTile = tile;
            gameManager.AddToObjetosTocados(firstTile);
        }
        else if (selectedPiece != null && secondTile == null)
        {
            // Segundo tile selecionado, mover a peça para o tile
            Debug.Log("Segundo tile selecionado: " + tile.name);
            secondTile = tile;

            if (firstTile != null && selectedPiece != null)
            {
                Debug.Log("Movendo peça para o segundo tile.");
                playerActions.MoveToTile(secondTile, firstTile, selectedPiece, TileType.Próprio);
            }
            ResetSelection(); // Resetar a seleção após a ação
        }
        else
        {
            Debug.LogWarning("Seleção inválida. Resetando.");
            ResetSelection();
        }
    }

    private void HandlePieceSelection(Jubileuson piece)
    {
        if (firstTile != null && selectedPiece == null)
        {
            // Se já houver um tile selecionado e selecionamos uma peça
            Debug.Log("Peça selecionada: " + piece.name);
            selectedPiece = piece;
            gameManager.AddToObjetosTocados(piece.gameObject);
        }
        else
        {
            Debug.LogWarning("Peça já selecionada ou tile não selecionado. Resetando.");
            ResetSelection();
        }
    }

    // Método para resetar a seleção
    private void ResetSelection()
    {
        Debug.Log("Resetando seleção.");
        firstTile = null;
        secondTile = null;
        selectedPiece = null;
    }
}
