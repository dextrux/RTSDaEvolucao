using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    // Atributos da classe
    public Camera mainCamera;
    public float rayDistance = 100f;
    public LayerMask layerMask;
    public GameObject UIManagerObject;
    private UIManager uiManager;
    public GameObject playerActionsobject;
    private PlayerActions playerActions;
    private GameObject firstSelectedObject = null; // Armazena o primeiro objeto selecionado (Tile)
    public GameObject Jubileu;

    void Start()
    {
        uiManager = UIManagerObject.GetComponent<UIManager>();
        playerActions = playerActionsobject.GetComponent<PlayerActions>();
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
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 2f);
            if (hit.collider.CompareTag("Piece"))
            {
                Jubileuson jub = Jubileu.GetComponent<Jubileuson>();
                jub._pieceRemainingActions--;
                if (jub._pieceRemainingActions <= 0)
                {
                    Destroy(Jubileu);
                }
                Debug.Log("Primeiro objeto selecionado: " + hit.collider.gameObject.name);
                HandleFirstSelection(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Tile"))
            {
                Jubileuson jub = Jubileu.GetComponent<Jubileuson>();
                jub._pieceRemainingActions--;
                if (jub._pieceRemainingActions <= 0)
                {
                    Destroy(Jubileu);
                }
                HandleSecondSelection(hit.collider.gameObject);
            }
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
            if (hit.collider.CompareTag("Piece"))
            {
                Jubileuson jub = Jubileu.GetComponent<Jubileuson>();
                jub._pieceRemainingActions--;
                if (jub._pieceRemainingActions <= 0)
                {
                    Destroy(Jubileu);
                }
                HandleFirstSelection(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Tile"))
            {
                Jubileuson jub = Jubileu.GetComponent<Jubileuson>();
                jub._pieceRemainingActions--;
                if (jub._pieceRemainingActions <= 0)
                {
                    Destroy(Jubileu);
                }
                // Segundo objeto (com a tag "SecondTag") selecionado
                Debug.Log("Segundo objeto selecionado: " + hit.collider.gameObject.name);

                // Ação a ser realizada após a seleção dos dois objetos
                ExecuteAction(firstSelectedObject, hit.collider.gameObject);

                // Resetar a seleção após a ação
                ResetSelection();
            }
        }
    }

    private void HandleFirstSelection(GameObject firstObject)
    {
        // Primeiro objeto (com a tag "FirstTag") selecionado
        Debug.Log("AB");
        firstSelectedObject = firstObject;
        Debug.Log("AC");
        if (firstObject.name.Trim().Equals("Totem"))
        {
            Debug.Log("BB");
            Alimentacao jubA = Jubileu.GetComponent<Alimentacao>();
            jubA.Alimentar(firstObject.gameObject);
            
        }
        else
        {
            Debug.Log("Nao e Totem");
        }

    }

    private void HandleSecondSelection(GameObject secondObject)
    {
        if (firstSelectedObject != null)
        {
            // Segundo objeto (com a tag "SecondTag") selecionado
            Debug.Log("Segundo objeto selecionado: " + secondObject.name);

            // Ação a ser realizada após a seleção dos dois objetos
            if (secondObject.name.Equals("Totem"))
            {
                Debug.Log("BBB");
                Alimentacao jubA = Jubileu.GetComponent<Alimentacao>();
                jubA.Alimentar(secondObject.gameObject);
                
            }
            else
            {
                Debug.Log("AAA");
                ExecuteAction(firstSelectedObject, secondObject);
                Jubileuson jub = Jubileu.GetComponent<Jubileuson>();
                
            }

            // Resetar a seleção após a ação
            ResetSelection();
        }
    }

    private void ExecuteAction(GameObject firstObject, GameObject secondObject)
    {

        if (secondObject.GetComponent<Tile>().GetTileType() == TileType.Vazio)
        {
            playerActions.MoveToTile(secondObject, firstObject.GetComponent<Jubileuson>().PieceRaycast(), firstObject.GetComponent<Jubileuson>(), TileType.Próprio);
        }
        else if (secondObject.GetComponent<Tile>().GetTileType() == TileType.Comida) // Assuming TileType.Alimento is a food tile type
        {
            playerActions.MoveToTile(secondObject, firstObject.GetComponent<Jubileuson>().PieceRaycast(), firstObject.GetComponent<Jubileuson>(), TileType.Próprio);
            //playerActions.Eat(firstObject.GetComponent<Jubileuson>(), secondObject.GetComponent<Tile>().GetComponent(f), secondObject, firstObject.GetComponent<Jubileuson>().PieceRaycast());
        }
        else if (secondObject.GetComponent<Tile>().GetTileType() == TileType.Inimigo) // Assuming TileType.Oponente is an enemy tile type
        {
            playerActions.Fight(firstObject.GetComponent<Jubileuson>(), secondObject.GetComponent<Tile>().TileRaycast().GetComponent<Jubileuson>(), secondObject, firstObject.GetComponent<Jubileuson>().PieceRaycast());
        }
    }

    // Método para resetar a seleção
    private void ResetSelection()
    {
        firstSelectedObject = null;
    }
}
