public class NonMeatFood
{
    // Atribuição dos limites de valores para os atributos da classe
    const float _maxNonMeatFoodValue = 100f;
    const float _minNonMeatFoodValue = 0f;
    // Atributos da classe
    float _fruits;
    float _grains;
    float _plants;

    // Use Unity lifecycle methods for initialization
    void Start()
    {
        // Example initialization; you can set values as required
        SetAllNonMeatFoodValues(50f, 60f, 80f);
    }

    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxNonMeatFoodValue)
        {
            return _maxNonMeatFoodValue;
        }
        else if (value < _minNonMeatFoodValue)
        {
            return _minNonMeatFoodValue;
        }
        return value;
    }

    // Get's
    public float GetFruitsValue() { return this._fruits; }
    public float GetGrainsValue() { return this._grains; }
    public float GetPlantsValue() { return this._plants; }

    // String Get's
    public string GetFruitsAsString() { return this._fruits.ToString(); }
    public string GetGrainsAsString() { return this._grains.ToString(); }
    public string GetPlantsAsString() { return this._plants.ToString(); }

    // Set's  
    public void SetAllNonMeatFoodValues(float fruits, float grains, float plants)
    {
        SetFruitsValue(fruits);
        SetGrainsValue(grains);
        SetPlantsValue(plants);
    }

    public void SetFruitsValue(float fruits)
    {
        this._fruits = AdjustToClosestBoundary(fruits);
    }

    public void SetGrainsValue(float grains)
    {
        this._grains = AdjustToClosestBoundary(grains);
    }

    public void SetPlantsValue(float plants)
    {
        this._plants = AdjustToClosestBoundary(plants);
    }

    // Event's Set's

    public void SetNewValueToAllNonMeatFoodAtributesByFactor(float factor)
    {
        SetNewFruitsByFactor(factor);
        SetNewGrainsByFactor(factor);
        SetNewPlantsByFactor(factor);
    }
    public void SetNewFruitsByFactor(float factor)
    {
        SetFruitsValue(_fruits * factor);
    }

    public void SetNewGrainsByFactor(float factor)
    {
        SetGrainsValue(_grains * factor);
    }

    public void SetNewPlantsByFactor(float factor)
    {
        SetPlantsValue(_plants * factor);
    }
}
