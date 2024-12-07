using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Analytics : MonoBehaviour
{
    public class Jogador
    {
        public float tempoAtePrimeiraCompra = 0;
        public float tempoMaxEntreCompras = 0;
        public int nTotalCompras = 0;
        public int nMutacoesHerb = 0;
        public int nMutacoesCarn = 0;
        public int nPecasMortas = 0;
        public int nPecasEliminadas = 0;
        public int nReproducoes = 0;
        public int nMudouDeBioma = 0;
        public bool foiAtingidoDesastreP = false;
        public bool foiAtingidoCatastrofe = false;
    }

    public Jogador jogador = new Jogador();

    public float tempoTotal = 0f;

    public float tempoAtual = 0f;
    public float tempoCompras = 0f;
    public bool timerAtivo = false;

    private int nMaximoMutacoes1Peca = 0;
    private int nMinimoMutacoes1Peca = 0;

private void Start()
    {
        timerAtivo = false;
    }

    void Update()
    {
        tempoAtual += Time.deltaTime;
    }

    public void SetTempoPrimeiraCompra()
    {
        jogador.tempoAtePrimeiraCompra = tempoAtual;
    }

    public void SetTempoMaxCompras()
    {
        
    }

    string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScn-iboINz7MOOKNJ63QUv87ef1BjUW1s-XNvPuLdqK05B9ww/formResponse";
    public void Send()
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
        form.AddField("entry.2049751604", $"{j.nMutacoesHerb}");

        //Quantidade de mutações carnívoras compradas
        form.AddField("entry.1704193836", $"{j.nMutacoesCarn}");

        //Número de peças mortas do jogador
        form.AddField("entry.345353297", $"{j.nPecasMortas}");

        //Número de peças mortas pelo jogador
        form.AddField("entry.1966054838", $"{j.nPecasEliminadas}");

        //Quantidade de vezes que o jogador reproduziu
        form.AddField("entry.712213778", $"{j}");

        //Tempo total de jogo
        form.AddField("entry.1486274654", $"{tempoTotal}");

        //Quantidade de vezes que um jogador foi para outro bioma
        form.AddField("entry.1825301084", $"{j.nMudouDeBioma}");

        //Número máximo de mutações em uma peça
        form.AddField("entry.877871308", $"{nMaximoMutacoes1Peca}");

        //Número mínimo de mutações em uma peça
        form.AddField("entry.1781269691", $"{nMinimoMutacoes1Peca}");

        //Jogador foi atingido por um desastre pequeno?
        form.AddField("entry.105131229", $"{j.foiAtingidoDesastreP}");

        //Jogador foi atingido por uma catástrofe?
        form.AddField("entry.1756648953", $"{j.foiAtingidoCatastrofe}");



        UnityWebRequest www = UnityWebRequest.Post(URL, form);
        yield return www.SendWebRequest();

    }
}
