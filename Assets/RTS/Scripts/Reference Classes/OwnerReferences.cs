using UnityEngine;

public class OwnerReference : MonoBehaviour
{
    [SerializeField]
    public Material[] ownerColor = new Material[System.Enum.GetValues(typeof(Owner)).Length];
    public Material GetColor(Owner owner)
    {
        return owner switch
        {
            Owner.P1 => ownerColor[0],
            Owner.P2 => ownerColor[1],
            Owner.P3 => ownerColor[2],
            Owner.P4 => ownerColor[3],
            Owner.P5 => ownerColor[4],
            _ =>
            null
        };
    }

    public void SetOwnerColors(Owner owner, Material referenceMaterial)
    {
        ownerColor[(int)owner] = referenceMaterial;
    }

    [SerializeField]
    public Material[] ownerGlowingColor = new Material[System.Enum.GetValues(typeof(Owner)).Length];
    public Material GetGlowingColor(Owner owner)
    {
        return owner switch
        {
            Owner.P1 => ownerGlowingColor[0],
            Owner.P2 => ownerGlowingColor[1],
            Owner.P3 => ownerGlowingColor[2],
            Owner.P4 => ownerGlowingColor[3],
            Owner.P5 => ownerGlowingColor[4],
            Owner.None => ownerGlowingColor[5],
            _ =>
            null
        };
    }

    public void SetOwnerGlowingColors(Owner owner, Material referenceMaterial)
    {
        ownerGlowingColor[(int)owner] = referenceMaterial;
    }
}
