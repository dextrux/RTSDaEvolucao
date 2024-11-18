using System;
using UnityEngine;

[System.Serializable]
public class Humidity
{
    // Atribuição dos limites de valores para os atributos da classe
    readonly static float _maxHumidity = 100f;
    readonly static float _minHumidity = 0f;
    // Atributos da classe
    [SerializeField]
    float _idealHumidity;
    [SerializeField]
    float _currentHumidity;

    // Construtor
    public Humidity(float idealHumidity, float currentHumidity)
    {
        IdealHumidity = idealHumidity;
        CurrentHumidity = currentHumidity;
    }
    public Humidity(float currentHumidity)
    {
        CurrentHumidity = currentHumidity;
    }

    // Prorieades
    public float IdealHumidity { get { return this._idealHumidity; } set { this._idealHumidity = Humidity.AdjustToClosestBoundary(value); } }
    public float CurrentHumidity { get { return this._currentHumidity; } set { this._currentHumidity = Humidity.AdjustToClosestBoundary(value); } }

    // Propriedades de Desastre
    public float IncreaseHumidityByValue { set { this._currentHumidity = Humidity.AdjustToClosestBoundary(this._currentHumidity + value); } }
    public float MultiplyHumidityByValue { set { this._currentHumidity = Humidity.AdjustToClosestBoundary(this._currentHumidity * value); } }

    public void IdealHumidityAdjustSum(float portion)
    {
        _idealHumidity += portion;
        AdjustToClosestBoundary(_currentHumidity);
    }
    // Boundaries Check
    private static float AdjustToClosestBoundary(float value)
    {
        if (value > _maxHumidity) { return _maxHumidity; }
        else if (value < _minHumidity) { return _minHumidity; }
        return value;
    }
}
