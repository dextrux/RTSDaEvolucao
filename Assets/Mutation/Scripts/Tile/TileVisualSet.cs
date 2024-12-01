using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualSet : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _activeObjectTiles;
    private void Awake()
    {
        _activeObjectTiles = new GameObject[_spawnPoints.Length];
    }
    public void SpawnObjectsTile(Tile tile)
    {
        DespawnObjects();
        for (int i = 0;  i < _spawnPoints.Length; i++)
        {
            GameObject newObj = Instantiate(TileVisualSingleton.Instance.ObjetoParaInstanciar(tile.biome), _spawnPoints[i].position, transform.rotation);
        }
    }
    private void DespawnObjects()
    {
        for (int i = 0; i < _activeObjectTiles.Length; i++)
        {
            _activeObjectTiles[i] = null;
            Destroy(_activeObjectTiles[i]);
        }
    }
}
