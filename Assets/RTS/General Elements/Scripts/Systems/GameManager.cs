using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public List<GameObject> ObjetosTocados = new List<GameObject>();
    private void Start()
    {
        FillList();
    }
    public void FillList()
    {
        for (int i = 0; i < 9; i++)
        {
            if (GameObject.Find($"Tile Pampa Variant ({i})"))
            {
                list.Add(GameObject.Find($"Tile Pampa Variant ({i})"));
            }
        }

        for (int i = 0; i < 40; i++)
        {
            if (GameObject.Find($"Tile Atlantica Variant ({i})"))
            {
                list.Add(GameObject.Find($"Tile Atlantica Variant ({i})"));
            }
        }
        for (int i = 0; i < 40; i++)
        {
            if (GameObject.Find($"Tile Araucarias Variant ({i})"))
            {
                list.Add(GameObject.Find($"Tile Araucarias Variant ({i})"));
            }
        }
        for (int i = 0; i < 40; i++)
        {
            if (GameObject.Find($"Tile Pantanal Variant ({i})"))
            {
                list.Add(GameObject.Find($"Tile Pantanal Variant ({i})"));
            }
        }
        for (int i = 0; i < 40; i++)
        {
            if (GameObject.Find($"Tile Caatinga Variant ({i})"))
            {
                list.Add(GameObject.Find($"Tile Caatinga Variant ({i})"));
            }
        }
    }

    public void AddToObjetosTocados(GameObject objectTouched) { ObjetosTocados.Add(objectTouched); }
    public void ClearToObjetosTocados() { ObjetosTocados.Clear(); }
}

