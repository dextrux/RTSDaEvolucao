public class Temperature
{
    // Atribuição dos limites de valores para os atributos da classe
    const float _maxTemperature = 40f;
    const float _minTemperature = 0f;
    // Atributos da classe
    float _idealTemperature;
    float _currentTemperature;
    float _lastTemperature;

    // Boundaries Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxTemperature)
        {
            return _maxTemperature;
        }
        else if (value < _minTemperature)
        {
            return _minTemperature;
        }
        return value;
    }

    // Construtor
    public void CreateTemperature(float idealTemperature, float currentTemperature)
    {
        SetIdealTemperatureValue(idealTemperature);
        SetCurrentTemperatureValue(currentTemperature);
    }

    // Get's
    public float GetIdealTemperatureValue() { return this._idealTemperature; }
    public float GetCurrentTemperatureValue() { return this._currentTemperature; }
    public float GetLastTemperatureValue() { return this._lastTemperature; }

    // String Get's
    public string GetIdealTemperatureAsString() { return this._idealTemperature.ToString(); }
    public string GetCurrentTemperatureAsString() { return this._currentTemperature.ToString(); }
    public string GetLastTemperatureAsString() { return this._lastTemperature.ToString(); }

    // Set's  
    public void SetAllTemperatureValues(float idealTemperature, float currentTemperature)
    {
        SetIdealTemperatureValue(idealTemperature);
        SetCurrentTemperatureValue(currentTemperature);
    }

    public void SetIdealTemperatureValue(float idealTemperature)
    {
        this._idealTemperature = AdjustToClosestBoundary(idealTemperature);
    }

    public void SetCurrentTemperatureValue(float currentTemperature)
    {
        this._currentTemperature = AdjustToClosestBoundary(currentTemperature);
        this._lastTemperature = this._currentTemperature; // Update last temperature to the new current value
    }

    // Event's Set's
    public void SetNewValueToAllTemperatureAtributesByFactor(float factor)
    {
        SetNewIdealTemperatureByFactor(factor);
        SetNewCurrentTemperatureByFactor(factor);
    }
    public void SetNewIdealTemperatureByFactor(float factor)
    {
        SetIdealTemperatureValue(_currentTemperature * factor);
    }

    public void SetNewCurrentTemperatureByFactor(float factor)
    {
        SetCurrentTemperatureValue(_currentTemperature * factor);
    }
}

