using UnityEngine;

public class BiomeReferences : MonoBehaviour
{
    [SerializeField]
    private  Material[] biomeMaterials = new Material[System.Enum.GetValues(typeof(Biome)).Length];
    public  Material GetBiomeMaterial(Biome biome)
    {
        return biome switch
        {
            Biome.Mata_das_Araucarias => biomeMaterials[0],
            Biome.Mata_Atlantica => biomeMaterials[1],
            Biome.Caatinga => biomeMaterials[2],
            Biome.Pampa => biomeMaterials[3],
            Biome.Pantanal => biomeMaterials[4],
            _ =>
            null
        };
    }

    public  void SetBiomeMaterials(Biome biome, Material referenceMaterial)
    {
        biomeMaterials[(int)biome] = referenceMaterial;
    }
}
