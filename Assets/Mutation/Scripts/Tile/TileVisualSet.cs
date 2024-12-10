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
        if (_activeObjectTiles.Length > 0) DespawnObjects();
        if (tile.tileType == TileType.Barreira)
        {
            GameObject newObj = Instantiate(TileVisualSingleton.Instance.ObjetoParaInstanciar(tile.biome, tile.tileType), _spawnPoints[0].position, Quaternion.Euler(0, 0, 0));
            _activeObjectTiles[0] = newObj;
        }
        else
        {
            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                GameObject newObj = Instantiate(TileVisualSingleton.Instance.ObjetoParaInstanciar(tile.biome, tile.tileType), _spawnPoints[i].position, Quaternion.Euler(transform.rotation.x, Random.Range(0, 360), transform.rotation.z));
                _activeObjectTiles[i] = newObj;
            }
            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                _activeObjectTiles[i].transform.localScale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f));
            }
        }
    }
    private void DespawnObjects()
    {
        for (int i = 0; i < _activeObjectTiles.Length; i++)
        {
            Destroy(_activeObjectTiles[i]);
        }
    }
}
