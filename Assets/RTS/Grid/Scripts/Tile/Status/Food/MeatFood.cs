public class MeatFood
{
    // Atribuição dos limites de valores para os atributos da classe
    const float _maxHuntValue = 100f;
    const float _minHuntValue = 0f;
    // Atributos da classe
    float _smallSizeHunt;
    float _midSizeHunt;
    float _largeSizeHunt;
    float _corpsesHunt;

    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxHuntValue)
        {
            return _maxHuntValue;
        }
        else if (value < _minHuntValue)
        {
            return _minHuntValue;
        }
        return value;
    }

    // Get's
    public float GetSmallSizeHuntValue() { return this._smallSizeHunt; }
    public float GetMidSizeHuntValue() { return this._midSizeHunt; }
    public float GetLargeSizeHuntValue() { return this._largeSizeHunt; }
    public float GetCorpsesHuntValue() { return this._corpsesHunt; }

    // String Get's
    public string GetSmallSizeHuntAsString() { return this._smallSizeHunt.ToString(); }
    public string GetMidSizeHuntAsString() { return this._midSizeHunt.ToString(); }
    public string GetLargeSizeHuntAsString() { return this._largeSizeHunt.ToString(); }
    public string GetCorpsesHuntAsString() { return this._corpsesHunt.ToString(); }

    // Set's  
    public void SetAllHuntValues(float smallSizeHunt, float midSizeHunt, float largeSizeHunt, float corpsesHunt)
    {
        SetSmallSizeHuntValue(smallSizeHunt);
        SetMidSizeHuntValue(midSizeHunt);
        SetLargeSizeHuntValue(largeSizeHunt);
        SetCorpsesHuntValue(corpsesHunt);
    }

    public void SetSmallSizeHuntValue(float smallSizeHunt)
    {
        this._smallSizeHunt = AdjustToClosestBoundary(smallSizeHunt);
    }

    public void SetMidSizeHuntValue(float midSizeHunt)
    {
        this._midSizeHunt = AdjustToClosestBoundary(midSizeHunt);
    }

    public void SetLargeSizeHuntValue(float largeSizeHunt)
    {
        this._largeSizeHunt = AdjustToClosestBoundary(largeSizeHunt);
    }

    public void SetCorpsesHuntValue(float corpsesHunt)
    {
        this._corpsesHunt = AdjustToClosestBoundary(corpsesHunt);
    }

    // Event's Set's

    public void SetNewValueToAllMeatFoodAtributesByFactor(float factor)
    {
        SetNewSmallSizeHuntByFactor(factor);
        SetNewMidSizeHuntByFactor(factor);
        SetNewLargeSizeHuntByFactor(factor);
        SetNewCorpsesHuntByFactor(factor);
    }
    public void SetNewSmallSizeHuntByFactor(float factor)
    {
        SetSmallSizeHuntValue(_smallSizeHunt * factor);
    }

    public void SetNewMidSizeHuntByFactor(float factor)
    {
        SetMidSizeHuntValue(_midSizeHunt * factor);
    }

    public void SetNewLargeSizeHuntByFactor(float factor)
    {
        SetLargeSizeHuntValue(_largeSizeHunt * factor);
    }

    public void SetNewCorpsesHuntByFactor(float factor)
    {
        SetCorpsesHuntValue(_corpsesHunt * factor);
    }
}

