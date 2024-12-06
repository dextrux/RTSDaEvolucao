using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    #region Constantes para mudar a cor da Skybox
    public Material skyboxMaterial; // Material do skybox
    public Color corDia; // Primeira cor
    public Color corPdS; // Segunda cor
    public Color corNoite; // Terceira cor
    private Color startColor; // Cor inicial da transicao
    private Color endColor; // Cor final da transicao

    private bool transitioning = false;
    public float transitionDuration = 2f; // Duração da transição em segundos
    private float time = 0f;
    private float contadorTurno = 0;
    #endregion

    //Variavel que define se está de dia ou de noite
    public string DayNight;

    private void Start()
    {
        DayNight = "Dia";
        transitioning = false;
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            skyboxMaterial.SetColor("_Tint", corDia);
        }
    }

    private void Update()
    {
        if (transitioning == true)
        {
            time += Time.deltaTime;

            Color currentColor = Color.Lerp(startColor, endColor, time);
            skyboxMaterial.SetColor("_Tint", currentColor);

            if (time >= transitionDuration)
            {
                transitioning = false;
            }
        }
    }

    public void ChangeDayNight()
    {
        Debug.Log("Dia&Noite chamado");

        contadorTurno += 0.5f;
        if(contadorTurno == 4f && DayNight == "Noite")
        {
            //Dia
            DayNight = "Dia";
            startColor = corNoite;
            endColor = corDia;

            transitioning = true;
            Debug.Log("Dia");
        }
        else if (contadorTurno == 5f && DayNight == "Dia")
        {
            //Por do Sol
            startColor = corDia;
            endColor = corPdS;

            transitioning = true;
            Debug.Log("Por do sol");
        }
        else if (contadorTurno == 6f && DayNight == "Dia")
        {
            //Noite
            DayNight = "Noite";
            startColor = corPdS;
            endColor = corNoite;

            contadorTurno = 0f;
            transitioning = true;
            Debug.Log("Noite");
        }

        Debug.Log($"counterTurno: {contadorTurno}\nDayNight: {DayNight}");
    }
}
