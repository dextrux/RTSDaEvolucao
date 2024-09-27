using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MapInstantiation : MonoBehaviour
{
    public GameObject[] mapTiles;
    private IEnumerator coroutine;
    public GameObject _mapEmpty;
    public GameObject newObject;
    private void Start()
    {
        coroutine = WaitSeconds(5);
        StartCoroutine(coroutine);
        InstantiateMap();
    }
    public void InstantiateMap()
    {
        for (int i = 0; i < mapTiles.Length; i++) 
        {
            newObject = Instantiate(mapTiles[i], _mapEmpty.transform.position, this.transform.rotation);
            newObject.transform.parent = _mapEmpty.transform;
            StartCoroutine(coroutine);
        }
    }

    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
