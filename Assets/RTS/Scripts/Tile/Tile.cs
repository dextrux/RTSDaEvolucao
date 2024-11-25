using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region Atributos
    private const float _rayDistanceTile = 3;
    private const float _rayDistanceTotem = 1;
    private const float _rayDistancePiece = 5f;

    [SerializeField] private EnviromentStatus _humidity;
    [SerializeField] private EnviromentStatus _temperature;
    [SerializeField] private List<GameObject> _tilesAdjacentes = new List<GameObject>();
    [SerializeField] private GameObject _totem;
    [SerializeField] private bool _isUnderDisastre;
    [SerializeField] private Owner _owner;

    // Atributos de inspetor para assinalação
    public Material[] materialOriginal = new Material[5];
    public float tileFirstHumidity;
    public float tileFirstTemperature;
    public TileType tileType;
    public Biome biome;
    public LayerMask tileLayerMask;
    public LayerMask totemLayerMask;
    public LayerMask pieceLayerMask;

    private Vector3[] directions = {
        Vector3.right,
        Quaternion.Euler(0, 60, 0) * Vector3.right,
        Quaternion.Euler(0, 120, 0) * Vector3.right,
        Quaternion.Euler(0, 180, 0) * Vector3.right,
        Quaternion.Euler(0, 240, 0) * Vector3.right,
        Quaternion.Euler(0, 300, 0) * Vector3.right
    };
    #endregion

    #region Propriedades
    public Owner Owner { get => _owner; set => _owner = value; }
    public TileType TileType { get => tileType; set => tileType = value; }
    public Biome Biome { get => biome; set => biome = value; }
    public EnviromentStatus Humidity => _humidity;
    public EnviromentStatus Temperature => _temperature;
    public List<GameObject> TilesAdjacentes => _tilesAdjacentes;
    public GameObject Totem { get => _totem; set => _totem = value; }
    public bool IsUnderDesastre { get => _isUnderDisastre; set => _isUnderDisastre = value; }
    #endregion

    #region Métodos de Inicialização
    private void Awake()
    {
        InicializarTile();
    }

    private void InicializarTile()
    {
        _humidity = new EnviromentStatus(0, 100, 0, tileFirstHumidity);
        _temperature = new EnviromentStatus(0, 40, 0, tileFirstTemperature);
        BuscarTotemCorresponente();
        BuscarTilesAdjacentes();
    }
    #endregion

    #region Métodos de Seleção de Cor
    public void ColorirTilesDuranteSeleção()
    {
        GetComponent<Renderer>().material.color = Color.green;
        TileTypeReferences tileTypeReferences = FindObjectOfType<TileTypeReferences>();

        foreach (var tile in _tilesAdjacentes)
        {
            tile.GetComponent<Renderer>().material = tileTypeReferences.GetGlowingColor(tile);
        }
    }

    public void RetornarTilesAdjacentesParaMaterialOriginal()
    {
        BiomeReferences biomeReferences = FindObjectOfType<BiomeReferences>();
        GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(Biome);

        foreach (var adjacentTile in _tilesAdjacentes)
        {
            Tile tileScript = adjacentTile.GetComponent<Tile>();
            adjacentTile.GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(tileScript.Biome);
        }
    }
    #endregion

    #region Métodos de Busca de Objetos
    private void BuscarTilesAdjacentes()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        foreach (var direction in directions)
        {
            if (Physics.Raycast(rayOrigin, direction, out RaycastHit hit, _rayDistanceTile, tileLayerMask) &&
                hit.collider.CompareTag("Tile") && !_tilesAdjacentes.Contains(hit.collider.gameObject))
            {
                _tilesAdjacentes.Add(hit.collider.gameObject);
                Debug.DrawRay(rayOrigin, direction * _rayDistanceTile, Color.green);
            }
            else
            {
                Debug.DrawRay(rayOrigin, direction * _rayDistanceTile, Color.red);
            }
        }
    }

    private void BuscarTotemCorresponente()
    {
        Vector3 rayOrigin = transform.position;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, _rayDistanceTotem, totemLayerMask) &&
            hit.collider.CompareTag("Totem"))
        {
            Totem = hit.collider.gameObject;
            Debug.DrawRay(rayOrigin, Vector3.down * _rayDistanceTotem, Color.green);
        }
        else
        {
            Debug.DrawRay(rayOrigin, Vector3.down * _rayDistanceTotem, Color.red);
        }
    }
    #endregion

    #region Métodos de Raycast
    public Piece TileRaycastForPiece()
    {
        Vector3 rayOrigin = transform.position;

        if (Physics.Raycast(rayOrigin, Vector3.up, out RaycastHit hit, _rayDistancePiece, pieceLayerMask) &&
            hit.collider.CompareTag("Piece"))
        {
            Debug.DrawRay(rayOrigin, Vector3.up * _rayDistancePiece, Color.green);
            return hit.collider.gameObject.GetComponent<Piece>();
        }

        Debug.DrawRay(rayOrigin, Vector3.up * _rayDistancePiece, Color.red);
        return null;
    }
    #endregion

    #region Métodos Estáticos
    public static void TransformarTile(Biome biome, GameObject tile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.Biome = biome;
        BiomeReferences biomeReferences = FindObjectOfType<BiomeReferences>();
        tileScript.GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(biome);
    }

    public static void AcabarDesastreEmTile(Biome biome, GameObject tile, bool desastreMaior)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.IsUnderDesastre = false;

        if (desastreMaior)
        {
            TransformarTile(biome, tile);
        }
    }
    #endregion
}
