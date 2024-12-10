using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SharedData", menuName = "ScriptableObjects/SharedData", order = 1)]
public class SharedData : ScriptableObject
{
    public List<string> dataList = new List<string>();
}
