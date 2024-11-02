using UnityEngine;
[SerializeField]
public class NonMeatFood
{
    // Atribuição dos limites de valores para os atributos da classe
    static readonly float _maxNonMeatValue = 100f;
    static readonly float _minNonMeatValue = 0f;

    // Atributos da classe
    [SerializeField]
    FoodSize _nonMeatFoodSize;
    [SerializeField]
    float _foodQuantity;

    //Propriedades
    public FoodSize FoodSize { get { return _nonMeatFoodSize; } set { _nonMeatFoodSize = value; } }
    public float FoodQuantity { get { return _foodQuantity; } set { _foodQuantity = AdjustToClosestBoundary(value); } }
    public float MultiplyFoodQuantityByFactor { set { _foodQuantity = AdjustToClosestBoundary(value * FoodQuantity); } }

    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxNonMeatValue) { return _maxNonMeatValue; }
        else if (value < _minNonMeatValue) { return _minNonMeatValue; }
        return value;
    }
}