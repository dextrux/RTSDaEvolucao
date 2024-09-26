using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jubileuson : MonoBehaviour
{
    // Atributos da classe
    private Temperature _pieceTemperature;
    private Humidity _pieceHumidity;
    private StatusBar _healthBar;
    private StatusBar _energyBar;
    private StatusBar _fertilityBar;
    private StatusBar _dietTendency;
    private StatusBar _strengthBar;
    private PieceDiet _pieceDiet;
    private PieceLevel _pieceLevel;
    private int _pieceRemainingActions;

    // Métodos
    public void InitializePiece(Temperature temperature, Humidity humidity, StatusBar healthBar, StatusBar energyBar, StatusBar fertilityBar, StatusBar dietTendency, StatusBar strengthBar, PieceDiet pieceDiet, PieceLevel pieceLevel)
    {
        _pieceTemperature = temperature;
        _pieceHumidity = humidity;
        _healthBar = healthBar;
        _energyBar = energyBar;
        _fertilityBar = fertilityBar;
        _dietTendency = dietTendency;
        _strengthBar = strengthBar;
        _pieceDiet = pieceDiet;
        _pieceLevel = pieceLevel;
        ResetActions();
    }

    public void ResetActions()
    {
        _pieceRemainingActions = (int)_pieceLevel; // Reset to default actions
    }

    // Getters
    public Temperature GetTemperature() => _pieceTemperature;
    public Humidity GetHumidity() => _pieceHumidity;
    public StatusBar GetHealthBar() => _healthBar;
    public StatusBar GetEnergyBar() => _energyBar;
    public StatusBar GetFertilityBar() => _fertilityBar;
    public StatusBar GetDietTendency() => _dietTendency;
    public StatusBar GetStrengthBar() => _strengthBar;
    public PieceDiet GetDiet() => _pieceDiet;
    public PieceLevel GetLevel() => _pieceLevel;
    public int GetRemainingActions() => _pieceRemainingActions;

    // Setters
    public void SetTemperature(Temperature temperature) => _pieceTemperature = temperature;
    public void SetHumidity(Humidity humidity) => _pieceHumidity = humidity;
    public void SetHealthBar(StatusBar healthBar) => _healthBar = healthBar;
    public void SetEnergyBar(StatusBar energyBar) => _energyBar = energyBar;
    public void SetFertilityBar(StatusBar fertilityBar) => _fertilityBar = fertilityBar;
    public void SetDietTendency(StatusBar dietTendency) => _dietTendency = dietTendency;
    public void SetStrengthBar(StatusBar strengthBar) => _strengthBar = strengthBar;
    public void SetDiet(PieceDiet pieceDiet) => _pieceDiet = pieceDiet;
    public void SetLevel(PieceLevel pieceLevel) => _pieceLevel = pieceLevel;
}

