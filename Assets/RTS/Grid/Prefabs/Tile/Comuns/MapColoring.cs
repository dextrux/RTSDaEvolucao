using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColoring : MonoBehaviour
{
    private void Update()
    {
        ColorGameObjects();
    }
    void ColorGameObjects()
    {
        if (this.GetComponent<Tile>().GetBiome() == Biome.Pantanal )
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.green;
        }
        /*else if (this.GetComponent<Tile>().GetBiome() == Biome.Cerrado )
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.yellow;
        }*/
        else if (this.GetComponent<Tile>().GetBiome() == Biome.Mata_Atlantica )
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.black;
        }
        else if (this.GetComponent<Tile>().GetBiome() == Biome.Pampa )
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.gray;
        }
        else if (this.GetComponent<Tile>().GetBiome() == Biome.Mata_das_Araucarias )
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.blue;
        }
        if(this.GetComponent<Tile>().GetTileType() == TileType.Barreira)
        {
            Renderer renderer = this.GetComponent<Renderer>();
            renderer.material.color = Color.red;
        }
    }
}
