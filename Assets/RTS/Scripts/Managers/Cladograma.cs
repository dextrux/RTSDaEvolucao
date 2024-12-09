using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cladograma : MonoBehaviour
{
    private List<string> cladogramaP1 = new List<string>();
    private List<string> cladogramaP2 = new List<string>();

    public void AddListaClado(Owner owner, string nomeMutacao)
    {
        if(owner.ToString() == "1")
        {
            cladogramaP1.Add(nomeMutacao);
        }
        else
        {
            cladogramaP2.Add(nomeMutacao);
        }
    }

    public void MostraCladoPlayers()
    {
        Debug.Log("Cladograma P1");
        foreach (string nome in cladogramaP1)
        {
            Debug.Log(nome);
        }

        Debug.Log("Cladograma P2");
        foreach (string nome in cladogramaP2)
        {
            Debug.Log(nome);
        }
    }
}
