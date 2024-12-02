using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    #region Atributos da Classe
    public Camera mainCamera;
    public float rayDistance = 100f;
    public LayerMask layerMask;
    public Owner playerCamOwner;
    public GameObject[] selectedObjects = new GameObject[2];
    public GameObject Manager;
    [SerializeField] private CreatureInfo _creatureInfo;
    #endregion

    #region Métodos Unity
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseRaycast();
        }
    }
    #endregion

    #region Métodos de Raycast
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
                HandlePieceSelection(hit.collider.gameObject);
            }
            else if (hit.collider.CompareTag("Tile"))
            {
                HandleTileSelection(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.green, 2f);
        }
    }
    #endregion

    #region Manipulação de Seleção
    private void HandlePieceSelection(GameObject hitPiece)
    {
        Piece pieceScript = hitPiece.GetComponent<Piece>();

        // Se nenhuma peça estiver selecionada
        if (selectedObjects[0] == null)
        {
            if (!pieceScript.IsDuringAction && pieceScript.Owner == playerCamOwner)
            {
                SelectPiece(hitPiece);
            }
        }
        else
        {
            // Reseta a seleção se for a mesma peça ou seleciona outra peça
            if (selectedObjects[0] == hitPiece)
            {
                DeselectPiece();
            }
            else
            {
                DeselectPiece();
                SelectPiece(hitPiece);
            }
        }
    }

    private void HandleTileSelection(GameObject hitTile)
    {
        Tile tileScript = hitTile.GetComponent<Tile>();

        if (selectedObjects[0] != null &&
            selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().TilesAdjacentes.Contains(hitTile) && !_creatureInfo.gameObject.activeSelf)
        {
            SelectTile(hitTile);
            selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile().RetornarTilesAdjacentesParaMaterialOriginal();
            ExecuteAction(selectedObjects[0], selectedObjects[1]);
        }
    }

    private void SelectPiece(GameObject piece)
    {
        selectedObjects[0] = piece;
        Piece pieceScript = piece.GetComponent<Piece>();
        _creatureInfo.gameObject.SetActive(true);
        _creatureInfo.SetPiece(pieceScript);
    }

    public void DeselectPiece()
    {
        if (selectedObjects[0] != null)
        {
            Piece pieceScript = selectedObjects[0].GetComponent<Piece>();
            pieceScript.PieceRaycastForTile().RetornarTilesAdjacentesParaMaterialOriginal();
        }
        ResetSelection();
    }

    private void SelectTile(GameObject tile)
    {
        selectedObjects[1] = tile;
    }
    #endregion

    #region Execução de Ações
    private void ExecuteAction(GameObject piece, GameObject tile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        Piece pieceScript = piece.GetComponent<Piece>();

        if (!pieceScript.IsDuringAction)
        {
            pieceScript.IsDuringAction = true;

            if (tileScript.Owner == Owner.None)
            {
                if (tileScript.TileType == TileType.Posicionamento)
                {
                    Debug.Log("Andar");
                    pieceScript.StartCoroutine(pieceScript.Walk(pieceScript, tile, false));
                }
                else if (tileScript.TileType == TileType.Comida)
                {
                    Debug.Log("Comer");
                    pieceScript.Eat(pieceScript.gameObject, tile);
                }
            }
            else if (tileScript.Owner != pieceScript.Owner)
            {
                Debug.Log("Lutar");
                pieceScript.Fight(pieceScript.gameObject, tile);
            }
            else if (tileScript.Owner == pieceScript.Owner)
            {
                HandleReproduction(tileScript, pieceScript);
            }

            ResetSelection();
        }
    }

    private void HandleReproduction(Tile tileScript, Piece pieceScript)
    {
        if (tileScript.TileRaycastForPiece().Fertility.CurrentBarValue == 100 &&
            pieceScript.Fertility.CurrentBarValue == 100)
        {
            List<GameObject> availableTiles = new List<GameObject>();

            foreach (GameObject adjacentTile in tileScript.TilesAdjacentes)
            {
                Tile adjTileScript = adjacentTile.GetComponent<Tile>();
                if (adjTileScript.Owner == Owner.None && adjTileScript.TileType == TileType.Posicionamento)
                {
                    availableTiles.Add(adjacentTile);
                }
            }

            if (availableTiles.Count > 0)
            {
                Debug.Log("Reproduzir");
                pieceScript.Reproduce(pieceScript.gameObject, tileScript.gameObject, availableTiles);
            }
        }
        else
        {
            pieceScript.IsDuringAction = false;
        }
    }
    #endregion

    #region Utilitários
    private void ResetSelection()
    {
        selectedObjects[0] = null;
        selectedObjects[1] = null;
    }
    #endregion
}
