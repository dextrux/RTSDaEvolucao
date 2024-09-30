public class MeatFood
{
    // Atribuição dos limites de valores para os atributos da classe
    static readonly float _maxMeatValue = 100f;
    static readonly float _minMeatValue = 0f;
    // Atributos da classe
    MeatFoodType _meatFoodType;
    float _foodQuantity;

    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxMeatValue) { return _maxMeatValue; }
        else if (value < _minMeatValue) { return _minMeatValue; }
        return value;
    }

    // Get's
    public float GetRemainingMeatValue() { return this._foodQuantity; }
    public MeatFoodType GetMeatFoodType() { return this._meatFoodType; }

    // String Get's
    public string GetRemainingMeatAsString() { return this._foodQuantity.ToString(); }
    public string GetMeatFoodTypeAsString() { return this._meatFoodType.ToString(); }

    // Set's  
    public void SetAllMeatValues(MeatFoodType type, float quantity)
    {
        SetMeatFoodType(type);
        SetMeatFoodQuantity(quantity);
    }
    public void SetMeatFoodType(MeatFoodType type) { this._meatFoodType = type; }
    public void SetMeatFoodQuantity(float value) { this._foodQuantity = AdjustToClosestBoundary(value); }

    // Event's Set's
    public void SetNewFoodValueByFactor(float factor) { SetMeatFoodQuantity(AdjustToClosestBoundary(_foodQuantity * factor)); }
}