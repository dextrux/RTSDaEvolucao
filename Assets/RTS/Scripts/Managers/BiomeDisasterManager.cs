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
        List<Totem> totensAtivosCarne = new List<Totem>();
        List<Totem> totensAtivosVegano = new List<Totem>();
        List<Totem> totensInativos = new List<Totem>();
        System.Random r = new System.Random();
        foreach (var biome in biomesArray)
        {
            Totem totem = biome.GetComponent<Tile>().Totem.GetComponent<Totem>();
            if (totem.isActiveAndEnabled)
            {
                //Carne
                if ((int)totem.TotemType < 3)
                {
                    totensAtivosCarne.Add(totem);
                }
                //Vegano
                else if((int)totem.TotemType > 3 && (int)totem.TotemType < 6)
                {
                    totensAtivosVegano.Add(totem);
                }            
            }
            else
            {
                totensInativos.Add(totem);
            }         
        }

        if (totensInativos.Count >= totensAtivosCarne.Count * 1.5f)
        {
            for (int i = 0; i < (totensAtivosCarne.Count * 1.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(1, 3);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }

        if (totensInativos.Count >= totensAtivosVegano.Count * 0.5f)
        {
            for (int i = 0; i < (totensAtivosVegano.Count * 0.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(3, 6);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }
    }

    public static void ReverterMigracao(List<GameObject> biomesArray)
    {
        List<Totem> totensAtivosCarne = new List<Totem>();
        List<Totem> totensAtivosVegano = new List<Totem>();
        List<Totem> totensInativos = new List<Totem>();
        System.Random r = new System.Random();
        foreach (var biome in biomesArray)
        {
            Totem totem = biome.GetComponent<Tile>().Totem.GetComponent<Totem>();
            if (totem.isActiveAndEnabled)
            {
                //Carne
                if ((int)totem.TotemType < 3)
                {
                    totensAtivosCarne.Add(totem);
                }
                //Vegano
                else if ((int)totem.TotemType > 3 && (int)totem.TotemType < 6)
                {
                    totensAtivosVegano.Add(totem);
                }
            }
            else
            {
                totensInativos.Add(totem);
            }
        }

        if (totensInativos.Count >= totensAtivosCarne.Count * 0.5f)
        {
            for (int i = 0; i < (totensAtivosCarne.Count * 0.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(1, 3);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }

        if (totensInativos.Count >= totensAtivosVegano.Count * 1.5f)
        {
            for (int i = 0; i < (totensAtivosVegano.Count * 1.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(3, 6);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }
    }

    public static void Infestacao(List<GameObject> biomesArray)
    {
        List<Totem> totensAtivosCarne = new List<Totem>();
        List<Totem> totensAtivosVegano = new List<Totem>();
        List<Totem> totensInativos = new List<Totem>();
        System.Random r = new System.Random();
        foreach (var biome in biomesArray)
        {
            Totem totem = biome.GetComponent<Tile>().Totem.GetComponent<Totem>();
            if (totem.isActiveAndEnabled)
            {
                //Carne
                if ((int)totem.TotemType < 3)
                {
                    totensAtivosCarne.Add(totem);
                }
                //Vegano
                else if ((int)totem.TotemType > 3 && (int)totem.TotemType < 6)
                {
                    totensAtivosVegano.Add(totem);
                }
            }
            else
            {
                totensInativos.Add(totem);
            }
        }

        if (totensInativos.Count >= totensAtivosCarne.Count * 0.5f)
        {
            for (int i = 0; i < (totensAtivosCarne.Count * 0.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(1, 3);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }

        if (totensInativos.Count >= totensAtivosVegano.Count * 1.5f)
        {
            for (int i = 0; i < (totensAtivosVegano.Count * 1.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(3, 6);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }
    }

    public static void ReverterInfestacao(List<GameObject> biomesArray)
    {
        List<Totem> totensAtivosCarne = new List<Totem>();
        List<Totem> totensAtivosVegano = new List<Totem>();
        List<Totem> totensInativos = new List<Totem>();
        System.Random r = new System.Random();
        foreach (var biome in biomesArray)
        {
            Totem totem = biome.GetComponent<Tile>().Totem.GetComponent<Totem>();
            if (totem.isActiveAndEnabled)
            {
                //Carne
                if ((int)totem.TotemType < 3)
                {
                    totensAtivosCarne.Add(totem);
                }
                //Vegano
                else if ((int)totem.TotemType > 3 && (int)totem.TotemType < 6)
                {
                    totensAtivosVegano.Add(totem);
                }
            }
            else
            {
                totensInativos.Add(totem);
            }
        }

        if (totensInativos.Count >= totensAtivosCarne.Count * 1.5f)
        {
            for (int i = 0; i < (totensAtivosCarne.Count * 1.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(1, 3);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }

        if (totensInativos.Count >= totensAtivosVegano.Count * 0.5f)
        {
            for (int i = 0; i < (totensAtivosVegano.Count * 0.5f); i++)
            {
                TotemType totemType = (TotemType)r.Next(3, 6);
                Totem newTotem = totensInativos[r.Next(0, totensInativos.Count)];
                newTotem.ActivateTotem(totemType);
                totensInativos.Remove(newTotem);
            }
        }
    }

    public static void OndaDeCalor(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 1.20f;
        }
    }

    public static void ReverterOndaDeCalor(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 0.80f;
        }
    }

    public static void FrenteFria(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 0.8f;
        }
    }

    public static void ReverterFrenteFria(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 1.2f;
        }
    }

    public static void Chuvas(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 1.20f;
        }
    }

    public static void ReverterChuvas(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 0.8f;
        }
    }

    public static void Secas(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 0.8f;
        }
    }

    public static void ReverterSecas(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 1.2f;
        }
    }

    #endregion

    #region Métodos de Eventos Maiores

    public static void Desertificacao(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 1.75f;
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 0.25f;
        }
    }

    public static void ReverterDesertificacao(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().TransformarTile(Biome.Caatinga, biome);
        }
    }

    public static void Alagamentos(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 1.75f;
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 1.75f;
        }
    }

    public static void ReverterAlagamentos(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().TransformarTile(Biome.Pantanal, biome);
        }
    }

    public static void Geadas(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 0.25f;
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 0.25f;
        }
    }

    public static void ReverterGeadas(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().TransformarTile(Biome.Mata_das_Araucarias, biome);
        }
    }

    public static void Tempestades(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().Temperature.CurrentValue *= 0.25f;
            biome.GetComponent<Tile>().Humidity.CurrentValue *= 1.75f;
        }
    }

    public static void ReverterTempestades(List<GameObject> biomesArray)
    {
        foreach (var biome in biomesArray)
        {
            biome.GetComponent<Tile>().TransformarTile(Biome.Mata_Atlantica, biome);
        }
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
