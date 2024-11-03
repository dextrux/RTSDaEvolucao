using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Atributos
    private const float _rayDistanceTile = 3;
    private const float _rayDistanceTotem = 1;
    private const float _rayDistancePiece = 5f;
    [SerializeField]
    private Humidity _humidity;
    [SerializeField]
    private Temperature _temperature;
    [SerializeField]
    private List<GameObject> _tilesAdjacentes = new List<GameObject>();
    [SerializeField]
    private GameObject _totem;
    private bool _isUnderDisastre;
    [SerializeField]
    private Owner _owner;

    // Atributos de inspetor para assinalação
    public Material[] materialOriginal = new Material[5];
    public float tileFirstHumidity;
    public float tileFirstTemperature;
    public TileType tileType;
    public Biome biome;
    public LayerMask tileLayerMask;
    public LayerMask totemLayerMask;
    public LayerMask pieceLayerMask;


    // Atributo para definir o ângulo dos raios no inspetor
    private Vector3[] directions = {
        Vector3.right,
        Quaternion.Euler(0, 60 * 1, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 2, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 3, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 4, 0) * Vector3.right,
        Quaternion.Euler(0, 60 * 5, 0) * Vector3.right
    };

    // Propriedades
    public Owner Owner { get { return _owner; } set { _owner = value; } }
    public TileType TileType { get { return tileType; } set { tileType = value; } }
    public Biome Biome { get { return biome; } set { biome = value; } }
    public Humidity Humidity { get { return _humidity; } }
    public Temperature Temperature { get { return _temperature; } }
    public List<GameObject> TilesAdjacentes { get { return _tilesAdjacentes; } }
    public GameObject Totem {  get { return _totem; }  set { _totem = value; } }
    public bool IsUnderDesastre { get { return _isUnderDisastre; } set { _isUnderDisastre = value; } }

    // Métodos
    private void Awake()
    {
        InicializarTile();
    }

    public GameObject prefab;
    private void Start()
    {
        if(Biome == Biome.Caatinga)
        {
            Vector3 instatiatePosition = new Vector3(this.transform.position.x, 5f, this.transform.position.z);
            Instantiate(prefab, instatiatePosition, Quaternion.identity);
        }
    }

    private void InicializarTile()
    {
        _humidity = new Humidity(tileFirstHumidity);
        _temperature = new Temperature(tileFirstTemperature);
        BuscarTotemCorresponente();
        BuscarTilesAdjacentes();
    }
    
    public void ColorirTilesDuranteSeleção()
    {
        this.GetComponent<Renderer>().material.color = Color.green;
        for (int i = 0; i < _tilesAdjacentes.Count; i++)
        {
            TileType type = _tilesAdjacentes[i].GetComponent<Tile>().TileType;
            Owner owner = _tilesAdjacentes[i].GetComponent<Tile>().Owner;
            if (owner == Owner.None)
            {
                switch (type)
                {
                    case TileType.Posicionamento:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.blue;
                        break;
                    case TileType.Comida:
                        FoodSize tileAdjacenteFoodSize = _tilesAdjacentes[i].GetComponent<Tile>().Totem.GetComponent<FoodTotem>().FoodSize;
                        switch ((int)tileAdjacenteFoodSize)
                        {
                            case <=3:
                                _tilesAdjacentes[i].GetComponent<Renderer>().material.color = new Color(145,35,35);
                                break;
                            case > 3:
                                _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.yellow;
                                break;
                            default: 
                                Debug.Log("Exceção encontrada no colorimento dos tiles adjacentes do tipo comida");
                                break;
                        }
                        break;
                    case TileType.Barreira:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.black;
                        break;
                    default:
                        Debug.Log("Exceção encontrada no colorimento dos tiles adjacentes");
                        break;
                }
            }
            else
            {
                switch (owner)
                {
                    case Owner.NPC:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.red;
                        break;
                    case Owner.P1:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = new Color(0.8f, 1f, 0.898f); // Verde Água
                        break;
                    case Owner.P2:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = new Color(0.4f, 0.4f, 1f); // Azul Claro
                        break;
                    case Owner.P3:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = new Color(1f, 0.647f, 0f); // Laranja
                        break;
                    case Owner.P4:
                        _tilesAdjacentes[i].GetComponent<Renderer>().material.color = new Color(0.294f, 0f, 0.51f); // Roxo
                        break;
                    default:
                        Debug.Log("Exceção encontrada no colorimento dos tiles adjacentes com dono");
                        break;

                }
            }

        }
    }

    public void RetornarTilesAdjacentesParaMaterialOriginal()
    {
        // Verifica se o componente Tile está presente antes de acessá-lo
        Tile currentTile = this.GetComponent<Tile>();
        if (currentTile != null && currentTile.materialOriginal != null)
        {
            this.GetComponent<Renderer>().material = currentTile.materialOriginal[(int)currentTile.Biome];
        }

        // Aplica o material original aos tiles adjacentes
        for (int i = 0; i < _tilesAdjacentes.Count; i++)
        {
            Tile adjacentTile = _tilesAdjacentes[i].GetComponent<Tile>();
            if (adjacentTile != null && adjacentTile.materialOriginal != null)
            {
                _tilesAdjacentes[i].GetComponent<Renderer>().material = adjacentTile.materialOriginal[(int)adjacentTile.Biome];
            }
        }
    }

    private void BuscarTilesAdjacentes()
    {
        Vector3 rayOriginTransform = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f, this.transform.position.z);

        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(rayOriginTransform, directions[i], out RaycastHit hit, _rayDistanceTile, tileLayerMask))
            {
                Debug.DrawRay(rayOriginTransform, directions[i] * _rayDistanceTile, Color.green);
                if(hit.collider.gameObject.tag == "Tile")
                {
                    if (!_tilesAdjacentes.Contains(hit.collider.gameObject))
                    {
                        _tilesAdjacentes.Add(hit.collider.gameObject); // Adiciona o tile adjacente detectado
                    }
                }
            }
            else
            {
                Debug.DrawRay(rayOriginTransform, directions[i] * _rayDistanceTile, Color.red);
            }
        }
    }

    private void BuscarTotemCorresponente() 
    { 
        Vector3 rayOriginTransform = this.transform.position;
        if (Physics.Raycast(rayOriginTransform, Vector3.down, out RaycastHit hit, _rayDistanceTotem, totemLayerMask))
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTotem, Color.green);
            if (hit.collider.gameObject.tag == "Totem")
            {
                if (_totem != (hit.collider.gameObject))
                {
                    Totem = (hit.collider.gameObject); // Adiciona o totem correspondente detectado
                }
            }
        }
        else
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTotem, Color.red);
        }
    }
    public Piece TileRaycastForPiece()
    {
        Vector3 rayOriginTransform = this.transform.position;
        if (Physics.Raycast(rayOriginTransform, Vector3.up, out RaycastHit hit, _rayDistancePiece, pieceLayerMask))
        {
            Debug.DrawRay(rayOriginTransform, Vector3.up * _rayDistancePiece, Color.green);
            if (hit.collider.gameObject.tag == "Piece")
            {
                return hit.collider.gameObject.GetComponent<Piece>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.DrawRay(rayOriginTransform, Vector3.up * _rayDistancePiece, Color.red);
            return null;
        }
    }

    public void TransformarTile(Biome biome)
    {
        Biome = biome;
        this.GetComponent<Renderer>().material = this.GetComponent<Tile>().materialOriginal[(int)Biome];
    }

    public void AcabarDesastreEmTile(Biome biome)
    {
        TransformarTile(biome);
        _isUnderDisastre = false;
    }
}
