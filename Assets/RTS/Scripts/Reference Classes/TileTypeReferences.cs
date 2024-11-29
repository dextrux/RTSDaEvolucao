using UnityEngine;
public class TileTypeReferences : MonoBehaviour
{
    [SerializeField]
    public Material[] tileTypeColor = new Material[System.Enum.GetValues(typeof(TileType)).Length];
    public Material GetColor(GameObject tile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        TileType tileType = tileScript.TileType;
        Owner tileOwner = tileScript.Owner;
        OwnerReference ownerReference = GameObject.FindObjectOfType<OwnerReference>();
        return tileType switch
        {
            TileType.Posicionamento => ownerReference.GetColor(tileOwner),
            TileType.Comida => tileTypeColor[1],
            TileType.Mutagêncio => tileTypeColor[2],
            TileType.Barreira => tileTypeColor[3],
            _ =>
            null
        };
    }

    public void SetOwnerColors(TileType tileType, Material referenceColor)
    {
        tileTypeColor[(int)tileType] = referenceColor;
    }

    [SerializeField]
    public Material[] tileTypeGlowingColor = new Material[System.Enum.GetValues(typeof(TileType)).Length];
    public Material GetGlowingColor(GameObject tile)
    {
        Tile tileScript = tile.GetComponent<Tile>();
        TileType tileType = tileScript.TileType;
        Owner tileOwner = tileScript.Owner;
        OwnerReference ownerReference = GameObject.FindObjectOfType<OwnerReference>();
        return tileType switch
        {
            TileType.Posicionamento => ownerReference.GetGlowingColor(tileOwner),
            TileType.Comida => tileTypeGlowingColor[1],
            TileType.Mutagêncio => tileTypeGlowingColor[2],
            TileType.Barreira => tileTypeGlowingColor[3],
            _ =>
            null
        };
    }

    public void SetOwnerGlowingColors(TileType tileType, Material referenceColor)
    {
        tileTypeGlowingColor[(int)tileType] = referenceColor;
    }

}