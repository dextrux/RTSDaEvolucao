using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Atributos
    private const float _rayDistanceTile = 3;
    private const float _rayDistanceTotem = 1;
    [SerializeField]
    private Humidity _humidity;
    [SerializeField]
    private Temperature _temperature;
    [SerializeField]
    private List<GameObject> _tilesAdjacentes = new List<GameObject>();
    [SerializeField]
    private GameObject _totem;
    private bool _isUnderDisastre;

    // Atributos de inspetor para assinalação
    public Material[] materialOriginal = new Material[5];
    public float tileFirstHumidity;
    public float tileFirstTemperature;
    public TileType tileType;
    public Biome biome;
    public LayerMask tileLayerMask;
    public LayerMask totemLayerMask;


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

    private void InicializarTile()
    {
        _humidity = new Humidity(tileFirstHumidity);
        _temperature = new Temperature(tileFirstTemperature);
        BuscarTotemCorresponente();
        BuscarTilesAdjacentes();
    }
    
    public void ColorirTilesAdjacentes()
    {
        for (int i = 0; i < _tilesAdjacentes.Count; i++)
        {
            TileType type = _tilesAdjacentes[i].GetComponent<Tile>().TileType;
            switch (type)
            {
                case TileType.Vazio:
                    _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case TileType.Comida:
                    _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                case TileType.NPC:
                    _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.gray;
                    break;
                case TileType.P1:
                    _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.green;
                    break;
                case TileType.Barreira:
                    _tilesAdjacentes[i].GetComponent<Renderer>().material.color = Color.red;
                    break;
                default: Debug.Log("Exceção encontrada");
                    break;
            }          
        }
    }

    public void RetornarTilesAdjacentesParaMaterialOriginal()
    {
        for (int i = 0; i < _tilesAdjacentes.Count; i++)
        {          
            _tilesAdjacentes[i].GetComponent<Renderer>().material = _tilesAdjacentes[i].GetComponent<Tile>().materialOriginal[(int)Biome];
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
