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
    public float idealHumidity;
    public float currentHumidity;
    public float idealTemperature;
    public float currentTemperature;
    public TileType firstTileType;
    public Biome firstBiome;
    public List<GameObject> tilesAdjacentes;

    // Inicialização dos valores
    public void Awake()
    {
        _humidity.CreateHumidity(idealHumidity, currentHumidity);
        _temperature.CreateTemperature(idealTemperature, currentTemperature);
        SetBiome(firstBiome);
        SetTileType(firstTileType);
    }

    // Getters
    public Biome GetBiome() { return _biome; }
    public TileType GetTileType() { return _tileType; }
    public Humidity GetHumidity() { return _humidity; }
    public float GetIdealHumidityValue() { return _humidity.GetIdealHumidityValue(); }
    public float GetCurrentHumidityValue() { return _humidity.GetCurrentHumidityValue(); }
    public float GetLastHumidityValue() { return _humidity.GetLastHumidityValue(); }
    public Temperature GetTemperature() { return _temperature; }
    public float GetIdealTemperatureValue() { return _temperature.GetIdealTemperatureValue(); }
    public float GetCurrentTemperatureValue() { return _temperature.GetCurrentTemperatureValue(); }
    public float GetLastTemperatureValue() { return _temperature.GetLastTemperatureValue(); }
    public List<GameObject> GetTilesAdjacentes() { return tilesAdjacentes; }

    // Getters as strings
    public string GetBiomeAsString() { return _biome.ToString(); }
    public string GetTileTypeAsString() { return _tileType.ToString(); }
    public string GetIdealHumidityAsString() { return _humidity.GetIdealHumidityAsString(); }
    public string GetCurrentHumidityAsString() { return _humidity.GetCurrentHumidityAsString(); }
    public string GetLastHumidityAsString() { return _humidity.GetLastHumidityAsString(); }
    public string GetIdealTemperatureAsString() { return _temperature.GetIdealTemperatureAsString(); }
    public string GetCurrentTemperatureAsString() { return _temperature.GetCurrentTemperatureAsString(); }
    public string GetLastTemperatureAsString() { return _temperature.GetLastTemperatureAsString(); }

    // Setters
    public void SetBiome(Biome biome)
    {
        _biome = biome;
    }

    public void SetTileType(TileType tileType)
    {
        _tileType = tileType;
    }

    // Event's Set's
    public void SetNewTemperatureValuesByFactor(float factor)
    {
        _temperature.SetNewValueToAllTemperatureAtributesByFactor(factor);
    }

    public void SetNewHumidityValuesByFactor(float factor)
    {
        _humidity.SetNewValueToAllHumidityAtributesByFactor(factor);
    }
}
