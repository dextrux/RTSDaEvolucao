using UnityEngine;
public class TileTypeReferences : MonoBehaviour
{
    [SerializeField]
    public  UnityEngine.Color[] tileTypeColor = new UnityEngine.Color[System.Enum.GetValues(typeof(TileType)).Length];
    public  UnityEngine.Color GetColor(GameObject tile)
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
            UnityEngine.Color.black
        };
    }

    public  void SetOwnerColors(TileType tileType, Color referenceColor)
    {
        tileTypeColor[(int)tileType] = referenceColor;
    }
}