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
    public GameObject mesh;
    public List<GameObject> tilesAdjacentes;
    public GameObject foodTotens;
    public List<GameObject> tileMesh;

    // Inicialização dos valores
    public void Start()
    {
        _humidity.CreateHumidity(0, currentHumidity);
        _temperature.CreateTemperature(0, currentTemperature);
        Coloring();
        Round();
    }

    private void Round()
    {
        System.Random random = new System.Random();
        int seed = random.Next(0, 100);
        if (seed > 80 && seed % 2 == 0 && _tileType == TileType.Vazio || _tileType == TileType.Comida)
        {
            FoodTotem totem;
            CurrentTotemType type;
            type = (CurrentTotemType)random.Next(1, (int)CurrentTotemType.NonMeatFood + 1);
            totem = foodTotens.GetComponent<FoodTotem>();
            totem.SetCurrentTotemType(type);
            totem.SetTotemAsActive(type);
            _tileType = TileType.Comida;
        }
    }

    // This method is called when the GameObject becomes visible to any camera
    private void OnBecameVisible()
    {
        // Optional: Reactivate if it becomes visible again
        gameObject.SetActive(true);
    }

    // This method is called when the GameObject is no longer visible by any camera
    private void OnBecameInvisible()
    {
        // Deactivate the GameObject when it's not visible
        gameObject.SetActive(false);
    }

    private void Coloring()
    {
        if (this._biome == Biome.Caatinga)
        {
            mesh.GetComponent<Renderer>().material.color = Color.yellow;

        }
        if (this._biome == Biome.Mata_Atlantica)
        {
            mesh.GetComponent<Renderer>().material.color = Color.magenta;
        }
        if (this._biome == Biome.Mata_das_Araucarias)
        {
            mesh.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (this._biome == Biome.Pantanal)
        {
            mesh.GetComponent<Renderer>().material.color = Color.black;
        }
        if (this._biome == Biome.Pampa)
        {
            mesh.GetComponent<Renderer>().material.color = Color.green;
        }
        if (this._tileType == TileType.Barreira)
        {
            mesh.GetComponent<Renderer>().material.color = Color.red;
        }


    }

    // Getters
    public Biome GetBiome() { return _biome; }
    public TileType GetTileType() { return _tileType; }
    public List<GameObject> GetTilesAdjacentes() { return tilesAdjacentes; }
    public GameObject GetFoodTotem() { return foodTotens; }
    public Humidity GetHumidity() { return this._humidity; }

    public Temperature GetTemperature() { return this._temperature; }

    // Getters as strings

    // Setters

    public void AddTilesAdjacentes(GameObject tile) { tilesAdjacentes.Add(tile); }
    public void SetBiome(Biome biome) { _biome = biome; }
    public void SetTileType(TileType tileType) { _tileType = tileType; }
}
