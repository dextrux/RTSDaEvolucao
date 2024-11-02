using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
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
    int turnos;
    readonly int _maxTurnos = 15;
    // ReadOnly strings
    private readonly string _atlanticaTileName = "Standard Atlantica";
    private readonly string _araucariasTileName = "Standard Araucarias";
    private readonly string _caatingaTileName = "Standard Caatinga";
    private readonly string _pampaTileName = "Standard Pampa";
    private readonly string _pantanalTileName = "Standard Pantanal";

    private void Start()
    {
        turnos = 1;
    }



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
                Debug.Log($"{obj.name} não pertence aos tiles especificados.");
            }
        }

        // Para verificar a quantidade de objetos encontrados em cada lista (opcional)
        Debug.Log($"Tiles Atlântica: {_atlanticaTiles.Count}");
        Debug.Log($"Tiles Araucárias: {_araucariasTiles.Count}");
        Debug.Log($"Tiles Caatinga: {_caatingaTiles.Count}");
        Debug.Log($"Tiles Pampa: {_pampaTiles.Count}");
        Debug.Log($"Tiles Pantanal: {_pantanalTiles.Count}");
    }


    public List<GameObject> SortearTilesParaAtivarTotens()
    {
        System.Random random = new System.Random();
        List<GameObject> list = new List<GameObject>();
        List<List<GameObject>> biomaTiles = new List<List<GameObject>>
    {
        _araucariasTiles,
        _atlanticaTiles,
        _caatingaTiles,
        _pantanalTiles
    };
        foreach (var tiles in biomaTiles)
        {
            int count = 0;
            while (count < 10)
            {
                int decider = random.Next(0, tiles.Count);
                GameObject tile = tiles[decider];
                if (!list.Contains(tile))
                {
                    list.Add(tile);
                    count++;
                }
            }
        }

        return list;
    }

    public int[] DefinirProporçãoComida()
    { 
        int[] proportion = new int[2];
        System.Random random = new System.Random();
        do
        {
            proportion[0] = random.Next(30,70);
            proportion[1] = random.Next(30,70);
        } while ((proportion[0] + proportion[1]) != 100);
        return proportion;  
    }  
}
