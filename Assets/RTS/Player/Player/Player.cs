using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 500f)]
    private int _totalPopulation;
    [SerializeField]
    private int _mutagenPoints;

    public void SetTotalPopulation(int totalPopulation)
    {
        _totalPopulation = totalPopulation;
    }

    public int GetTotalPopulation()
    {
        return _totalPopulation;
    }

    public void SetMutagenPoints(int mutagenPoints)
    {
        _mutagenPoints = mutagenPoints;
    }

    public int GetMutagenPoints()
    {
        return _mutagenPoints;
    }
}
