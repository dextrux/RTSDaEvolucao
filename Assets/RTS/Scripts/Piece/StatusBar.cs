using UnityEngine;
[System.Serializable]
public class StatusBar
{
    // Constantes da classe  
    [SerializeField]
    readonly float _minBarValue = 0f;

    // Atributos
    [SerializeField]
    float _maxBarValue = 100f;
    [SerializeField]
    private float _currentValue;

    //Construtor
    public StatusBar(float maxValue, float currentValue)
    {
        MaxBarValue = maxValue;
        CurrentBarValue = currentValue;
    }

    // Propriedades
    public float CurrentBarValue { get { return _currentValue; } set { _currentValue = AdjustToClosestBoundary(value); } }
    public float MaxBarValue { get { return _maxBarValue; } set { _maxBarValue = value; } }
    public float MinBarValue { get { return _minBarValue; } }

    public void StatusAdjustSum(float portion)
    {
        MaxBarValue += portion;
        CurrentBarValue += portion;
    }
    public void StatusAdjustMultiplyPercent(float percent)
    {
        MaxBarValue *= percent;
        CurrentBarValue *= percent;
    }
    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxBarValue)
        {
            return _maxBarValue;
        }
        else if (value < _minBarValue)
        {
            return _minBarValue;
        }
        return value;
    }
}
