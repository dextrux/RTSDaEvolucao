using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    #region Constantes para mudar a cor da Skybox
    public Material skyboxMaterial; // Material do skybox
    public Color cor1 = Color.blue; // Primeira cor
    public Color cor2 = Color.red; // Segunda cor
    public Color cor3 = Color.green; // Terceira cor
    public float transitionDuration = 5f; // Dura��o da transi��o em segundos
    public int nTurnosParaTrocarDia = 5; // N�mero de turnos para trocar de dia para noite

    private InGameUi inGameUi;
    public GameObject GameUi;

    private float transitionTime = 0f;
    private bool transitioning = false;
    private int currentColorIndex = 0; // �ndice da cor atual
    private Color startColor; // Cor inicial da transi��o
    private Color targetColor; // Cor final da transi��o
    #endregion

    //Variavel que define se est� de dia ou de noite
    public string DayNight;

    void Start()
    {
        DayNight = "Dia";
        inGameUi = GameUi.GetComponent<InGameUi>();
        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            startColor = cor1; // Inicia com a primeira cor
            targetColor = cor2; // Define a pr�xima cor
            skyboxMaterial.SetColor("_Tint", startColor);
        }
    }

    void Update()
    {
        if ((inGameUi.ActualTurn != 0) && (inGameUi.ActualTurn % nTurnosParaTrocarDia == 0) && !transitioning) // troca o skybox de cor de 5 em 5 turnos
        {
            StartTransition();
        }

        //transiociona a cor do skybox para a proxima cor e seta 
        if (transitioning && skyboxMaterial != null)
        {
            transitionTime += Time.deltaTime / transitionDuration;
            Color currentColor = Color.Lerp(startColor, targetColor, transitionTime);
            skyboxMaterial.SetColor("_Tint", currentColor);

            if (transitionTime >= 1f)
            {
                transitioning = false;
                transitionTime = 0f;
                CycleColors(); // Avan�a para a pr�xima transi��o
            }
        }
    }

    // M�todo para iniciar a transi��o
    private void StartTransition()
    {
        transitioning = true;
        transitionTime = 0f;
    }

    // Alterna entre as cores em sequ�ncia
    private void CycleColors()
    {
        currentColorIndex = (currentColorIndex + 1) % 3;
        startColor = targetColor;

        switch (currentColorIndex)
        {
            case 0:
                DayNight = "Dia";
                targetColor = cor1;
                break;
            case 1:
                targetColor = cor2;
                break;
            case 2:
                DayNight = "Noite";
                targetColor = cor3;
                break;
        }
    }
}
