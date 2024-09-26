public class NonMeatFood
{
    // Atribuição dos limites de valores para os atributos da classe
    const float _maxVeggieValue = 100f;
    const float _minVeggieValue = 0f;
    // Atributos da classe
    NonMeatFoodType _nonMeatFoodType;
    float _foodQuantity;

    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxVeggieValue) { return _maxVeggieValue; }
        else if (value < _minVeggieValue) {return _minVeggieValue; }
        return value;
    }

    // Get's
    public float GetRemainingVeggieValue() { return this._foodQuantity; }
    public NonMeatFoodType GetNonMeatFoodType() { return this._nonMeatFoodType; }

    // String Get's
    public string GetRemainingVeggieAsString() { return this._foodQuantity.ToString(); }
    public string GetNonMeatFoodTypeAsString() { return this._nonMeatFoodType.ToString(); }

    // Set's  
    public void SetAllVeggieValues(NonMeatFoodType type, float quantity)
    {
        SetNonMeatFoodType(type);
        SetNonMeatFoodQuantity(quantity);
    }
    public void SetNonMeatFoodType(NonMeatFoodType type) { this._nonMeatFoodType = type; }
    public void SetNonMeatFoodQuantity(float value) { this._foodQuantity = AdjustToClosestBoundary(value); }

    // Event's Set's
    public void SetNewFoodValueByFactor(float factor) { SetNonMeatFoodQuantity(AdjustToClosestBoundary(_foodQuantity * factor)); }
}