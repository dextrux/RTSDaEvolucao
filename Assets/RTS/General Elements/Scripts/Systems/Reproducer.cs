using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reproducer : MonoBehaviour
{
    GameObject prefabSon;
    void ReproducePiece(Jubileuson father, Jubileuson mother, Tile currentTile)
    {
        GameObject newPiece = Instantiate(prefabSon, this.transform.position, Quaternion.identity);
        Jubileuson son = newPiece.GetComponent<Jubileuson>();
        //Temperature sonTemperature = son.GetTemperature();
       // sonTemperature.SetAllTemperatureValues(2 * father.GetTemperature().GetIdealTemperatureValue() + 2 * mother.GetTemperature().GetIdealTemperatureValue() + currentTile.GetTemperature().GetCurrentTemperatureValue() / 5, currentTile.GetTemperature().GetCurrentTemperatureValue());
       // Humidity sonHumidity = son.GetHumidity();
       // sonHumidity.SetAllHumidityValues(2 * father.GetHumidity().GetIdealHumidityValue() + 2 * mother.GetHumidity().GetIdealHumidityValue() + currentTile.GetHumidity().GetCurrentHumidityValue() / 5, currentTile.GetHumidity().GetCurrentHumidityValue());
        StatusBar sonFertilityBar = son.GetFertilityBar();
        sonFertilityBar.SetNewBarValue(0);
        StatusBar sonHealthBar = son.GetHealthBar();
        //Altere para contar com as mutações
        sonHealthBar.SetNewBarValue(sonHealthBar.GetBarMaxValue());
        StatusBar sonStrenghtBar = son.GetStrenghtBar();
        sonStrenghtBar.SetNewBarValue(20);
        StatusBar sonEnergyBar = son.GetEnergyBar();
        sonEnergyBar.SetNewBarValue(100);
    }
}
/**/
