using UnityEngine;
public class TotemTypeReferences : MonoBehaviour
{
    // Material
    [SerializeField]
    private  Material[] totemTypeMaterials = new Material[System.Enum.GetValues(typeof(TotemType)).Length];
    public  Material GetTotemTypeMaterial(TotemType totemType)
    {
        return totemType switch
        {
            TotemType.P => totemTypeMaterials[0],
            TotemType.M => totemTypeMaterials[1],
            TotemType.G => totemTypeMaterials[2],
            TotemType.Frutas => totemTypeMaterials[3],
            TotemType.Grãos => totemTypeMaterials[4],
            TotemType.Plantas => totemTypeMaterials[5],
            TotemType.Ponto_Mutagenico => totemTypeMaterials[6],
            TotemType.Corpo => totemTypeMaterials[7],
            _ =>
            null
        };
    }

    public  void SetTotemMaterials(TotemType totemType, Material referenceMaterial)
    {
        totemTypeMaterials[(int)totemType] = referenceMaterial;
    }
    // Mesh
    [SerializeField]
    private  Mesh[] totemTypeMesh = new Mesh[System.Enum.GetValues(typeof(TotemType)).Length];
    public  Mesh GetTotemTypeMesh(TotemType totemType)
    {
        return totemType switch
        {
            TotemType.P => totemTypeMesh[0],
            TotemType.M => totemTypeMesh[1],
            TotemType.G => totemTypeMesh[2],
            TotemType.Frutas => totemTypeMesh[3],
            TotemType.Grãos => totemTypeMesh[4],
            TotemType.Plantas => totemTypeMesh[5],
            TotemType.Ponto_Mutagenico => totemTypeMesh[6],
            TotemType.Corpo => totemTypeMesh[7],
            _ =>
            null
        };
    }

    public  void SetTotemTypeMesh(TotemType totemType, Mesh totemTypeMeshReference)
    {
        totemTypeMesh[(int)totemType] = totemTypeMeshReference;
    }
}