using ArvoreAVL;
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

    public void SetMinimoMutacoesCriatura(int nMutacoes)
    {
        if (nMutacoes < jogador.nMinimoMutacoes1Peca)
        {
            jogador.nMinimoMutacoes1Peca = nMutacoes;
        }
    }

    public void SetMaximoMutacoesCriatura(ArvoreAVL<MutationBase> mutacoes)
    {
        List<Nodo<MutationBase>> listaMutacoes = mutacoes.Nodos();
        int nMutacoes = listaMutacoes.Count();

        if (nMutacoes > jogador.nMaximoMutacoes1Peca)
        {
            jogador.nMaximoMutacoes1Peca = nMutacoes;
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

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScn-iboINz7MOOKNJ63QUv87ef1BjUW1s-XNvPuLdqK05B9ww/formResponse";
    private void Send()
    {
        StartCoroutine(Post(jogador));
    }

    IEnumerator Post(Jogador j)
    {
        WWWForm form = new WWWForm();

        //Tempo ate primeira compra
        form.AddField("entry.766786925", $"{j.tempoAtePrimeiraCompra}");

        //Tempo maximo entre compras
        form.AddField("entry.87471560", $"{j.tempoMaxEntreCompras}");

        //Quantidade de compras
        form.AddField("entry.1958779428", $"{j.nTotalCompras}");

        //Quantidade de mutações herbívoras compradas
        form.AddField("entry.2049751604", $"{j.nMutacoesHerb}"); //falta

        //Quantidade de mutações carnívoras compradas
        form.AddField("entry.1704193836", $"{j.nMutacoesCarn}"); //falta

        //Número de peças mortas do jogador
        form.AddField("entry.345353297", $"{j.nPecasMortas}");

        //Número de peças mortas pelo jogador
        form.AddField("entry.1966054838", $"{j.nPecasEliminadas}");

        //Quantidade de vezes que o jogador reproduziu
        form.AddField("entry.712213778", $"{j.nReproducoes}");

        //Tempo total de jogo
        form.AddField("entry.1486274654", $"{tempoTotal}");

        //Quantidade de vezes que um jogador foi para outro bioma
        form.AddField("entry.1825301084", $"{j.nMudouDeBioma}");

        //Número máximo de mutações em uma peça
        form.AddField("entry.877871308", $"{j.nMaximoMutacoes1Peca}");

        //Número mínimo de mutações em uma peça
        form.AddField("entry.1781269691", $"{j.nMinimoMutacoes1Peca}");

        //Jogador foi atingido por um desastre pequeno?
        if (j.foiAtingidoDesastreP)
        {
            form.AddField("entry.105131229", "Sim");
        }
        else
        {
            form.AddField("entry.105131229", "Não");
        }

        //Jogador foi atingido por uma catástrofe?
        if (j.foiAtingidoCatastrofe)
        {
            form.AddField("entry.1756648953", "Sim");
        }
        else
        {
            form.AddField("entry.1756648953", "Não");
        }



        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

    }
}
