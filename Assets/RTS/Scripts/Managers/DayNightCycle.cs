using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    #region Constantes para mudar a cor da Skybox
    public Material skyboxMaterial; // Material do skybox
    public Color corDia; // Primeira cor
    public Color corPorDoSol; // Segunda cor
    public Color corNoite; // Terceira cor
    private Color startColor; // Cor inicial da transição
    private Color endColor; // Cor final da transição

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
                time = 0f;
            }
        }
    }

    public void ChangeDayNight()
    {
        contadorTurno += 0.5f;
        if(contadorTurno == 4f && DayNight == "Noite")
        {
            //Dia
            DayNight = "Dia";
            startColor = corNoite;
            endColor = corDia;

            transitioning = true;
        }
        else if (contadorTurno == 5f && DayNight == "Dia")
        {
            //Por do Sol
            startColor = corDia;
            endColor = corPorDoSol;

            transitioning = true;
        }
        else if (contadorTurno == 6f && DayNight == "Dia")
        {
            //Noite
            DayNight = "Noite";
            startColor = corPorDoSol;
            endColor = corNoite;

            contadorTurno = 0f;
            transitioning = true;
        }

        Debug.Log($"counterTurno: {contadorTurno}\nDayNight: {DayNight}");
    }
}
