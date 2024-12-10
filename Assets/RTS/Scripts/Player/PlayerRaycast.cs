using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    #region Atributos da Classe
    public Camera mainCamera;
    public float rayDistance = Mathf.Infinity;
    public LayerMask layerMask;
    public Owner playerCamOwner;
    public GameObject[] selectedObjects = new GameObject[2];
    public GameObject Manager;
    [SerializeField] private CreatureInfo _creatureInfo;
    private GameObject chosenTileUpdate;
    public bool rayPossible = true;
    #endregion

    #region Métodos Unity
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MouseRaycast();
        }
        if (isBlinking)
        {
            colorTimer += Time.deltaTime;

            if (colorTimer >= 0.7f)
            {
                // Alterna entre os métodos
                if (Pintar)
                {
                    Tile.ColorirTilesDuranteSeleção(chosenTileUpdate.GetComponent<Tile>());
                }
                else
                {
                    Tile.RetornarTilesAdjacentesParaMaterialOriginal(chosenTileUpdate.GetComponent<Tile>());
                }

                // Alterna a condição
                Pintar = !Pintar;

                // Reinicia o temporizador
                colorTimer = 0f;
            }
        }
    }
    #endregion

    #region Métodos de Raycast
    private void MouseRaycast()
    {
        if (rayPossible)
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
    }
    #endregion

    #region Manipulação de Seleção
    private void HandlePieceSelection(GameObject hitPiece)
    {
        Piece pieceScript = hitPiece.GetComponent<Piece>();

        // Se nenhuma peça estiver selecionada
        if (selectedObjects[0] == null && pieceScript.Owner == playerCamOwner)
        {
            if (!pieceScript.IsDuringAction && pieceScript.Owner == playerCamOwner)
            {
                SelectPiece(hitPiece);
                chosenTileUpdate = pieceScript.PieceRaycastForTile().gameObject;
            }
        }
        else
        {
            if (selectedObjects[0] != hitPiece)
            {
                DeselectPiece();
                if (!pieceScript.IsDuringAction && pieceScript.Owner == playerCamOwner)
                {
                    SelectPiece(hitPiece);
                    chosenTileUpdate = pieceScript.PieceRaycastForTile().gameObject;
                }
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
            FimDoBlink(tileScript);
            ExecuteAction(selectedObjects[0], selectedObjects[1], selectedObjects[0].GetComponent<Piece>().PieceRaycastForTile());
        }
    }

    private void SelectPiece(GameObject piece)
    {
        mainCamera.GetComponent<PlayerCam>().ChangeCameraPositionToFocusPiece(piece);
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
            FimDoBlink(pieceScript.PieceRaycastForTile());

        }
        ResetSelection();
    }

    private void SelectTile(GameObject tile)
    {
        selectedObjects[1] = tile;
    }
    #endregion

    #region Execução de Ações
    private void ExecuteAction(GameObject piece, GameObject tile, Tile LatsTile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        Piece pieceScript = piece.GetComponent<Piece>();
        isBlinking = false;
        pieceScript.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
        if (!pieceScript.IsDuringAction)
        {
            pieceScript.IsDuringAction = true;

            if (tileScript.Owner == Owner.None)
            {
                if (tileScript.TileType == TileType.Posicionamento)
                {
                    Debug.Log("Andar");
                    pieceScript.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
                    pieceScript.StartCoroutine(pieceScript.Walk(pieceScript, tile, false, LatsTile));
                }
                else if (tileScript.TileType == TileType.Comida || tileScript.TileType == TileType.Mutagencio)
                {
                    Debug.Log("Comer");
                    pieceScript.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
                    pieceScript.Eat(pieceScript.gameObject, tile, LatsTile);
                }
                else if (tileScript.TileType == TileType.Barreira)
                {
                    pieceScript.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
                    pieceScript.IsDuringAction = false;
                }
            }
            else if (tileScript.Owner != pieceScript.Owner)
            {
                Debug.Log("Lutar");
                pieceScript.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
                pieceScript.Fight(pieceScript.gameObject, tile, LatsTile);
            }
            else if (tileScript.Owner == pieceScript.Owner)
            {
                pieceScript.PieceRaycastForTile().RetornarTilesParaMaterialOriginal();
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

    #region Cores
    float colorTimer = 0;
    public bool isBlinking;
    bool Pintar = true;
    public void FimDoBlink(Tile tile)
    {
        isBlinking = false;
        Tile.RetornarTilesAdjacentesParaMaterialOriginal(tile);
    }
    #endregion
}

