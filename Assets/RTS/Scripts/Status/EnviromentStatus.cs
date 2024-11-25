using System;
using UnityEngine;

[System.Serializable]
public class EnviromentStatus
{
    // Atributos da classe
    private readonly float _minValue;
    private readonly float _maxValue;
    [SerializeField] private float _idealValue;
    [SerializeField] private float _currentValue;

    // Construtor para inicializar os limites e os valores iniciais
    public EnviromentStatus(float minValue, float maxValue, float idealValue, float currentValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
        IdealValue = idealValue;
        CurrentValue = currentValue;
    }

    public EnviromentStatus(float minValue, float maxValue, float currentValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
        CurrentValue = currentValue;
    }

    public EnviromentStatus(EnviromentStatus enviromentStatus)
    {
        this._minValue = enviromentStatus._minValue;
        this._maxValue = enviromentStatus._maxValue;
        this._idealValue = enviromentStatus.CurrentValue;
        this._currentValue = enviromentStatus.CurrentValue;
    }

    // Propriedades
    public float IdealValue
    {
        get => _idealValue;
        set => _idealValue = AdjustToClosestBoundary(value);
    }

    public float CurrentValue
    {
        get => _currentValue;
        set => _currentValue = AdjustToClosestBoundary(value);
    }

    // Métodos para ajuste de valor
    public void IncreaseByValue(float value)
    {
        CurrentValue = AdjustToClosestBoundary(_currentValue + value);
    }

    public void MultiplyByValue(float value)
    {
        CurrentValue = AdjustToClosestBoundary(_currentValue * value);
    }

    public void AdjustIdealValueBySum(float portion)
    {
        IdealValue += portion;
        AdjustToClosestBoundary(_idealValue);
    }

    // Método privado para ajustar o valor aos limites estabelecidos
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxValue) return _maxValue;
        if (value < _minValue) return _minValue;
        return value;
    }
}

