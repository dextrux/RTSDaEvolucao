using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Observer : MonoBehaviour 
{
    [SerializeField]
    public List<Owner> Owners { get; set; }
}

