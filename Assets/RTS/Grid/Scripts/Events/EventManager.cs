using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EventManager : MonoBehaviour
{
    private int _disasterCount = 0;
    private int _minorEventsCount = 0;
    public List<GameObject> tiles;
    private List<Tile> _tilesComponentTile;
    private List<NonMeatFood> _tilesComponentNonMeatFood;
    private List<MeatFood> _tilesComponentMeatFood;

    public void GetTileComponentsInTiles()
    {
        for (int i = 0; i < tiles.Count; i++) { this._tilesComponentTile[i] = this.tiles[i].GetComponent<Tile>(); }
    }

    public void GetNonMeatFoodComponentsInTiles()
    {
        for (int i = 0; i < tiles.Count; i++) { this._tilesComponentNonMeatFood[i] = this.tiles[i].GetComponent<NonMeatFood>(); }
    }

    public void GetMeatFoodComponentsInTiles()
    {
        for (int i = 0; i < tiles.Count; i++) { this._tilesComponentMeatFood[i] = this.tiles[i].GetComponent<MeatFood>(); }
    }

    //Migração - Aumento de 50% a quantidade de peças de TileHuntStatus na região e de diminuição de 50% de peças de Não-Carne.
    public void DesastreMigração()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentMeatFood[i].SetNewFoodValueByFactor(1.50f);
            this._tilesComponentNonMeatFood[i].SetNewFoodValueByFactor(0.50f);
        }
        MinorEventsRoutine();
    }

    //Infestação - Aumento de 50% a quantidade de peças de Não-Carne na região e de diminuição de 50% de peças de TileHuntStatus. 
    public void DesastreInfestação()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentMeatFood[i].SetNewFoodValueByFactor(0.50f);
            this._tilesComponentNonMeatFood[i].SetNewFoodValueByFactor(1.50f);
        }
        MinorEventsRoutine();
    }

    //Onda de Calor - Aumenta em 20% a Temperature dos tiles afetados.
    public void DesastreOndaDeCalor()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(1.20f);
        }
        MinorEventsRoutine();
    }

    //Frente Fria - Diminui em 20% a Temperature dos tiles afetados.
    public void DesastreFrenteFria()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(0.80f);
        }
        MinorEventsRoutine();
    }

    //Chuvas - Aumenta em 20% a Humidity dos tiles afetados.
    public void DeastreChuvas()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(1.20f);
        }
        MinorEventsRoutine();
    }

    //Secas - Diminui em 20% a Humidity dos tiles afetados.
    public void DesastreSecas()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(0.8f);
        }
        MinorEventsRoutine();
    }

    //Desertificação - Aumenta a Temperature em 75% e diminuição da Humidity em 75% dentro dos tiles de transição. Transforma a região afetada em uma área de Caatinga.
    public void DesastreDesertificação()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(1.75f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(0.25f);
        }
        DesastreFinalRoutine();
    }

    //Alagamentos - Aumenta a Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transição. Transforma a região afetada em uma área de Pantanal
    public void DesastreAlagamento()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(1.75f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(1.75f);
        }
        DesastreFinalRoutine();
    }

    //Geadas - Diminuição da Temperature em 75% e diminuição da Humidity em 75% dentro dos tiles de transição. Transforma a região afetada em uma área de Mata das Araucarias
    public void DesastreGeada()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(0.25f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(0.25f);
        }
        DesastreFinalRoutine();
    }

    // Tempestades - Diminuição da Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transição.Transforma a região afetada em uma área de Mata_Atlantica
    public void DesastreTempestade()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(0.25f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(1.75f);
        }
        DesastreFinalRoutine();
    }


    public void AddTileToList(GameObject tile)
    {
        tiles.Add(tile);
    }

    private void RemoveAllFromList()
    {
        tiles.Clear();
        _tilesComponentTile.Clear();
        _tilesComponentMeatFood.Clear();
        _tilesComponentNonMeatFood.Clear();
    }

    private void DesastreFinalRoutine()
    {
        RemoveAllFromList();
        _disasterCount = _disasterCount + 1;
    }

    private void MinorEventsRoutine()
    {
        RemoveAllFromList();
        _minorEventsCount = _minorEventsCount + 1;
    }

    //Os eventos de catástrofes climáticas tem o intuito de transformar completamente uma área de um TileBiomeStatus para o outro. Portanto, as catástrofes mudam os tiles para um TileBiomeStatus de transição que é mortal para as peças do jogador e das peças NPCs e duram por 4 turnos, permanecer em um tile de transição fará com que a vida da peça diminua a cada turno que ele esteja nele. 
}
