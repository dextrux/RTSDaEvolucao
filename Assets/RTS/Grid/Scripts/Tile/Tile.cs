using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Atributos
    public TileType _tileType;
    public Biome _biome;
    public Humidity _humidity = new Humidity();
    public Temperature _temperature = new Temperature();

    // Atributos de inspetor para assinalação
    public float currentHumidity;
    public float currentTemperature;
    public List<GameObject> tilesAdjacentes;
    public List <GameObject> foodTotens;

    // Inicialização dos valores
    public void Start()
    {
        _humidity.CreateHumidity(0, currentHumidity);
        _temperature.CreateTemperature(0, currentTemperature);
    }

    // Getters
    public Biome GetBiome() { return _biome; }
    public TileType GetTileType() { return _tileType; }
    public List<GameObject> GetTilesAdjacentes() { return tilesAdjacentes; }
    public List<GameObject> GetFoodTotem() {  return foodTotens; }
    public Humidity GetHumidity() { return this._humidity; }

    public Temperature GetTemperature() { return this._temperature; }

    // Getters as strings

    // Setters
    public void SetBiome(Biome biome) { _biome = biome; }
    public void SetTileType(TileType tileType) { _tileType = tileType; }
}
