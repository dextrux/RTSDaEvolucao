using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    #region Constantes
    private const float MaxFoodQuantity = 100f;
    private const float InitialFoodQuantity = 10f;
    private const float RayDistanceTile = 5f;
    private static readonly Vector3 InactivePositionOffset = new Vector3(0, -1, 0);
    private static readonly Vector3 ActivePositionOffset = new Vector3(0, 5, 0);
    #endregion

    #region Atributos
    private Renderer _renderer;
    private MeshFilter _meshFilter;
    private float _foodQuantity;
    private TotemType _totemType;
    //P = 0,
    //M = 1,
    //G = 2,
    //Frutas = 3,
    //Graos = 4,
    //Plantas = 5,
    //Ponto_Mutagenico = 6,
    //Corpo = 7,
    public List<GameObject> _totemModes = new List<GameObject>();
    #endregion

    #region Atributos de Inspetor
    public LayerMask tileLayerMask;
    #endregion

    #region Propriedades
    public TotemType TotemType
    {
        get => _totemType;
        set => _totemType = value;
    }

    public float FoodQuantity
    {
        get => _foodQuantity;
        set => _foodQuantity = Mathf.Min(value, MaxFoodQuantity);
    }

    public float MultiplyFoodQuantityByFactor
    {
        set => FoodQuantity *= value;
    }
    #endregion

    #region M�todos de Inicializa��o
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }
    #endregion

    #region M�todos de Ativa��o e Desativa��o do Totem
    public void ActivateTotem(TotemType totemType)
    {
        TotemType = totemType;
        FoodQuantity = InitialFoodQuantity;

        UpdateTotemAppearance(totemType);
        SetTotemPosition(ActivePositionOffset);

        var tile = PerformRaycastToTile();
        if (tile != null)
        {
            if (totemType == TotemType.Ponto_Mutagenico)
            {
                tile.TileType = TileType.Mutagencio;
            }
            else if (totemType != TotemType.Ponto_Mutagenico)
            {
                tile.TileType = TileType.Comida;
            }       
        }

        gameObject.SetActive(true);
    }

    public void DeactivateTotem()
    {
        var tile = PerformRaycastToTile();
        if (tile != null)
        {
            tile.TileType = TileType.Posicionamento;
        }

        TotemType = TotemType.None;
        SetTotemPosition(InactivePositionOffset);

        gameObject.SetActive(false);
    }
    #endregion

    #region M�todos Auxiliares
    private void UpdateTotemAppearance(TotemType totemType)
    {
        SetTotemPosition(new Vector3(0f,1f,0f));
        for(int i = 0; i < _totemModes.Count; i++)
        {
            if (i != (int)totemType)
            {
                _totemModes[i].SetActive(false);
            }
            else
            {
                _totemModes[i].SetActive(true);
            }
        }
    }

    private void SetTotemPosition(Vector3 offset)
    {
        transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
    }

    private Tile PerformRaycastToTile()
    {
        Vector3 rayOrigin = transform.position;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, RayDistanceTile, tileLayerMask))
        {
            Debug.DrawRay(rayOrigin, Vector3.down * RayDistanceTile, Color.green);

            if (hit.collider.CompareTag("Tile"))
            {
                return hit.collider.GetComponent<Tile>();
            }
        }

        Debug.DrawRay(rayOrigin, Vector3.down * RayDistanceTile, Color.red);
        return null;
    }
    #endregion
}
