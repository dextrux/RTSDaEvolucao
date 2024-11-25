using UnityEngine;

public class OwnerReference : MonoBehaviour
{
    [SerializeField]
    public  UnityEngine.Color[] ownerColor = new UnityEngine.Color[System.Enum.GetValues(typeof(Owner)).Length];
    public  UnityEngine.Color GetColor(Owner owner)
    {
        return owner switch
        {
            Owner.P1 => ownerColor[0],
            Owner.P2 => ownerColor[1],
            Owner.P3 => ownerColor[2],
            Owner.P4 => ownerColor[3],
            Owner.P5 => ownerColor[4],
            _ =>
            UnityEngine.Color.black
        };
    }

    public  void SetOwnerColors(Owner owner, Material referenceMaterial)
    {
        UnityEngine.Color color = referenceMaterial.color;
        ownerColor[(int)owner] = color;
    }
}
