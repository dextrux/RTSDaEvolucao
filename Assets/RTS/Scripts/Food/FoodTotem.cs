using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FoodTotem : MonoBehaviour
{
    // Atributos
    private Renderer _renderer;
    [SerializeField]
    private MeatFood _meatFood = new MeatFood();
    [SerializeField]
    private NonMeatFood _nonMeatFood = new NonMeatFood();
    private FoodSize _foodSize;
    // Atributos de inspetor para assinalação
    public Material[] totemMaterial = new Material[6];
    public Mesh[] totemMesh = new Mesh[6];

    // Propriedades
    public FoodSize FoodSize { get { return _foodSize; } set { _foodSize = value; } }
    public MeatFood MeatFood { get { return _meatFood; } }
    public NonMeatFood NonMeatFood { get { return _nonMeatFood; } }

    // Ações do Totem
    public void SetTotemAsActive(FoodSize size)
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        // Transformação dos atributos
        this.FoodSize = size;
        // Transformaação da forma e material do objeto
        meshFilter.mesh = totemMesh[(int)size];
        this._renderer.material = totemMaterial[(int)size];
        // Posicionamento do objeto
        this.transform.position = new Vector3(this.transform.position.x, 3, this.transform.position.z);
        this.gameObject.SetActive(true);
    }

    public void SetTotemAsInactive()
    {
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
