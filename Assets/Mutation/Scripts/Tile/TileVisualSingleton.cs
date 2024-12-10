using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileVisualSingleton : MonoBehaviour
{
    public static TileVisualSingleton Instance;
    [SerializeField] private GameObject[] prefabsAraucaria;
    [SerializeField] private GameObject[] prefabsAtlantica;
    [SerializeField] private GameObject[] prefabsCaatinga;
    [SerializeField] private GameObject[] prefabsPampa;
    [SerializeField] private GameObject[] prefabsPantanal;
    [SerializeField] private GameObject[] prefabsBarreiras;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public GameObject ObjetoParaInstanciar(Biome tileBiome, TileType type)
    {
        GameObject instancivel = new GameObject();
        if (type != TileType.Barreira)
        {
            switch (tileBiome)
            {
                case Biome.Mata_das_Araucarias:
                    if (prefabsAraucaria.Length > 0) instancivel = prefabsAraucaria[Random.Range(0, prefabsAraucaria.Length - 1)];
                    break;
                case Biome.Mata_Atlantica:
                    if (prefabsAtlantica.Length > 0) instancivel = prefabsAtlantica[Random.Range(0, prefabsAtlantica.Length - 1)];
                    break;
                case Biome.Caatinga:
                    if (prefabsCaatinga.Length > 0) instancivel = prefabsCaatinga[Random.Range(0, prefabsCaatinga.Length - 1)];
                    break;
                case Biome.Pampa:
                    if (prefabsPampa.Length > 0) instancivel = prefabsPampa[Random.Range(0, prefabsPampa.Length - 1)];
                    break;
                case Biome.Pantanal:
                    if (prefabsPantanal.Length > 0) instancivel = prefabsPantanal[Random.Range(0, prefabsPantanal.Length - 1)];
                    break;
            }
        }
        else
        {
            switch (tileBiome)
            {
                case Biome.Mata_das_Araucarias:
                    if (prefabsAraucaria.Length > 0) instancivel = prefabsBarreiras[0];
                    break;
                case Biome.Mata_Atlantica:
                    if (prefabsAtlantica.Length > 0) instancivel = prefabsBarreiras[1];
                    break;
                case Biome.Caatinga:
                    if (prefabsCaatinga.Length > 0) instancivel = prefabsBarreiras[2];
                    break;
                case Biome.Pantanal:
                    if (prefabsPantanal.Length > 0) instancivel = prefabsBarreiras[3];
                    break;
            }
        }
        return instancivel;
    }
}
