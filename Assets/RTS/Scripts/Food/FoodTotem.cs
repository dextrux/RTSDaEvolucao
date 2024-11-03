using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FoodTotem : MonoBehaviour
{
    // Atributos
    private Renderer _renderer;
    private float _foodQuantity;
    private FoodSize _foodSize;
    private const float _rayDistanceTile = 5f;
    // Atributos de inspetor para assinalação
    public Material[] totemMaterial = new Material[6];
    public Mesh[] totemMesh = new Mesh[6];
    public LayerMask tileLayerMask;

    // Propriedades
    public FoodSize FoodSize { get { return _foodSize; } set { _foodSize = value; } }
    public float FoodQuantity { get { return _foodQuantity; } set { if (value >= 100) { _foodQuantity = 100; } else { _foodQuantity = value; } } }
    public float MultiplyFoodQuantityByFactor { set { FoodQuantity = FoodQuantity * value; } }

    // Ações do Totem
    public void SetTotemAsActive(FoodSize size)
    {
        Debug.Log("Totem ativo");
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        // Transformação dos atributos
        this.FoodSize = size;
        // Transformaação da forma e material do objeto
        meshFilter.mesh = totemMesh[(int)size];
        this._renderer.material = totemMaterial[(int)size];
        // Posicionamento do objeto
        this.transform.position = new Vector3(this.transform.position.x, 5, this.transform.position.z);
        this.gameObject.SetActive(true);
        TotemRaycastToTile().TileType = TileType.Comida;
        FoodQuantity = 100; 
    }

    private Tile TotemRaycastToTile()
    {
        Vector3 rayOriginTransform = this.transform.position;
        if (Physics.Raycast(rayOriginTransform, Vector3.down, out RaycastHit hit, _rayDistanceTile, tileLayerMask))
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTile, Color.green);
            if (hit.collider.gameObject.tag == "Tile")
            {
                return hit.collider.gameObject.GetComponent<Tile>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTile, Color.red);
            return null;
        }
    }

    public void SetTotemAsInactive()
    {
        Debug.Log("Totem inativo");
        this.gameObject.SetActive(false);
        this.FoodSize = FoodSize.None;
        this.transform.position = new Vector3(this.transform.position.x, -1, this.transform.position.z);
    }

    private void Start()
    {
        this._renderer = this.GetComponent<Renderer>();
        SetTotemAsInactive();
    }
}