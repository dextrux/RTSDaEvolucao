using System.Drawing;
using UnityEngine;

public enum Owner
{
    
    P1 = 1, 
    P2 = 2, 
    P3 = 3, 
    P4 = 4, 
    NPC = 5,
    None = 6
}
public static class OwnerColors
{
    public static UnityEngine.Color GetColor(Owner owner)
    {
        return owner switch
        {
            Owner.P1 => new UnityEngine.Color(0.8f, 1f, 0.898f),
            Owner.P2 => new UnityEngine.Color(0.4f, 0.4f, 1f),
            Owner.P3 => new UnityEngine.Color(1f, 0.647f, 0f),
            Owner.P4 => new UnityEngine.Color(0.294f, 0f, 0.51f),
            Owner.NPC => UnityEngine.Color.red,
            _ => UnityEngine.Color.white
        };
    }
}
