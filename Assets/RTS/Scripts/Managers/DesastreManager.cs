using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class DesastreManager : MonoBehaviour
{
    //Atributos da classe
    [SerializeField]
    List<GameObject> _atlanticaTiles;
    [SerializeField]
    List<GameObject> _araucariasTiles;
    [SerializeField]
    List<GameObject> _caatingaTiles;
    [SerializeField]
    List<GameObject> _pampaTiles;
    [SerializeField]
    List<GameObject> _pantanalTiles;
    // ReadOnly strings
    private readonly string _atlanticaTileName = "Standard Atlantica";
    private readonly string _araucariasTileName = "Standard Araucarias";
    private readonly string _caatingaTileName = "Standard Caatinga";
    private readonly string _pampaTileName = "Standard Pampa";
    private readonly string _pantanalTileName = "Standard Pantanal";

    private void BuscarTiles()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(_atlanticaTileName))
            {
                _atlanticaTiles.Add(obj);
            }
            else if (obj.name.Contains(_araucariasTileName))
            {
                _araucariasTiles.Add(obj);
            }
            else if (obj.name.Contains(_caatingaTileName))
            {
                _caatingaTiles.Add(obj);
            }
            else if (obj.name.Contains(_pampaTileName))
            {
                _pampaTiles.Add(obj);
            }
            else if (obj.name.Contains(_pantanalTileName))
            {
                _pantanalTiles.Add(obj);
            }
            else
            {
                Debug.Log($"{obj.name} n�o pertence aos tiles especificados.");
            }
        }

        // Para verificar a quantidade de objetos encontrados em cada lista (opcional)
        Debug.Log($"Tiles Atl�ntica: {_atlanticaTiles.Count}");
        Debug.Log($"Tiles Arauc�rias: {_araucariasTiles.Count}");
        Debug.Log($"Tiles Caatinga: {_caatingaTiles.Count}");
        Debug.Log($"Tiles Pampa: {_pampaTiles.Count}");
        Debug.Log($"Tiles Pantanal: {_pantanalTiles.Count}");
    }


    public List<GameObject> SortearBiomaParaDesastre()
    {
        System.Random random = new System.Random();
        int decider = random.Next(0, 5);
        switch (decider)
        { 
            case 0: return _araucariasTiles;
            case 1: return _atlanticaTiles;
            case 2: return _caatingaTiles;
            case 3: return _pantanalTiles;
            default : Debug.Log("Exce��o encontrada no sorteio do bioma");
                return null;
        }      
    }

    public void SortearEventoMenor(List<GameObject> list) 
    {
        System.Random random = new System.Random();
        int decider = random.Next(0, 6);
        switch (decider)
        {
            case 0:
                Migra��o(list);
                break;
            case 1:
                Infesta��o(list);
                break;
            case 2:
                OndaDeCalor(list);
                break;
            case 3:
                FrenteFria(list);
                break;
            case 4:
                Chuvas(list);
                break;
            case 5:
                Secas(list);
                break;
            default:
                Debug.Log("Exce��o encontrada no sorteio do desastre maior");
                break;
        }
    }
    public int SortearEventoMaior(List<GameObject> list) 
    {
        System.Random random = new System.Random();
        int decider = random.Next(0, 4);
        switch (decider)
        {
            case 0:
                Desertifica��o(list);
                break;
            case 1:
                Alagamentos(list);
                break;
            case 2:
                Geadas(list);
                break;
            case 3:
                Tempestades(list);
                break;
            default:
                Debug.Log("Exce��o encontrada no sorteio do desastre maior");
                break;                           
        }
        return decider;
    }

    //EVENTOS MENORES

    //Migra��o - Aumento de 50% a quantidade de pe�as de ca�a na regi�o e de diminui��o de 50% de pe�as de N�o-Carne.
    public void Migra��o(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Totem.GetComponent<FoodTotem>().MultiplyFoodQuantityByFactor = 1.5f;
            list[i].GetComponent<Tile>().Totem.GetComponent<FoodTotem>().MultiplyFoodQuantityByFactor = 0.5f;
        }
    }

    //Infesta��o - Aumento de 50% a quantidade de pe�as de N�o-Carne na regi�o e de diminui��o de 50% de pe�as de ca�a.
    public void Infesta��o(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Totem.GetComponent<FoodTotem>().MultiplyFoodQuantityByFactor = 0.5f;
            list[i].GetComponent<Tile>().Totem.GetComponent<FoodTotem>().MultiplyFoodQuantityByFactor = 1.5f;
        }
    }

    //Onda de Calor - Aumenta em 20% a temperatura dos tiles afetados.
    public void OndaDeCalor(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Temperature.MultiplyTemperatureByValue = 1.2f;
        }
    }

    //Frente Fria - Diminui em 20% a temperatura dos tiles afetados.
    public void FrenteFria(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Temperature.MultiplyTemperatureByValue = 0.8f;
        }
    }

    //Chuvas - Aumenta em 20% a umidade dos tiles afetados.
    public void Chuvas(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Humidity.MultiplyHumidityByValue = 1.2f;
        }
    }

    //Secas - Diminui em 20% a umidade dos tiles afetados.
    public void Secas(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Humidity.MultiplyHumidityByValue = 0.8f;
        }
    }

    //EVENTOS MAIORES

    //Desertifica��o - Aumenta a temperatura em 75% e diminui��o da umidade em 75% dentro dos tiles de transi��o.Transforma a regi�o afetada em uma �rea de Caatinga.
    public void Desertifica��o(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Temperature.MultiplyTemperatureByValue = 1.75f;
            list[i].GetComponent<Tile>().Humidity.MultiplyHumidityByValue = 0.25f;
            list[i].GetComponent<Tile>().IsUnderDesastre = true;
        }
    }



    //Alagamentos - Aumenta a temperatura em 75% e aumento da umidade em 75% dentro dos tiles de transi��o.Transforma a regi�o afetada em uma �rea de Pantanal
    public void Alagamentos(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Temperature.MultiplyTemperatureByValue = 1.75f;
            list[i].GetComponent<Tile>().Humidity.MultiplyHumidityByValue = 1.75f;
            list[i].GetComponent<Tile>().IsUnderDesastre = true;
        }
    }

    //Geadas - Diminui��o da temperatura em 75% e diminui��o da umidade em 75% dentro dos tiles de transi��o.Transforma a regi�o afetada em uma �rea de Mata das Arauc�rias
    public void Geadas(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Temperature.MultiplyTemperatureByValue = 0.25f;
            list[i].GetComponent<Tile>().Humidity.MultiplyHumidityByValue = 0.25f;
            list[i].GetComponent<Tile>().IsUnderDesastre = true;
        }
    }

    //Tempestades - Diminui��o da temperatura em 75% e aumento da umidade em 75% dentro dos tiles de transi��o.Transforma a regi�o afetada em uma �rea de Mata Atl�ntica
    public void Tempestades(List<GameObject> list) 
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].GetComponent<Tile>().Temperature.MultiplyTemperatureByValue = 0.25f;
            list[i].GetComponent<Tile>().Humidity.MultiplyHumidityByValue = 1.75f;
            list[i].GetComponent<Tile>().IsUnderDesastre = true;
        }
    }

    private void Awake()
    {
        BuscarTiles();
    }
}
