using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTotem : MonoBehaviour
{
    private MeatFood meatFood = new MeatFood();
    private NonMeatFood nonMeatFood = new NonMeatFood();
    public Renderer renderer;
    public List<Mesh> totemMesh;
    public CurrentTotemType totemType;

    private void Start()
    {
        SetTotemAsInactive();
    }
    public MeatFood GetMeatFood() { return this.meatFood; }
    public NonMeatFood GetNonMeatFood() { return this.nonMeatFood; }
    public CurrentTotemType GetCurrentTotemType() { return this.totemType; }
    public void SetCurrentTotemType(CurrentTotemType totemType) { this.totemType = totemType; }

    public void SetTotemAsActive(CurrentTotemType type)
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (type == CurrentTotemType.NonMeatFood) { renderer.material.color = Color.cyan; meshFilter.mesh = totemMesh[0]; }
        else if(type == CurrentTotemType.MeatFood) { renderer.material.color = Color.red; meshFilter.mesh = totemMesh[1]; }
        this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        this.gameObject.SetActive(true);
    }

    public void SetTotemAsInactive()
    {
        this.gameObject.SetActive(false);
        this.SetCurrentTotemType(CurrentTotemType.None);
        this.transform.position = new Vector3(this.transform.position.x, -1, this.transform.position.z);
    }
}
