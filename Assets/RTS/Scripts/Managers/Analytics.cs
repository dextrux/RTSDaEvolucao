using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    public class Jogador
    {
        public float tempoAtePrimeiraCompra = 0f;
        public float tempoMaxEntreCompras = 0f;
        public int nTotalCompras = 0;
        public int nMutacoesHerb = 0;
        public int nMutacoesCarn = 0;
        public int nPecasMortas = 0;
        public int nPecasEliminadas = 0;
        public int nReproducoes = 0;
        public int nMudouDeBioma = 0;
        public int nMaximoMutacoes1Peca = 0;
        public int nMinimoMutacoes1Peca = 0;
        public bool foiAtingidoDesastreP = false;
        public bool foiAtingidoCatastrofe = false;
    }

    private Jogador jogador = new Jogador();

    private float tempoTotal = 0f;

    private float tempoAtual = 0f;
    private float tempoCompras = 0f;
    private bool timerAtivoCompras;
    private bool timerAtivoTotal;
    private bool timerPriCompraAtivo;

    private string biomaAtual;

    private void Start()
    {
        timerAtivoCompras = false;
        timerAtivoTotal = true;
        tempoCompras = 0f;
        tempoAtual = 0f;
        timerPriCompraAtivo = true;
    }

    void Update()
    {
        if (timerAtivoCompras)
        {
            tempoCompras += Time.deltaTime;
        }

        if (timerAtivoTotal)
        {
            tempoAtual += Time.deltaTime;
        }
    }

    public void AcabouJogo()
    {
        ParaTimerTotal();
        Send();
    }

    public void SetNumeroTotalCompras()
    {
        jogador.nTotalCompras++;
    }

    public void SetMinimoMutacoesCriatura(List<MutationBase> mutacoes)
    {
        int nMutacoes = mutacoes.Count();

        if (nMutacoes < jogador.nMinimoMutacoes1Peca)
        {
            jogador.nMinimoMutacoes1Peca = nMutacoes;
        }
    }

    public void SetMaximoMutacoesCriatura(List<MutationBase> mutacoes)
    {
        int Mutacoes = mutacoes.Count();

        if (Mutacoes > jogador.nMaximoMutacoes1Peca)
        {
            jogador.nMaximoMutacoes1Peca = Mutacoes;
        }
    }

    public void SetTempoPrimeiraCompra()
    {
        if (timerPriCompraAtivo)
        {
            jogador.tempoAtePrimeiraCompra = tempoAtual;
            timerPriCompraAtivo = false;
        }
    }

    public void ParaTimerTotal()
    {
        timerAtivoTotal = false;
        timerAtivoCompras = false;
        timerPriCompraAtivo = false;
        tempoTotal = tempoAtual;
    }

    public void SetNumeroMudouBioma(Tile tileAtual)
    {
        if (biomaAtual == null)
        {
            biomaAtual = tileAtual.biome.ToString();
        }
        else if (biomaAtual != tileAtual.biome.ToString())
        {
            jogador.nMudouDeBioma++;
        }
    }

    public void SetTempoMaxCompras()
    {
        if (timerAtivoCompras == false)
        {
            timerAtivoCompras = true;
        }
        else
        {
            timerAtivoCompras = false;
            if (tempoCompras > jogador.tempoMaxEntreCompras)
            {
                jogador.tempoMaxEntreCompras = tempoCompras;
            }
        }
    }

    public void SetPecasMortas()
    {
        jogador.nPecasMortas++;
    }

    public void SetPecasEliminadas()
    {
        jogador.nPecasEliminadas++;
    }

    public void SetNumeroReproducoes()
    {
        jogador.nReproducoes++;
    }

    public void SetNumeroMutacoesHerbCarn(string nomeMutacao)
    {
        //switch carnivoras
        switch (nomeMutacao)
        {
            case "Canibal":
                jogador.nMutacoesCarn++;
                break;
            case "Necr�fago":
                jogador.nMutacoesCarn++;
                break;
            case "Parasita":
                jogador.nMutacoesCarn++;
                break;
            case "Bico Carn�voro I":
                jogador.nMutacoesCarn++;
                break;
            case "Bico Carn�voro II":
                jogador.nMutacoesCarn++;
                break;
            case "Bico Carn�voro III":
                jogador.nMutacoesCarn++;
                break;
            case "Dentes Afiados":
                jogador.nMutacoesCarn++;
                break;
            case "Quel�cera":
                jogador.nMutacoesCarn++;
                break;
            case "Pata de Urso":
                jogador.nMutacoesCarn++;
                break;
            case "Cauda de Gato Selvagem":
                jogador.nMutacoesCarn++;
                break;
            case "Boca de Ca�a Pequena":
                jogador.nMutacoesCarn++;
                break;
            case "Boca de Ca�a M�dia":
                jogador.nMutacoesCarn++;
                break;
            case "Boca de Ca�a Grande":
                jogador.nMutacoesCarn++;
                break;
            case "Bico de Ca�a Pequena":
                jogador.nMutacoesCarn++;
                break;
            case "Bico de Ca�a M�dia":
                jogador.nMutacoesCarn++;
                break;
            case "Bico de Ca�a Grande":
                jogador.nMutacoesCarn++;
                break;
        }

        //switch herbivoras
        switch (nomeMutacao)
        {
            case "Bico Herb�voro I":
                jogador.nMutacoesHerb++;
                break;
            case "Bico Herb�voro II":
                jogador.nMutacoesHerb++;
                break;
            case "Bico Herb�voro III":
                jogador.nMutacoesHerb++;
                break;
            case "Dentes Quadrados":
                jogador.nMutacoesHerb++;
                break;
            case "Cauda Felpuda":
                jogador.nMutacoesHerb++;
                break;
            case "Cauda Felpuda Pequena":
                jogador.nMutacoesHerb++;
                break;
            case "Bico Tritura Nozes":
                jogador.nMutacoesHerb++;
                break;
            case "Bico de Frutas":
                jogador.nMutacoesHerb++;
                break;
            case "Dentes Tritura Nozes":
                jogador.nMutacoesHerb++;
                break;
            case "Dentes de Frutas":
                jogador.nMutacoesHerb++;
                break;
        }
    }

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScn-iboINz7MOOKNJ63QUv87ef1BjUW1s-XNvPuLdqK05B9ww/formResponse";
    private void Send()
    {
        StartCoroutine(Post(jogador));
    }

    IEnumerator Post(Jogador j)
    {
        WWWForm form = new WWWForm();

        //Tempo ate primeira compra
        if(j.tempoAtePrimeiraCompra == 0)
        {
            form.AddField("entry.766786925", "N�o houve compra");
        }
        else
        {
            form.AddField("entry.766786925", $"{j.tempoAtePrimeiraCompra}");
        }

        //Tempo maximo entre compras
        form.AddField("entry.87471560", $"{j.tempoMaxEntreCompras}");

        //Quantidade de compras
        form.AddField("entry.1958779428", $"{j.nTotalCompras}");

        //Quantidade de muta��es herb�voras compradas
        form.AddField("entry.2049751604", $"{j.nMutacoesHerb}"); //falta

        //Quantidade de muta��es carn�voras compradas
        form.AddField("entry.1704193836", $"{j.nMutacoesCarn}"); //falta

        //N�mero de pe�as mortas do jogador
        form.AddField("entry.345353297", $"{j.nPecasMortas}");

        //N�mero de pe�as mortas pelo jogador
        form.AddField("entry.1966054838", $"{j.nPecasEliminadas}");

        //Quantidade de vezes que o jogador reproduziu
        form.AddField("entry.712213778", $"{j.nReproducoes}");

        //Tempo total de jogo
        form.AddField("entry.1486274654", $"{tempoTotal}");

        //Quantidade de vezes que um jogador foi para outro bioma
        form.AddField("entry.1825301084", $"{j.nMudouDeBioma}");

        //N�mero m�ximo de muta��es em uma pe�a
        form.AddField("entry.877871308", $"{j.nMaximoMutacoes1Peca}");

        //N�mero m�nimo de muta��es em uma pe�a
        form.AddField("entry.1781269691", $"{j.nMinimoMutacoes1Peca}");

        //Jogador foi atingido por um desastre pequeno?
        if (j.foiAtingidoDesastreP)
        {
            form.AddField("entry.105131229", "Sim");
        }
        else
        {
            form.AddField("entry.105131229", "N�o");
        }

        //Jogador foi atingido por uma cat�strofe?
        if (j.foiAtingidoCatastrofe)
        {
            form.AddField("entry.1756648953", "Sim");
        }
        else
        {
            form.AddField("entry.1756648953", "N�o");
        }



        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

    }
}
