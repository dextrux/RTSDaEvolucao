using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Atributos do Tile
    [Range(1f, 100f)]
    public float indexHumidity; 
    [Range(1f, 40f)]
    public float indexTemperature; 
    // Atributos de alimentação
    [Range(1f, 100f)]
    public float indexTileHuntStatus;
    [Range(1f, 100f)]
    public float indexTileFruitsStatus;

    //Constantes de verificação
    private const float MaxHumidity = 100;
    private const float MinHumidity = 0;
    private const float MaxTemperature = 40;
    private const float MinTemperature = 0;
    private const float MaxTileHuntStatus = 100;
    private const float MinTileHuntStatus = 0;
    private const float MaxTileFruitsStatus = 100;
    private const float MinTileFruitsStatus = 0;

    // TileBiomeStatus atual do tile
    public TileBiomeStatus TileBiomeStatusAtual;
    public TileHuntStatus TileHuntStatusAtual;
    public TileFruitsStatus TileFruitsStatusAtual;

    // Índices de referência
    private float lastHumidity;
    private float lastTemperature;
    private float lastTileHuntStatus;
    private float lastTileFruitsStatus;

    // Referência ao material do Tile
    private Renderer tileRenderer; // Renderer para aplicar a cor no material do Tile
    private Color targetColor;     // Cor alvo para interpolação
    private Color currentColor;    // Cor atual do Tile

    void Start()
    {
        DefinirStatusAnteriorDoTile();

        // Definir o TileBiomeStatus com base nos índices de Humidity e Temperature
        TileBiomeStatusAtual = SetTileBiomeStatus(indexHumidity, indexTemperature);
        TileHuntStatusAtual = SetTileHuntStatus(indexTileHuntStatus);
        TileFruitsStatusAtual = SetTileFruitsStatus(indexTileFruitsStatus);

        // Debug para verificar o estado inicial do tile
        Debug.Log($"TileBiomeStatus Atual: {TileBiomeStatusAtual}");
        Debug.Log($"TileHuntStatus Atual: {TileHuntStatusAtual}");
        Debug.Log($"Coleta Atual: {TileFruitsStatusAtual}");
    }

    private void Update()
    {
        VerificarEstadoTile();
    }

    // Método para verificar e atualizar o estado do Tile se houver mudanças nos índices
    void VerificarEstadoTile()
    {
        // Verificar mudanças no índice de Humidity ou Temperature para redefinir o TileBiomeStatus
        if (indexHumidity != lastHumidity || indexTemperature != lastTemperature)
        {
            TileBiomeStatusAtual = SetTileBiomeStatus(indexHumidity, indexTemperature);
            Debug.Log($"TileBiomeStatus Atualizado: {TileBiomeStatusAtual}");
            lastHumidity = indexHumidity;
            lastTemperature = indexTemperature;
        }

        // Verificar mudanças no índice de TileHuntStatus para redefinir o estado de TileHuntStatus
        if (indexTileHuntStatus != lastTileHuntStatus)
        {
            TileHuntStatusAtual = SetTileHuntStatus(indexTileHuntStatus);
            Debug.Log($"TileHuntStatus Atualizada: {TileHuntStatusAtual}");
            lastTileHuntStatus = indexTileHuntStatus;
        }

        // Verificar mudanças no índice de TileFruitsStatus para redefinir o estado de coleta
        if (indexTileFruitsStatus != lastTileFruitsStatus)
        {
            TileFruitsStatusAtual = SetTileFruitsStatus(indexTileFruitsStatus);
            Debug.Log($"Coleta Atualizada: {TileFruitsStatusAtual}");
            lastTileFruitsStatus = indexTileFruitsStatus;
        }
    }

    private void DefinirStatusAnteriorDoTile()
    {
        lastHumidity = indexHumidity;
        lastTemperature = indexTemperature;
        lastTileHuntStatus = indexTileHuntStatus;
        lastTileFruitsStatus = indexTileFruitsStatus;
    }

    //Seção de Get's e Set's

    //Get's e Set's enumeradores
    public TileBiomeStatus GetTileBiomeStatus()
    {
        return TileBiomeStatusAtual;

    }
    public TileFruitsStatus GetTileFruitsStatus()
    {
        return TileFruitsStatusAtual;
    }
    public TileHuntStatus GetTileHuntStatus()
    {
        return TileHuntStatusAtual;
    }

    // Método para definir o TileBiomeStatus com base nos índices de Humidity e Temperature
    TileBiomeStatus SetTileBiomeStatus(float humidity, float temperature)
    {
        if (humidity >= 0 && humidity <= 50 && temperature >= 0 && temperature <= 20)
        {
            return TileBiomeStatus.Mata_das_Araucarias;
        }
        else if (humidity >= 51 && humidity <= 100 && temperature >= 0 && temperature <= 20)
        {
            return TileBiomeStatus.Mata_Atlantica;
        }
        else if (humidity >= 51 && humidity <= 100 && temperature >= 21 && temperature <= 40)
        {
            return TileBiomeStatus.Pantanal;
        }
        else if (humidity >= 0 && humidity <= 50 && temperature >= 21 && temperature <= 40)
        {
            return TileBiomeStatus.Caatinga;
        }
        return TileBiomeStatus.Erro;
    }

    // Método para definir a TileHuntStatus com base no índice
    TileHuntStatus SetTileHuntStatus(float index)
    {
        if (index <= 25)
        {
            return TileHuntStatus.TileHuntStatusVazio;
        }
        else if (index > 25 && index <= 50)
        {
            return TileHuntStatus.TileHuntStatusEscassa;
        }
        else if (index > 50 && index <= 75)
        {
            return TileHuntStatus.TileHuntStatusModerada;
        }
        else if (index > 75)
        {
            return TileHuntStatus.TileHuntStatusAbundante;
        }
       return TileHuntStatus.TileHuntStatusVazio;
    }

    // Método para definir as TileFruitsStatus com base no índice
    TileFruitsStatus SetTileFruitsStatus(float index)
    {
        if (index <= 25)
        {
            return TileFruitsStatus.TileFruitsStatusVazio;
        }
        else if (index > 25 && index <= 50)
        {
            return TileFruitsStatus.TileFruitsStatusEscassas;
        }
        else if (index > 50 && index <= 75)
        {
            return TileFruitsStatus.TileFruitsStatusModerada;
        }       
        else if (index > 75)
        {
            return TileFruitsStatus.FlorestaFrutifera;
        }
        // Valor padrão caso nenhum estado específico seja encontrado
        return TileFruitsStatus.TileFruitsStatusVazio;
    }

    //Get's e Set's de indicadores
    public float GetHumidity()
    {
        return indexHumidity;
    }

    public float GetTemperature()
    {
        return indexTemperature;
    }

    public void SetHumidity(float modificador)
    {
        lastHumidity = indexHumidity;
        indexHumidity = indexHumidity * modificador;
    }

    public void SetTemperature(float modificador)
    {
        lastTemperature = indexTemperature;
        indexTemperature = indexTemperature * modificador;
    }

    public void SetTileHuntStatusIndex(float modificador)
    {
        lastTileHuntStatus = indexTileHuntStatus;
        indexTileHuntStatus = indexTileHuntStatus * modificador;
    }

    public void SetTileFruitsStatusIndex(float modificador)
    {
        lastTileFruitsStatus = indexTileFruitsStatus;
        indexTileFruitsStatus = indexTileFruitsStatus * modificador;
        //if (indexTileFruitsStatus * modificador > indexTileFruitsStatus.Range)
        //{

        //}
    }

    public float GetTileHuntStatusIndex()
    {
        return indexTileHuntStatus;
    }

    public float GetTileFruitsStatusIndex()
    {
        return indexTileFruitsStatus;
    }

    //Get's do formato string

    public string GetTileBiomeStatusString()
    {
        return TileBiomeStatusAtual.ToString();
    }

    public string GetTileHuntStatusString()
    {
        return TileHuntStatusAtual.ToString();
    }
    public string GetTileFruitsStatusString()
    {
        return TileFruitsStatusAtual.ToString();
    }
    public string GetTileHuntStatusIndexString()
    {
        return indexTileHuntStatus.ToString();
    }
    public string GetTileFruitsStatusIndexString()
    {
        return indexTileFruitsStatus.ToString();
    }
    public string GetHumidityIndexString()
    {
        return indexHumidity.ToString();
    }
    public string GetTemperatureString()
    {
        return indexTemperature.ToString();
    }
}
