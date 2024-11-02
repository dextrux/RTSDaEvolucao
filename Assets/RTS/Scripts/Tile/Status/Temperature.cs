using System;
using UnityEngine;

[System.Serializable]
public class Temperature
{
    // Atribuição dos limites de valores para os atributos da classe
    readonly static float _maxTemperature = 40f;
    readonly static float _minTemperature = 0f;
    // Atributos da classe
    [SerializeField]
    float _idealTemperature;
    [SerializeField]
    float _currentTemperature;

    // Construtor
    public Temperature(float idealTemperature, float currentTemperature)
    {
        IdealTemperature = idealTemperature;
        CurrentTemperature = currentTemperature;
    }
    public Temperature(float currentTemperature)
    {
        CurrentTemperature = currentTemperature;
    }

    // Prorieades
    public float IdealTemperature {  get { return this._idealTemperature; } set { this._idealTemperature = Temperature.AdjustToClosestBoundary(value); } }
    public float CurrentTemperature { get { return this._currentTemperature; } set { this._currentTemperature = Temperature.AdjustToClosestBoundary(value); } }

    // Propriedades de Desastre
    public float IncreaseTemperatureByValue { set { this._currentTemperature = Temperature.AdjustToClosestBoundary(this._currentTemperature + value); } }
    public float MultiplyTemperatureByValue { set { this._currentTemperature = Temperature.AdjustToClosestBoundary(this._currentTemperature * value); } }


    // Boundaries Check
    private static float AdjustToClosestBoundary(float value)
    {
        if (value > _maxTemperature) { return _maxTemperature; }
        else if (value < _minTemperature) { return _minTemperature; }
        return value;
    }
}

