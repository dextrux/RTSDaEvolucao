using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class GerenciadorDesastres : MonoBehaviour
{
    private int _disasterCount = 0;
    private int _minorEventsCount = 0;
    public List<Tile> tiles;

    //Migração - Aumento de 50% a quantidade de peças de TileHuntStatus na região e de diminuição de 50% de peças de Não-Carne.
    private void DesastreMigração()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTileFruitsStatusIndex(0.5f);
            tiles[i].SetTileHuntStatusIndex(1.5f);
        }
        MinorEventsRoutine();
    }

    //Infestação - Aumento de 50% a quantidade de peças de Não-Carne na região e de diminuição de 50% de peças de TileHuntStatus. 
    private void DesastreInfestação()
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

    //Desertificação - Aumenta a Temperature em 75% e diminuição da Humidity em 75% dentro dos tiles de transição. Transforma a região afetada em uma área de Caatinga.
    private void DesastreDesertificação()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(1.75f);
            tiles[i].SetHumidity(0.25f);
        }
        DesastreFinalRoutine();
    }

    //Alagamentos - Aumenta a Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transição. Transforma a região afetada em uma área de Pantanal
    private void DesastreAlagamento()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(1.75f);
            tiles[i].SetHumidity(1.75f);
        }
        DesastreFinalRoutine();
    }

    //Geadas - Diminuição da Temperature em 75% e diminuição da Humidity em 75% dentro dos tiles de transição. Transforma a região afetada em uma área de Mata das Araucarias
    private void DesastreGeada()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].SetTemperature(0.25f);
            tiles[i].SetHumidity(0.25f);
        }
        DesastreFinalRoutine();
    }

    // Tempestades - Diminuição da Temperature em 75% e aumento da Humidity em 75% dentro dos tiles de transição.Transforma a região afetada em uma área de Mata_Atlantica
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

    //Os eventos de catástrofes climáticas tem o intuito de transformar completamente uma área de um TileBiomeStatus para o outro. Portanto, as catástrofes mudam os tiles para um TileBiomeStatus de transição que é mortal para as peças do jogador e das peças NPCs e duram por 4 turnos, permanecer em um tile de transição fará com que a vida da peça diminua a cada turno que ele esteja nele. 
}






