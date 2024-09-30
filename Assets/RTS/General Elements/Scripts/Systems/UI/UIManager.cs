using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Tile Panel
    public GameObject tilePanel;
    public TMP_Text biome;
    public TMP_Text type;
    public TMP_Text humidity;
    public TMP_Text temperature;
    public GameObject moveButton;
    public GameObject eatButton;
    public GameObject attackButton;
    //Piece Panel
    public GameObject piecePanel;
    public Slider healthSlider;
    public Slider levelSlider;
    public Slider strenghtSlider;
    public TMP_Text diet;
    public TMP_Text pieceHumidity;
    public TMP_Text pieceTemperature;
    public Slider actionsSlider;
    public Slider fertilitySlider;
    //Menu Panel
    public GameObject menuPanel;
    public bool menuPanelIsActivated;
    //EndGame Panel
    public GameObject endGamePanel;
    public TMP_Text endGamePanelTitle;

    private void Start()
    {
        SetAllCanvasOff();
    }
    public void OpenTilePanel(Tile tile)
    {
        biome.text = $"Bioma do Tile: {tile.GetBiome().ToString()}";
        type.text = $"Tipo do Tile: {tile.GetTileType().ToString()}";
        humidity.text = $"Umidade do Tile: {tile.GetHumidity().GetCurrentHumidityAsString()}";
        temperature.text = $"Temperatura do Tile: {tile.GetTemperature().GetCurrentTemperatureAsString()}";
        TilePanelButtonDealer(tile.GetTileType());
        tilePanel.SetActive(true);
        StartCoroutine(DeactivateCanvasCoroutine(tilePanel, 5f));
    }

    public void OpenPiecePanel(Jubileuson piece)
    {
        healthSlider.maxValue = piece.GetHealthBar().GetBarMaxValue();
        healthSlider.minValue = piece.GetHealthBar().GetBarMinValue();
        healthSlider.value = piece.GetHealthBar().GetCurrentBarValue();
        levelSlider.maxValue = 3f;
        levelSlider.minValue = 1f;
        levelSlider.value = (int)piece.GetLevel();
        strenghtSlider.maxValue = piece.GetStrenghtBar().GetBarMaxValue();
        strenghtSlider.minValue = piece.GetStrenghtBar().GetBarMinValue();
        strenghtSlider.value = piece.GetStrenghtBar().GetCurrentBarValue();
        diet.text = $"Dieta: {piece.GetDiet().ToString()}";
        //pieceHumidity.text = $"Umidade: {piece.GetHumidity().GetCurrentHumidityValue()} Umidade Ideal: {piece.GetHumidity().GetIdealHumidityValue()}";
        //pieceTemperature.text = $"Temperatura: {piece.GetTemperature().GetCurrentTemperatureValue()} Umidade Ideal: {piece.GetTemperature().GetIdealTemperatureValue()}";
        actionsSlider.maxValue = 7f;
        actionsSlider.minValue = 4f;
        actionsSlider.value = piece.GetRemainingActions();
        fertilitySlider.maxValue = piece.GetFertilityBar().GetBarMaxValue();
        fertilitySlider.minValue = piece.GetFertilityBar().GetBarMinValue();
        fertilitySlider.value = piece.GetFertilityBar().GetCurrentBarValue();
        piecePanel.SetActive(true);
        StartCoroutine(DeactivateCanvasCoroutine(piecePanel, 5f));
    }

    public void OpenMenuPanel()
    {
        menuPanel.SetActive(true);
        menuPanelIsActivated = true;
    }

    public void OpenEndGamePanel(bool victory)
    {
        if (victory)
        {
            endGamePanelTitle.text = "VITÓRIA";
        }
        else
        {
            endGamePanelTitle.text = "DERROTA";
        }
        endGamePanel.SetActive(true);
    }

    private IEnumerator DeactivateCanvasCoroutine(GameObject canvas, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Deactivate the canvas
        canvas.SetActive(false);
    }

    public void ClosePiecePanel() { piecePanel.SetActive(false); }
    public void CloseTilePanel() { tilePanel.SetActive(false); }

    public void CloseMenuPanel() {  menuPanel.SetActive(false); menuPanelIsActivated = false; }

    public void CloseEndGamePanel() { endGamePanel.SetActive(false); }
    public void TilePanelButtonDealer(TileType type)
    {
        if (type == TileType.Vazio)
        {
            moveButton.SetActive(true);
            eatButton.SetActive(false);
            attackButton.SetActive(false);
        }
        if (type == TileType.Comida)
        {
            moveButton.SetActive(false);
            eatButton.SetActive(true);
            attackButton.SetActive(false);
        }
        if (type == TileType.Inimigo)
        {
            moveButton.SetActive(false);
            eatButton.SetActive(false);
            attackButton.SetActive(true);
        }
        if (type == TileType.Barreira || type == TileType.Próprio)
        {
            moveButton.SetActive(false);
            eatButton.SetActive(false);
            attackButton.SetActive(false);
        }
    }
    
    void SetAllCanvasOff()
    {
        CloseTilePanel();
        ClosePiecePanel();
        CloseMenuPanel();
        CloseEndGamePanel();
    }

    public void ResumeGame()
    {
        SetAllCanvasOff();
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OpenMenuPanel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
