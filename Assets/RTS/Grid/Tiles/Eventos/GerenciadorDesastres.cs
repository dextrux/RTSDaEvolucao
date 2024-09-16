using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GerenciadorDesastres : MonoBehaviour
{
    private int _disasterCount = 0;
    private int _minorEventsCount = 0;
    public List<Tile> tiles;

    //Migra��o - Aumento de 50% a quantidade de pe�as de TileHuntStatus na regi�o e de diminui��o de 50% de pe�as de N�o-Carne.
    private void DesastreMigra��o()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTileFruitsStatusIndex(0.5f);
            tiles[i].SetTileHuntStatusIndex(1.5f);
        }
        MinorEventsRoutine();
    }

    //Infesta��o - Aumento de 50% a quantidade de pe�as de N�o-Carne na regi�o e de diminui��o de 50% de pe�as de TileHuntStatus. 
    private void DesastreInfesta��o()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTileFruitsStatusIndex(1.5f);
            tiles[i].SetTileHuntStatusIndex(0.5f);
        }
        MinorEventsRoutine();
    }

    //Onda de Calor - Aumenta em 20% a Temperature dos tiles afetados.
    private void DesastreOndaDeCalor()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(1.2f);
        }
        MinorEventsRoutine();
    }

    //Frente Fria - Diminui em 20% a Temperature dos tiles afetados.
    private void DesastreFrenteFria()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(0.8f);
        }
        MinorEventsRoutine();
    }

    //Chuvas - Aumenta em 20% a Humidity dos tiles afetados.
    private void DeastreChuvas()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetHumidity(1.2f);
        }
        MinorEventsRoutine();
    }

    //Secas - Diminui em 20% a Humidity dos tiles afetados.
    private void DesastreSecas()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetHumidity(0.8f);
        }
        MinorEventsRoutine();
    }

    //Desertifica��o - Aumenta a Temperature em 75% e diminui��o da Humidity em 75% dentro dos tiles de transi��o. Transforma a regi�o afetada em uma �rea de Caatinga.
    private void DesastreDesertifica��o()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(1.75f);
            tiles[i].SetHumidity(0.25f);
        }
        DesastreFinalRoutine();
    }

    //Alagamentos - Aumenta a Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transi��o. Transforma a regi�o afetada em uma �rea de Pantanal
    private void DesastreAlagamento()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(1.75f);
            tiles[i].SetHumidity(1.75f);
        }
        DesastreFinalRoutine();
    }

    //Geadas - Diminui��o da Temperature em 75% e diminui��o da Humidity em 75% dentro dos tiles de transi��o. Transforma a regi�o afetada em uma �rea de Mata das Araucarias
    private void DesastreGeada()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(0.25f);
            tiles[i].SetHumidity(0.25f);
        }
        DesastreFinalRoutine();
    }

    // Tempestades - Diminui��o da Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transi��o.Transforma a regi�o afetada em uma �rea de Mata_Atlantica
    private void DesastreTempestade()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetHumidity(1.75f);
            tiles[i].SetTemperature(0.25f);
        }
        DesastreFinalRoutine();
    }


    public void AddTileToList(Tile tile)
    {
        tiles.Add(tile);
    }

    private void RemoveAllFromList()
    {
        tiles.Clear();
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






