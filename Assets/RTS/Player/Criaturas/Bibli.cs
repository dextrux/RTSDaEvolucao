using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bibli : MonoBehaviour
{

    public string _name;
    public int _health;
    public float _weight;
    public float _height;
    public float _idealTemperature;
    public float _temperature;
    public float _idealHumidity;
    public float _dietTendency;
    public float _fertilityBar;

    public BibliDietStatus _currentBibliDiet;
    public BibliFertilityStatus _currentBibliFertilityStatus;
    public BibliHealthStatus _currentBibliHealthStatus;
    public BibliTemperatureStatus _currentBibliTemperatureStatus;
    public BibliLevelStatus _currentBibliLevelStatus;

    public void Update()
    {
        BibliStatusUpdater();
    }
    public void DietaUpdater() 
    { 
        if(_dietTendency < 45)
        {
            _currentBibliDiet = BibliDietStatus.Carnívoro;
        }
        else if(_dietTendency > 55)
        {
            _currentBibliDiet = BibliDietStatus.Herbívoro;
        }
        else
        {
            _currentBibliDiet = BibliDietStatus.Onívoro;
        }
    }
    public void ReproduçãoUpdater() 
    { 
        if(_fertilityBar < 100)
        {
            _currentBibliFertilityStatus = BibliFertilityStatus.Despreparado;
        }
        else
        {
            _currentBibliFertilityStatus = BibliFertilityStatus.Pronto;
        }
    }
    public void SaudeUpdater() 
    { 
        if(_health < 50)
        {
            _currentBibliHealthStatus = BibliHealthStatus.Doente;
        }
        else
        {
            _currentBibliHealthStatus = BibliHealthStatus.Saudavel;
        }
    }
    public void TemperatureUpdater() 
    { 
        if(_idealTemperature > _temperature + 15 || _idealTemperature < _temperature - 15)
        {
            if (_idealTemperature > _temperature + 15)
            {
                _currentBibliTemperatureStatus = BibliTemperatureStatus.Calor;
            }
            else if (_idealTemperature < _temperature - 15)
            {
                _currentBibliTemperatureStatus = BibliTemperatureStatus.Frio;
            }
        }
        else
        {
            _currentBibliTemperatureStatus = BibliTemperatureStatus.Neutro;
        }
    }

    public void BibliStatusUpdater()
    {
        ReproduçãoUpdater();
        SaudeUpdater();
        TemperatureUpdater();
    }

    // Get/Set para _name
    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    // Get/Set para _weight
    public float GetWeight()
    {
        return _weight;
    }

    public string GetWeightAsString()
    {
        return _weight.ToString();
    }

    public void SetWeight(float weight)
    {
        _weight = weight;
    }

    // Get/Set para _height
    public float GetHeight()
    {
        return _height;
    }

    public string GetHeightAsString()
    {
        return _height.ToString();
    }

    public void SetHeight(float height)
    {
        _height = height;
    }



    // Get/Set para _idealTemperature
    public float GetIdealTemperature()
    {
        return _idealTemperature;
    }

    public string GetIdealTemperatureAsString()
    {
        return _idealTemperature.ToString();
    }

    public void SetIdealTemperature(float idealTemperature)
    {
        _idealTemperature = idealTemperature;
    }

    // Get/Set para _idealHumidity
    public float GetIdealHumidity()
    {
        return _idealHumidity;
    }

    public string GetIdealHumidityAsString()
    {
        return _idealHumidity.ToString();
    }

    public void SetIdealHumidity(float idealHumidity)
    {
        _idealHumidity = idealHumidity;
    }

    // Get/Set para _dietTendency
    public float GetDietTendency()
    {
        return _dietTendency; ;
    }

    public string GetDietTendencyAsString()
    {
        return _dietTendency.ToString();
    }

    public void SetDietTendency(float dietTendency)
    {
        _dietTendency = dietTendency;
    }

    // Get/Set para _currentBibliLevelStatus

    public BibliLevelStatus GetCurrentBibliLevel()
    {
        return _currentBibliLevelStatus;
    }

    public string GetCurrentBibliLevelAsString()
    {
        return _currentBibliLevelStatus.ToString();
    }

    public void SetCurrentBibliLevelStatus(BibliLevelStatus currentState)
    {
        _currentBibliLevelStatus = currentState;
    }
    // Get/Set para _health
    public int GetHealth()
    {
        return _health;
    }

    public string GetHealthAsString()
    {
        return _health.ToString();
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    // Get/Set para _temperature
    public float GetTemperature()
    {
        return _temperature;
    }

    public string GetTemperatureAsString()
    {
        return _temperature.ToString();
    }

    public void SetTemperature(float temperature)
    {
        _temperature = temperature;
    }

    // Get/Set para _fertilityBar
    public float GetFertilityBar()
    {
        return _fertilityBar;
    }

    public string GetFertilityBarAsString()
    {
        return _fertilityBar.ToString();
    }

    public void SetFertilityBar(float fertilityBar)
    {
        _fertilityBar = fertilityBar;
    }

    // Get/Set para _currentBibliDiet
    public BibliDietStatus GetCurrentBibliDiet()
    {
        return _currentBibliDiet;
    }

    public string GetCurrentBibliDietAsString()
    {
        return _currentBibliDiet.ToString();
    }

    public void SetCurrentBibliDiet()
    {

    }

    // Get/Set para _currentBibliFertilityStatus
    public BibliFertilityStatus GetCurrentBibliFertilityStatus()
    {
        return _currentBibliFertilityStatus;
    }

    public string GetCurrentBibliFertilityStatusAsString()
    {
        return _currentBibliFertilityStatus.ToString();
    }

    public void SetCurrentBibliFertilityStatus(BibliFertilityStatus currentBibliFertilityStatus)
    {
        _currentBibliFertilityStatus = currentBibliFertilityStatus;
    }

    // Get/Set para _currentBibliHealthStatus
    public BibliHealthStatus GetCurrentBibliHealthStatus()
    {
        return _currentBibliHealthStatus;
    }

    public string GetCurrentBibliHealthStatusAsString()
    {
        return _currentBibliHealthStatus.ToString();
    }

    public void SetCurrentBibliHealthStatus(BibliHealthStatus currentBibliHealthStatus)
    {
        _currentBibliHealthStatus = currentBibliHealthStatus;
    }

    // Get/Set para _currentBibliTemperatureStatus
    public BibliTemperatureStatus GetCurrentBibliTemperatureStatus()
    {
        return _currentBibliTemperatureStatus;
    }

    public string GetCurrentBibliTemperatureStatusAsString()
    {
        return _currentBibliTemperatureStatus.ToString();
    }

    public void SetCurrentBibliTemperatureStatus(BibliTemperatureStatus currentBibliTemperatureStatus)
    {
        _currentBibliTemperatureStatus = currentBibliTemperatureStatus;
    }

}
