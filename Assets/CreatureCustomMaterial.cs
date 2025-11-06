using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCustomMaterial : MonoBehaviour
{
    [SerializeField]private Mesh _meshToChange;
    private bool _materialChange;

    public bool HasBasicMaterial(Material playerMaterial)
    {
        if (_materialChange)
        {
            //_meshToChange.
            return true;
        }
        return false;
    }
}
