using System.Collections.Generic;
using UnityEngine;

public class BiomeDisasterManager : MonoBehaviour
{
    // Instância de Random compartilhada
    private static System.Random _random = new System.Random();

    #region Sorteio de Eventos

    /// <summary>
    /// Sorteia e aplica um evento menor em biomas.
    /// </summary>
    public static int SortearEventoMenor(List<GameObject> biomesArray)
    {
        int decider = _random.Next(0, 6);

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
                Debug.LogError("Erro no sorteio do desastre menor.");
                break;
        }
        return decider;
    }

    public static void ReverterEventoMenor(List<GameObject> biomesArray, int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Evento menor revertido: Migração");
                ReverterMigracao(biomesArray);
                break;
            case 1:
                Debug.Log("Evento menor revertido: Infestação");
                ReverterInfestacao(biomesArray);
                break;
            case 2:
                Debug.Log("Evento menor revertido: Onda de Calor");
                ReverterOndaDeCalor(biomesArray);
                break;
            case 3:
                Debug.Log("Evento menor revertido: Frente Fria");
                ReverterFrenteFria(biomesArray);
                break;
            case 4:
                Debug.Log("Evento menor revertido: Chuvas");
                ReverterChuvas(biomesArray);
                break;
            case 5:
                Debug.Log("Evento menor revertido: Secas");
                ReverterSecas(biomesArray);
                break;
            default:
                Debug.LogError("Erro na reversão do desastre menor.");
                break;
        }
    }

    /// <summary>
    /// Sorteia e aplica um evento maior em biomas.
    /// </summary>
    public static int SortearEventoMaior(List<GameObject> biomesArray)
    {
        int decider = _random.Next(0, 4);

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
                Debug.LogError("Erro no sorteio do desastre maior.");
                break;
        }
        return decider;
    }

    public static void ReverterEventoMaior(List<GameObject> biomesArray, int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Evento maior revertido: Desertificação");
                ReverterDesertificacao(biomesArray);
                break;
            case 1:
                Debug.Log("Evento maior revertido: Alagamentos");
                ReverterAlagamentos(biomesArray);
                break;
            case 2:
                Debug.Log("Evento maior revertido: Geadas");
                ReverterGeadas(biomesArray);
                break;
            case 3:
                Debug.Log("Evento maior revertido: Tempestades");
                ReverterTempestades(biomesArray);
                break;
            default:
                Debug.LogError("Erro na reversão desastre maior.");
                break;
        }
    }

    #endregion

    #region Aplicação de Biomas Pós-Desastre

    /// <summary>
    /// Aplica o bioma resultante de um desastre maior.
    /// </summary>
    public static void AplicarNovoBiomaDesastreMaior(List<GameObject> biomesArray, int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Novo bioma após Desertificação aplicado.");
                break;
            case 1:
                Debug.Log("Novo bioma após Alagamentos aplicado.");
                break;
            case 2:
                Debug.Log("Novo bioma após Geadas aplicado.");
                break;
            case 3:
                Debug.Log("Novo bioma após Tempestades aplicado.");
                break;
            default:
                Debug.LogError("Erro na aplicação do bioma pós-desastre.");
                break;
        }
    }

    #endregion

    #region Métodos de Eventos Menores

    public static void Migracao(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de migração
    }

    public static void ReverterMigracao(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter migração
    }

    public static void Infestacao(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de infestação
    }

    public static void ReverterInfestacao(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter infestação
    }

    public static void OndaDeCalor(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de onda de calor
    }

    public static void ReverterOndaDeCalor(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter onda de calor
    }

    public static void FrenteFria(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de frente fria
    }

    public static void ReverterFrenteFria(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter frente fria
    }

    public static void Chuvas(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de chuvas
    }

    public static void ReverterChuvas(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter chuvas
    }

    public static void Secas(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de secas
    }

    public static void ReverterSecas(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter secas
    }

    #endregion

    #region Métodos de Eventos Maiores

    public static void Desertificacao(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de desertificação
    }

    public static void ReverterDesertificacao(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter desertificação
    }

    public static void Alagamentos(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de alagamentos
    }

    public static void ReverterAlagamentos(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter alagamentos
    }

    public static void Geadas(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de geadas
    }

    public static void ReverterGeadas(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter geadas
    }

    public static void Tempestades(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica de tempestades
    }

    public static void ReverterTempestades(List<GameObject> biomesArray)
    {
        // TODO: Implementar lógica para reverter tempestades
    }

    #endregion

    #region Finalização de Desastres

    /// <summary>
    /// Finaliza um desastre em uma lista de tiles, aplicando um novo bioma.
    /// </summary>
    public static void AcabarDesastre(List<GameObject> tiles, bool desastreMaior, int index)
    {
        if (!desastreMaior)
        {
            ReverterEventoMenor(tiles, index);
        }
        else if (desastreMaior)
        {
            ReverterEventoMaior(tiles, index);
        }
    }

    #endregion
}
