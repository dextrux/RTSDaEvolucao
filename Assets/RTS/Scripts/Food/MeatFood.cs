using UnityEngine;
[SerializeField]
public class MeatFood
{
    // Atribuição dos limites de valores para os atributos da classe
    static readonly float _maxMeatValue = 100f;
    static readonly float _minMeatValue = 0f;

    // Atributos da classe
    [SerializeField]
    FoodSize _meatFoodSize;
    [SerializeField]
    float _foodQuantity;

    //Propriedades
    public FoodSize FoodSize {  get { return _meatFoodSize; }  set { _meatFoodSize = value; } }
    public float FoodQuantity { get { return _foodQuantity; } set { _foodQuantity = AdjustToClosestBoundary(value); } }
    public float MultiplyFoodQuantityByFactor { set { _foodQuantity = AdjustToClosestBoundary(value * FoodQuantity); } }

    // Boundary Check
    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxMeatValue) { return _maxMeatValue; }
        else if (value < _minMeatValue) { return _minMeatValue; }
        return value;
    }
}