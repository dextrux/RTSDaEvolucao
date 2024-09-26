public class Humidity
{
    // Atribuição dos limites de valores para os atributos da classe
    const float _maxHumidity = 100f;
    const float _minHumidity = 0f;
    // Atributos da classe
    float _idealHumidity;
    float _currentHumidity;
    float _lastHumidity;

    // Boundary check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxHumidity)
        {
            return _maxHumidity;
        }
        else if (value < _minHumidity)
        {
            return _minHumidity;
        }
        return value;
    }

    // Construtor
    public void CreateHumidity(float idealHumidity, float currentHumidity)
    {
        SetIdealHumidityValue(idealHumidity);
        SetCurrentHumidityValue(currentHumidity);
    }

    // Get's
    public float GetIdealHumidityValue() { return this._idealHumidity; }
    public float GetCurrentHumidityValue() { return this._currentHumidity; }
    public float GetLastHumidityValue() { return this._lastHumidity; }

    // String Get's
    public string GetIdealHumidityAsString() { return this._idealHumidity.ToString(); }
    public string GetCurrentHumidityAsString() { return this._currentHumidity.ToString(); }
    public string GetLastHumidityAsString() { return this._lastHumidity.ToString(); }

    // Set's  
    public void SetAllHumidityValues(float idealHumidity, float currentHumidity)
    {
        SetIdealHumidityValue(idealHumidity);
        SetCurrentHumidityValue(currentHumidity);
    }

    public void SetIdealHumidityValue(float idealHumidity)
    {
        this._idealHumidity = AdjustToClosestBoundary(idealHumidity);
    }

    public void SetCurrentHumidityValue(float currentHumidity)
    {
        this._currentHumidity = AdjustToClosestBoundary(currentHumidity);
        this._lastHumidity = this._currentHumidity;
    }

    // Event's Set's

    public void SetNewValueToAllHumidityAtributesByFactor(float factor)
    {
        SetNewIdealHumidityByFactor(factor);
        SetNewCurrentHumidityByFactor(factor);
    }
    public void SetNewIdealHumidityByFactor(float factor)
    {
        SetIdealHumidityValue(_currentHumidity * factor);
    }

    public void SetNewCurrentHumidityByFactor(float factor)
    {
        SetCurrentHumidityValue(_currentHumidity * factor);
    }
}
