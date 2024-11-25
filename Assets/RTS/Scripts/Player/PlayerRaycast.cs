using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    // Atributos da classe
    public Camera mainCamera;
    public float rayDistance = 100f;
    public LayerMask layerMask;
    public Owner playerCamOwner;
    public GameObject[] selectedObjects = new GameObject[2];
    public GameObject Manager;
    //&& Manager.GetComponent<RoundManager>().RoundOwner == playerCamOwner
    [SerializeField] private CreatureInfo _creatureInfo;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseRaycast();
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

            // Verifica se o objeto atingido tem a tag "Piece"
            if (hit.collider.CompareTag("Piece"))
            {
                GameObject hitPiece = hit.collider.gameObject;

                // Se o primeiro objeto não estiver selecionado, selecione-o
                if (selectedObjects[0] == null)
                {
                    if (!hit.collider.gameObject.GetComponent<Piece>().IsDuringAction && hit.collider.gameObject.GetComponent<Piece>().Owner == playerCamOwner)
                    {
                        selectedObjects[0] = hitPiece;
                        _creatureInfo.gameObject.SetActive(true);
                        _creatureInfo.SetPiece(hitPiece.GetComponent<Piece>());
                    }
                }
                else
                {
                    // Reseta a seleção anterior e atualiza o objeto selecionado
                    if (selectedObjects[0] == hit.collider.gameObject && !hit.collider.gameObject.GetComponent<Piece>().IsDuringAction)
                    {
                        selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().RetornarTilesAdjacentesParaMaterialOriginal();
                        ResetSelection();
                    }
                    else
                    {
                        selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().RetornarTilesAdjacentesParaMaterialOriginal();
                        ResetSelection();
                        selectedObjects[0] = hitPiece;
                        _creatureInfo.gameObject.SetActive(true);
                        _creatureInfo.SetPiece(hitPiece.GetComponent<Piece>());
                    }
                }
            }
            // Verifica se o objeto atingido tem a tag "Tile"
            else if (hit.collider.CompareTag("Tile"))
            {
                GameObject hitTile = hit.collider.gameObject;

                // Executa a ação apenas se o primeiro objeto estiver selecionado e o tile não for o mesmo da peça
                if (selectedObjects[0] != null && selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().gameObject != hitTile && selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().TilesAdjacentes.Contains(hitTile))
                {
                    selectedObjects[1] = hitTile;
                    selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().GetComponent<Tile>().RetornarTilesAdjacentesParaMaterialOriginal();
                    ExecuteAction(selectedObjects[0], selectedObjects[1]);
                }
            }
        }
        else
        {
            // Caso o raycast não atinja nada, desenha o raio em verde
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 2f);
        }
    }


    private void ExecuteAction(GameObject piece, GameObject tile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        Piece pieceScript = piece.GetComponent<Piece>();
        if (!pieceScript.IsDuringAction)
        {
            pieceScript.IsDuringAction = true;
            tileScript.RetornarTilesAdjacentesParaMaterialOriginal();
            //Vazio
            if (tileScript.Owner == Owner.None && tileScript.TileType == TileType.Posicionamento)
            {
                Debug.Log("Andar");
                pieceScript.StartCoroutine(pieceScript.Walk(pieceScript,tile, false));
            }
            //Alimentação
            if (tileScript.Owner == Owner.None && tileScript.TileType == TileType.Comida)
            {
                Debug.Log("Comer");
                pieceScript.Eat(pieceScript.gameObject,tile);
            }
            //Inimigo
            if (tileScript.Owner != Owner.None && tileScript.Owner != pieceScript.Owner)
            {
                Debug.Log("Lutar");
                pieceScript.Fight(pieceScript.gameObject, tile);
            }
            //Reprodução
            if (tileScript.Owner != Owner.None && tileScript.Owner == pieceScript.Owner)
            {
                if(tileScript.TileRaycastForPiece().Fertility.CurrentBarValue == 100 && pieceScript.Fertility.CurrentBarValue == 100)
                {
                    bool _thereIsEmptyTile = false;
                    List<GameObject> _availables = new List<GameObject>();
                    for (int i = 0; i < tileScript.TilesAdjacentes.Count; i++)
                    {
                        if (tileScript.TilesAdjacentes[i].GetComponent<Tile>().Owner == Owner.None && tileScript.TilesAdjacentes[i].GetComponent<Tile>().TileType == TileType.Posicionamento)
                        {
                            _thereIsEmptyTile = true;
                            if (!_availables.Contains(tileScript.TilesAdjacentes[i]))
                            {
                                _availables.Add(tileScript.TilesAdjacentes[i]);
                            }
                        }
                    }
                    if (_thereIsEmptyTile)
                    {
                        Debug.Log("Reproduzir");
                        pieceScript.Reproduce(piece, tile, _availables);
                    }
                }
            }
            ResetSelection();
        }
    }

    // Método para resetar a seleção
    private void ResetSelection()
    {
        selectedObjects[0] = null;
        selectedObjects[1] = null;
    }
}
