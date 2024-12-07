using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBackground : MonoBehaviour
{
    [SerializeField] private GameObject[] _randomAssets;
    [SerializeField] private GameObject[] _randomBarriers;
    [SerializeField] private GameObject[] _randomMouths;
    [SerializeField] private GameObject[] _randomTails;
    [SerializeField] private GameObject[] _randomHand;

    [SerializeField] private Transform[] _assetsPositions;
    [SerializeField] private Transform[] _barriersPositions;
    [SerializeField] private Transform _mouth;
    [SerializeField] private Transform _tails;
    [SerializeField] private Transform _hand;

    private void Awake()
    {
        SetRandomAsset(_randomAssets, _assetsPositions);
        SetRandomAsset(_randomBarriers, _barriersPositions);
    }

    private void SetRandomAsset(GameObject[] assetsToSpawn, Transform[] positionsToSpawn)
    {
        foreach (Transform t in positionsToSpawn)
        {
            int randomNumber = Random.Range(0, assetsToSpawn.Length-1);
            Instantiate(assetsToSpawn[randomNumber], t.position, t.rotation);
        }
    }
    private void SetRandomAsset(GameObject[] assetsToSpawn, Transform positionToSpawn)
    {
            int randomNumber = Random.Range(0, assetsToSpawn.Length - 1);
            Instantiate(assetsToSpawn[randomNumber], positionToSpawn.position, positionToSpawn.rotation);
    }
}
