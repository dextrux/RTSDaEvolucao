using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurn : MonoBehaviour
{
    private int DesastreCounter = 1;
    private int GrandeDesastreCounter = 1;
    public GameObject player;
    public GameObject EventSystem;
    Jubileuson jubPlayer;
    EventManager EM;

    private void Start()
    {
        jubPlayer = player.GetComponent<Jubileuson>();
        EM = EventSystem.GetComponent<EventManager>();
    }

    //Funções que serão chamadas quando o player passar o turno apertando o botão
    public void NextTurnButton() 
    {
        PlayerStaminaCalculator();
        DesastreCheck();
    }

    //Restora ações do player depois de passar o turno
    private void PlayerStaminaCalculator()
    {
        jubPlayer._pieceRemainingActions += 3; //Exemplo, mudar pra quantidade certa depois
    }

    //Adiciona +1 nos contadores dos desastres e checa se um desastre vai acontecer
    private void DesastreCheck()
    {
        DesastreCounter += 1;
        GrandeDesastreCounter += 1;
        if (GrandeDesastreCounter >= 15)
        {
            //Grande Desastre
            GrandeDesastreCounter = 0;
        }
        else if (DesastreCounter >= 5)
        {
            DesastreCounter = 0;
            EscolheDesastre();
        }
    }

    private void EscolheDesastre()
    {
        int n = Random.Range(1, 5);

        switch (n)
        {
            case 1:
                EM.DeastreChuvas();
                break;
            case 2:
                EM.DesastreAlagamento();
                break;
            case 3:
                EM.DesastreDesertificação();
                break;
            case 4:
                EM.DesastreFrenteFria();
                break;
        }

    }
}
