public class StatusBar
{
    // Constantes da classe  
    readonly float _minBarValue = 0f;

    // Atributos
    float _maxBarValue = 100f;
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
