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
        biome.text = $"Bioma do Tile: {tile.Biome}";
        type.text = $"Tipo do Tile: {tile.TileType}";
        humidity.text = $"Umidade do Tile: {tile.Humidity.CurrentHumidity}";
        temperature.text = $"Temperatura do Tile: {tile.Temperature.CurrentTemperature}";
        TilePanelButtonDealer(tile.TileType);
        tilePanel.SetActive(true);
        StartCoroutine(DeactivateCanvasCoroutine(tilePanel, 5f));
    }

    public void OpenPiecePanel(Piece piece)
    {
        healthSlider.maxValue = piece.HealthBar.MaxBarValue;
        healthSlider.minValue = piece.HealthBar.MinBarValue;
        healthSlider.value = piece.HealthBar.CurrentBarValue;
        levelSlider.maxValue = 3f;
        levelSlider.minValue = 1f;
        levelSlider.value = (int)piece.Level;
        //strenghtSlider.maxValue = piece.StrenghtBar.GetBarMaxValue;
        //strenghtSlider.minValue = piece.GetStrenghtBar.GetBarMinValue;
        //strenghtSlider.value = piece.GetStrenghtBar.GetCurrentBarValue;
        diet.text = $"Dieta: {piece.Diet}";
        pieceHumidity.text = $"Umidade: {piece.Humidity.CurrentHumidity} Umidade Ideal: {piece.Humidity.IdealHumidity}";
        pieceTemperature.text = $"Temperatura: {piece.Temperature.CurrentTemperature} Umidade Ideal: {piece.Temperature.IdealTemperature}";
        actionsSlider.maxValue = 7f;
        actionsSlider.minValue = 4f;
        //actionsSlider.value = piece.GetRemainingActions();
        fertilitySlider.maxValue = piece.FertilityBar.MaxBarValue;
        fertilitySlider.minValue = piece.FertilityBar.MinBarValue;
        fertilitySlider.value = piece.FertilityBar.CurrentBarValue;
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
        if (type == TileType.NPC)
        {
            moveButton.SetActive(false);
            eatButton.SetActive(false);
            attackButton.SetActive(true);
        }
        if (type == TileType.Barreira || type == TileType.P1)
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
