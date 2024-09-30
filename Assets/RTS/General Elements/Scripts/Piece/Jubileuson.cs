using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jubileuson : MonoBehaviour
{
    // Atributos da classe
    //public Temperature _pieceTemperature;
    //public Humidity _pieceHumidity;
    public StatusBar _healthBar;
    public StatusBar _energyBar;
    public StatusBar _fertilityBar;
    public StatusBar _strengthBar;
    public PieceDiet _pieceDiet;
    public PieceLevel _pieceLevel;
    public int _pieceRemainingActions;

    // Métodos
    public void InitializePiece(Temperature temperature, Humidity humidity, StatusBar healthBar, StatusBar energyBar, StatusBar fertilityBar, StatusBar strengthBar, PieceDiet pieceDiet, PieceLevel pieceLevel)
    {
        //_pieceTemperature = temperature;
        //_pieceHumidity = humidity;
        _healthBar = healthBar;
        _energyBar = energyBar;
        _fertilityBar = fertilityBar;
        _strengthBar = strengthBar;
        _pieceDiet = pieceDiet;
        _pieceLevel = pieceLevel;
        ResetActions();
    }

    private void Start()
    {
        _healthBar = new StatusBar(50f);
        _energyBar = new StatusBar(50f);
        _fertilityBar = new StatusBar(50f);
        _strengthBar = new StatusBar(50f);
       // _pieceHumidity.SetAllHumidityValues(50, 50);
       // _pieceTemperature.SetAllTemperatureValues(50, 50);
    }

    public void ResetActions()
    {
        //_pieceRemainingActions = (int)_pieceLevel; // Reset to default actions
    }

    // Getters
    public PieceDiet GetDiet() => _pieceDiet;
    public PieceLevel GetLevel() => _pieceLevel;
    public int GetRemainingActions() => _pieceRemainingActions;

    //public Temperature GetTemperature() => _pieceTemperature;

   // public Humidity GetHumidity() => _pieceHumidity;
    public StatusBar GetEnergyBar() { return _energyBar; }
    public StatusBar GetFertilityBar() { return _fertilityBar; }
    public StatusBar GetHealthBar() { return _healthBar; }

    public StatusBar GetStrenghtBar() { return _strengthBar; }


    // Setters
    public void SetDiet(PieceDiet pieceDiet) => _pieceDiet = pieceDiet;
    public void SetLevel(PieceLevel pieceLevel) => _pieceLevel = pieceLevel;
}

