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

    #region Métodos de Inicialização
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _meshFilter = GetComponent<MeshFilter>();
    }
    #endregion

    #region Métodos de Ativação e Desativação do Totem
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
                tile.TileType = TileType.Mutagêncio;
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

    #region Métodos Auxiliares
    private void UpdateTotemAppearance(TotemType totemType)
    {
        TotemTypeReferences totempTypeScript = GameObject.FindAnyObjectByType<TotemTypeReferences>();
        this._renderer.material = totempTypeScript.GetTotemTypeMaterial(totemType);
        this._meshFilter.mesh = totempTypeScript.GetTotemTypeMesh(totemType);
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
