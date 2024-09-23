public class StatusBar
{
    // Constantes da classe
    float _maxBarValue = 100f;
    const float _minBarValue = 0f;

    // Atributo
    private float _currentValue;

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

    // Get's
    public float GetCurrentBarValue() { return _currentValue; }
    public float GetBarMaxValue() { return _maxBarValue; }
    public float GetBarMinValue() { return _minBarValue; }

    // String Get's
    public string GetCurrentBarValueAsString() { return _currentValue.ToString(); }
    public string GetBarMaxValueAsString() { return _maxBarValue.ToString(); }
    public string GetBarMinValueAsString() { return _minBarValue.ToString(); }

    // Set's
    public void SetNewBarValue(float value) { _currentValue = AdjustToClosestBoundary(value); }
    // Set new Max Value
    public void SetNewMaxBoundarie(float value){ _maxBarValue = value; }
}

