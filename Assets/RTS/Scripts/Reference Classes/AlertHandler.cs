using System.Collections.Generic;
using UnityEngine;

public static class AlertHandler
{
    #region Public Methods

    public static void AlertVerificationRoutine(Piece piece)
    {
        Tile tile = piece.PieceRaycastForTile();

        if (tile == null) return;
        Debug.Log("Verificando os alertas");
        CheckTemperatureAlert(piece, tile);
        CheckHumidityAlert(piece, tile);
        CheckHungerAlert(piece);
        CheckEnergyAlert(piece);
    }

    #endregion

    #region Temperature Alerts

    private static void CheckTemperatureAlert(Piece piece, Tile tile)
    {
        bool isTemperatureAlert = Mathf.Abs(piece.Temperature.IdealValue - tile.Temperature.CurrentValue) > 15;
        UpdateAlert(piece.Alerta, Alerta.Temperatura, isTemperatureAlert);

        if (isTemperatureAlert)
        {
            bool isCold = piece.Temperature.IdealValue > tile.Temperature.CurrentValue;
            bool isHot = piece.Temperature.IdealValue < tile.Temperature.CurrentValue;
            UpdateAlert(piece.Alerta, Alerta.Frio, isCold);
            UpdateAlert(piece.Alerta, Alerta.Calor, isHot);
        }
        else
        {
            RemoveAdjacentTemperatureAlerts(piece.Alerta);
        }
    }

    private static void RemoveAdjacentTemperatureAlerts(List<Alerta> alerts)
    {
        alerts.Remove(Alerta.Frio);
        alerts.Remove(Alerta.Calor);
    }

    #endregion

    #region Humidity Alerts

    private static void CheckHumidityAlert(Piece piece, Tile tile)
    {
        bool isHumidityAlert = Mathf.Abs(piece.Humidity.IdealValue - tile.Humidity.CurrentValue) > 38;
        UpdateAlert(piece.Alerta, Alerta.Umidade, isHumidityAlert);

        if (isHumidityAlert)
        {
            bool isDry = piece.Humidity.IdealValue < tile.Humidity.CurrentValue;
            bool isUncomfortable = piece.Humidity.IdealValue > tile.Humidity.CurrentValue;
            UpdateAlert(piece.Alerta, Alerta.Ressecaçao, isDry);
            UpdateAlert(piece.Alerta, Alerta.Desconforto, isUncomfortable);
        }
        else
        {
            RemoveAdjacentHumidityAlerts(piece.Alerta);
        }
    }

    private static void RemoveAdjacentHumidityAlerts(List<Alerta> alerts)
    {
        alerts.Remove(Alerta.Ressecaçao);
        alerts.Remove(Alerta.Desconforto);
    }

    #endregion

    #region Hunger Alerts

    private static void CheckHungerAlert(Piece piece)
    {
        bool isHungry = piece.Hunger.CurrentBarValue < 0.25f * piece.Hunger.MaxBarValue;
        if (isHungry) { piece.Health.CurrentBarValue -= 20; }
        UpdateAlert(piece.Alerta, Alerta.Fome, isHungry);
    }

    #endregion

    #region Energy Alerts

    private static void CheckEnergyAlert(Piece piece)
    {
        bool isTired = piece.Energy.CurrentBarValue < 0.25f * piece.Energy.MaxBarValue;
        UpdateAlert(piece.Alerta, Alerta.Cansaço, isTired);
    }

    #endregion

    #region Alert Management

    private static void UpdateAlert(List<Alerta> alerts, Alerta alert, bool condition)
    {
        if (condition)
        {
            if (!alerts.Contains(alert))
                alerts.Add(alert);
        }
        else
        {
            alerts.Remove(alert);
        }
    }

    #endregion
}
