using System.Collections.Generic;
using UnityEngine;

public class BiomeDisasterManager : MonoBehaviour
{
    public void SortearEventoMenor(List<GameObject>[] biomesArray)
    {
        System.Random random = new System.Random();
        int decider = random.Next(0, 6);

        switch (decider)
        {
            case 0:
                Debug.Log("Evento menor sorteado: Migração");
                Migracao(biomesArray);
                break;
            case 1:
                Debug.Log("Evento menor sorteado: Infestação");
                Infestacao(biomesArray);
                break;
            case 2:
                Debug.Log("Evento menor sorteado: Onda de Calor");
                OndaDeCalor(biomesArray);
                break;
            case 3:
                Debug.Log("Evento menor sorteado: Frente Fria");
                FrenteFria(biomesArray);
                break;
            case 4:
                Debug.Log("Evento menor sorteado: Chuvas");
                Chuvas(biomesArray);
                break;
            case 5:
                Debug.Log("Evento menor sorteado: Secas");
                Secas(biomesArray);
                break;
            default:
                Debug.LogError("Exceção encontrada no sorteio do desastre menor");
                break;
        }
    }

    public void SortearEventoMaior(List<GameObject>[] biomesArray)
    {
        System.Random random = new System.Random();
        int decider = random.Next(0, 4);

        switch (decider)
        {
            case 0:
                Debug.Log("Evento maior sorteado: Desertificação");
                Desertificacao(biomesArray);
                break;
            case 1:
                Debug.Log("Evento maior sorteado: Alagamentos");
                Alagamentos(biomesArray);
                break;
            case 2:
                Debug.Log("Evento maior sorteado: Geadas");
                Geadas(biomesArray);
                break;
            case 3:
                Debug.Log("Evento maior sorteado: Tempestades");
                Tempestades(biomesArray);
                break;
            default:
                Debug.LogError("Exceção encontrada no sorteio do desastre maior");
                break;
        }
    }
    public void Migracao(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Infestacao(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void OndaDeCalor(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void FrenteFria(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Chuvas(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Secas(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Desertificacao(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Alagamentos(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Geadas(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }

    public void Tempestades(List<GameObject>[] biomesArray)
    {
        foreach (var list in biomesArray)
        {
            foreach (GameObject tile in list)
            {
            }
        }
    }
}
