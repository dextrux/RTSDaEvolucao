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

    //Migra��o - Aumento de 50% a quantidade de pe�as de TileHuntStatus na regi�o e de diminui��o de 50% de pe�as de N�o-Carne.
    public void DesastreMigra��o()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentMeatFood[i].SetNewFoodValueByFactor(1.50f);
            this._tilesComponentNonMeatFood[i].SetNewFoodValueByFactor(0.50f);
        }
        MinorEventsRoutine();
    }

    //Infesta��o - Aumento de 50% a quantidade de pe�as de N�o-Carne na regi�o e de diminui��o de 50% de pe�as de TileHuntStatus. 
    public void DesastreInfesta��o()
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

    //Desertifica��o - Aumenta a Temperature em 75% e diminui��o da Humidity em 75% dentro dos tiles de transi��o. Transforma a regi�o afetada em uma �rea de Caatinga.
    public void DesastreDesertifica��o()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(1.75f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(0.25f);
        }
        DesastreFinalRoutine();
    }

    //Alagamentos - Aumenta a Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transi��o. Transforma a regi�o afetada em uma �rea de Pantanal
    public void DesastreAlagamento()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(1.75f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(1.75f);
        }
        DesastreFinalRoutine();
    }

    //Geadas - Diminui��o da Temperature em 75% e diminui��o da Humidity em 75% dentro dos tiles de transi��o. Transforma a regi�o afetada em uma �rea de Mata das Araucarias
    public void DesastreGeada()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            this._tilesComponentTile[i].GetTemperature().SetNewCurrentTemperatureByFactor(0.25f);
            this._tilesComponentTile[i].GetHumidity().SetNewCurrentHumidityByFactor(0.25f);
        }
        DesastreFinalRoutine();
    }

    // Tempestades - Diminui��o da Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transi��o.Transforma a regi�o afetada em uma �rea de Mata_Atlantica
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

    //Os eventos de cat�strofes clim�ticas tem o intuito de transformar completamente uma �rea de um TileBiomeStatus para o outro. Portanto, as cat�strofes mudam os tiles para um TileBiomeStatus de transi��o que � mortal para as pe�as do jogador e das pe�as NPCs e duram por 4 turnos, permanecer em um tile de transi��o far� com que a vida da pe�a diminua a cada turno que ele esteja nele. 
}
