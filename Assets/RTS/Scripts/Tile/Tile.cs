using System;
using System.Collections;
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
    [SerializeField] private TileVisualSet _tileVisualSet;

    // Atributos de inspetor para assinala��o
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

    #region M�todos de Inicializa��o
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
    private void Start()
    {
        _tileVisualSet.SpawnObjectsTile(gameObject.GetComponent<Tile>());
    }
    #endregion

    #region M�todos de Sele��o de Cor
    public static void ColorirTilesDuranteSele��o(Tile tile)
    {
        TileTypeReferences tileTypeReferences = FindObjectOfType<TileTypeReferences>();
        tile.StartCoroutine(tile.ColorirTilesGradualmente(tileTypeReferences));
    }

    private IEnumerator ColorirTilesGradualmente(TileTypeReferences tileTypeReferences)
    {
        try
        {
            foreach (var tile in _tilesAdjacentes)
            {
                tile.GetComponent<Renderer>().material = tileTypeReferences.GetGlowingColor(tile);
                yield return new WaitForSeconds(0.1f); // Espera 0.1 segundo antes de continuar
            }
        }
        finally
        {
            // Chama o m�todo para retornar os materiais originais
            RetornarTilesAdjacentesParaMaterialOriginal(this);
        }
    }

    public static void RetornarTilesAdjacentesParaMaterialOriginal(Tile tile)
    {
        BiomeReferences biomeReferences = FindObjectOfType<BiomeReferences>();
        foreach (var adjTile in tile._tilesAdjacentes)
        {
            adjTile.GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(adjTile.GetComponent<Tile>().Biome);
        }
    }

    public void RetornarTilesParaMaterialOriginal()
    {
        BiomeReferences biomeReferences = FindObjectOfType<BiomeReferences>();
        foreach (var adjacentTile in _tilesAdjacentes)
        {
            Tile tileScript = adjacentTile.GetComponent<Tile>();
            adjacentTile.GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(tileScript.Biome);

        }
    }
    private IEnumerator RetornarTilesGradualmente(BiomeReferences biomeReferences)
    {
        foreach (var adjacentTile in _tilesAdjacentes)
        {
            Tile tileScript = adjacentTile.GetComponent<Tile>();
            adjacentTile.GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(tileScript.Biome);
            //yield return new WaitForSeconds(0.1f); // Espera 0.1 segundo antes de continuar
            yield return null;
        }
    }

    #endregion

    #region M�todos de Busca de Objetos
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

    #region M�todos de Raycast
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

    #region M�todos Est�ticos
    public void TransformarTile(Biome biome, GameObject tile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.Biome = biome;
        BiomeReferences biomeReferences = FindObjectOfType<BiomeReferences>();
        tileScript.GetComponent<Renderer>().material = biomeReferences.GetBiomeMaterial(biome);
        _tileVisualSet.SpawnObjectsTile(gameObject.GetComponent<Tile>());
    }

    public static void AcabarDesastreEmTile(Biome biome, GameObject tile, bool desastreMaior)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        tileScript.IsUnderDesastre = false;

        if (desastreMaior)
        {
            tileScript.TransformarTile(biome, tile);
        }
    }

    //public static void TransitionTileTextureBigDisaster(float progress, Biome antes, Biome depois, List<GameObject> tiles)
    //{
    //    // Obt�m as refer�ncias aos materiais
    //    BiomeReferences biomeRefs = GameObject.FindAnyObjectByType<BiomeReferences>();
    //    if (biomeRefs == null)
    //    {
    //        Debug.LogError("BiomeReferences n�o encontrado.");
    //        return;
    //    }

    //    Material oldMat = biomeRefs.GetBiomeMaterial(antes);
    //    Material newMat = biomeRefs.GetBiomeMaterial(depois);

    //    if (oldMat == null || newMat == null)
    //    {
    //        Debug.LogError("Materiais n�o encontrados para os biomas fornecidos.");
    //        return;
    //    }

    //    // Cria a transi��o manipulando as propriedades do material
    //    foreach (GameObject tile in tiles)
    //    {
    //        Renderer renderer = tile.GetComponent<Renderer>();
    //        if (renderer == null)
    //        {
    //            Debug.LogWarning($"Tile {tile.name} n�o possui um Renderer.");
    //            continue;
    //        }

    //        // Cria um novo material baseado na mistura dos materiais oldMat e newMat
    //        Material blendedMaterial = new Material(renderer.sharedMaterial);
    //        blendedMaterial.Lerp(oldMat, newMat, progress);

    //        // Atribui o material ao tile
    //        renderer.material = blendedMaterial;
    //    }
    //}

    #endregion
}
