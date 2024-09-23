using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Atributos de não-carne
    NonMeatFood nonMeatFood = new NonMeatFood();

    // Get's - NonMeatFood
    public float GetFruitsValue() { return this.nonMeatFood.GetFruitsValue(); }
    public float GetGrainsValue() { return this.nonMeatFood.GetGrainsValue(); }
    public float GetPlantsValue() { return this.nonMeatFood.GetPlantsValue(); }

    // String Get's - NonMeatFood
    public string GetFruitsAsString() { return this.nonMeatFood.GetFruitsAsString(); }
    public string GetGrainsAsString() { return this.nonMeatFood.GetGrainsAsString(); }
    public string GetPlantsAsString() { return this.nonMeatFood.GetPlantsAsString(); }

    // Set's - NonMeatFood
    public void SetAllNonMeatValues(float fruits, float grains, float plants)
    {
        nonMeatFood.SetAllNonMeatFoodValues(fruits, grains, plants);
    }

    public void SetFruitsValue(float fruits)
    {
        this.nonMeatFood.SetFruitsValue(fruits);
    }

    public void SetGrainsValue(float grains)
    {
        this.nonMeatFood.SetGrainsValue(grains);
    }

    public void SetPlantsValue(float plants)
    {
        this.nonMeatFood.SetPlantsValue(plants);
    }

    // Event's Set's - NonMeatFood
    public void SetNewValueToAllNonMeatFoodAtributesByFactor(float factor)
    {
        this.nonMeatFood.SetNewValueToAllNonMeatFoodAtributesByFactor(factor);
    }

    // Atributos de carne
    MeatFood meatFood = new MeatFood();

    // Get's - MeatFood
    public float GetSmallSizeHuntValue() { return this.meatFood.GetSmallSizeHuntValue(); }
    public float GetMidSizeHuntValue() { return this.meatFood.GetMidSizeHuntValue(); }
    public float GetLargeSizeHuntValue() { return this.meatFood.GetLargeSizeHuntValue(); }
    public float GetCorpsesHuntValue() { return this.meatFood.GetCorpsesHuntValue(); }

    // String Get's - MeatFood
    public string GetSmallSizeHuntAsString() { return this.meatFood.GetSmallSizeHuntAsString(); }
    public string GetMidSizeHuntAsString() { return this.meatFood.GetMidSizeHuntAsString(); }
    public string GetLargeSizeHuntAsString() { return this.meatFood.GetLargeSizeHuntAsString(); }
    public string GetCorpsesHuntAsString() { return this.meatFood.GetCorpsesHuntAsString(); }

    // Set's - MeatFood
    public void SetAllMeatValues(float smallHunt, float midHunt, float largeHunt, float corpsesHunt)
    {
        meatFood.SetAllHuntValues(smallHunt, midHunt, largeHunt, corpsesHunt);
    }

    public void SetSmallSizeHuntValue(float smallHunt)
    {
        this.meatFood.SetSmallSizeHuntValue(smallHunt);
    }

    public void SetMidSizeHuntValue(float midHunt)
    {
        this.meatFood.SetMidSizeHuntValue(midHunt);
    }

    public void SetLargeSizeHuntValue(float largeHunt)
    {
        this.meatFood.SetLargeSizeHuntValue(largeHunt);
    }

    public void SetCorpsesHuntValue(float corpsesHunt)
    {
        this.meatFood.SetCorpsesHuntValue(corpsesHunt);
    }

    // Event's Set's - MeatFood
    public void SetNewValueToAllMeatFoodAtributesByFactor(float factor)
    {
        this.meatFood.SetNewValueToAllMeatFoodAtributesByFactor(factor);
    }
}
