using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Atributos da peça
    private StatusBar _healthBar;
    private StatusBar _energyBar;
    private StatusBar _fertilityBar;
    private PieceDiet _diet;
    private PieceOwner _owner;
    private PieceLevel _level;
    private StatusBar _strength;
    private Humidity _humidity;
    private Temperature _temperature;
    private bool _isResting;
    private bool _isUnderDisastre;
    // Atributos Raycast
    private const float _rayDistanceTile = 10f;
    public static LayerMask TileLayerMask;

    // Propriedades
    public StatusBar EnergyBar { get { return _energyBar; } }
    public StatusBar HealthBar { get { return _healthBar; } }
    public StatusBar FertilityBar { get { return _fertilityBar; } }
    public PieceDiet Diet { get { return _diet; } set { _diet = value; } }
    public PieceOwner Owner { get { return _owner; } set { _owner = value; } }
    public PieceLevel Level { get { return _level; } set { _level = value; } }
    public StatusBar Strength { get { return _strength; } }
    public Humidity Humidity { get { return _humidity; } }
    public Temperature Temperature { get { return _temperature; } }
    public bool Resting { get { return _isResting; } set { _isResting = value; } }

    public bool IsUnderDesastre { get { return _isUnderDisastre; } set { _isUnderDisastre = value; } }

    public Tile PieceRaycastForTile()
    {
        Vector3 rayOriginTransform = this.transform.position;
        if (Physics.Raycast(rayOriginTransform, Vector3.down, out RaycastHit hit, _rayDistanceTile, TileLayerMask))
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTile, Color.green);
            if (hit.collider.gameObject.tag == "Tile")
            {
                return hit.collider.gameObject.GetComponent<Tile>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            Debug.DrawRay(rayOriginTransform, Vector3.down * _rayDistanceTile, Color.red);
            return null;
        }
    }

    public static void Eat() { }
    public static void Rest(Piece piece)
    { 
        piece.Resting = true;
    }
    public static void Walk(GameObject piece, GameObject newTile) 
    {
        piece.gameObject.transform.position = new Vector3(newTile.transform.position.x, 5, newTile.transform.position.z);
    }
    public static void Fight() { }
    //public static void Reproduce(Piece father, Piece mother, Tile tile, GameObject newTile) 
    //{
    //    Vector3 newPiecePosition = new Vector3(newTile.transform.position.x, 5f, newTile.transform.position.z);
    //    GameObject newPiece = Instantiate(prefabSon, newPiecePosition, Quaternion.identity);
    //    Piece son = newPiece.AddComponent<Piece>();
    //    son.Temperature.IdealTemperature = (2 * father.GetTemperature().GetIdealTemperatureValue() + 2 * mother.GetTemperature().GetIdealTemperatureValue() + currentTile.GetTemperature().GetCurrentTemperatureValue() / 5);
    //    son.Humidity.IdealHumidity = (2 * father.GetHumidity().GetIdealHumidityValue() + 2 * mother.GetHumidity().GetIdealHumidityValue() + currentTile.GetHumidity().GetCurrentHumidityValue() / 5);
    //    StatusBar sonFertilityBar = son.GetFertilityBar();
    //    sonFertilityBar.SetNewBarValue(0);
    //    StatusBar sonHealthBar = son.GetHealthBar();
    //    //Altere para contar com as mutações
    //    sonHealthBar.SetNewBarValue(sonHealthBar.GetBarMaxValue());
    //    StatusBar sonStrenghtBar = son.GetStrenghtBar();
    //    sonStrenghtBar.SetNewBarValue(20);
    //    StatusBar sonEnergyBar = son.GetEnergyBar();
    //    sonEnergyBar.SetNewBarValue(100);
    //}

    public static bool LoseEnergy(Piece piece, Tile tile)
    {
        //(| Tr - TI | +(0.4 * | Ur - UI |))
        //Alterar para levar em conta as mutações
        if (!piece.Resting) 
        {
            if (piece.EnergyBar.CurrentBarValue >= piece.EnergyBar.CurrentBarValue - (Mathf.Abs(tile.Temperature.CurrentTemperature - piece.Temperature.IdealTemperature) + (0.4f * Mathf.Abs(tile.Humidity.CurrentHumidity - piece.Humidity.IdealHumidity))))
            {
                piece.EnergyBar.CurrentBarValue = piece.EnergyBar.CurrentBarValue - (Mathf.Abs(tile.Temperature.CurrentTemperature - piece.Temperature.IdealTemperature) + (0.4f * Mathf.Abs(tile.Humidity.CurrentHumidity - piece.Humidity.IdealHumidity)));
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void LoseLifeUnderDisastre(Piece piece)
    {
        //(| Tr - TI | +(0.4 * | Ur - UI |))
        Tile tile = PieceRaycastForTile();
        if (piece.IsUnderDesastre)
        {
          piece.EnergyBar.CurrentBarValue = piece.EnergyBar.CurrentBarValue - (Mathf.Abs(tile.Temperature.CurrentTemperature - piece.Temperature.IdealTemperature) + (0.4f * Mathf.Abs(tile.Humidity.CurrentHumidity - piece.Humidity.IdealHumidity)));
        }
    }

    public static void SetPieceAtributes(Piece piece)
    {
        //Level 1 =  (0 - 3 mutações) - HP máximo: 100 - Energia: 100 - Força: 20
        //Level 2 = (4 - 6 mutações) - HP máximo: 300 - Energia: 200 - Força: 20
        //Level 3 = HP máximo: 900 - Energia: 400 - Força: 30
        switch ((int)piece.Level)
        {
            case 1: 
                piece.HealthBar.MaxBarValue = 100;           
                piece.EnergyBar.MaxBarValue = 100;
                piece.Strength.CurrentBarValue = 20;
                break;
            case 2:
                piece.HealthBar.MaxBarValue = 300;              
                piece.EnergyBar.MaxBarValue = 200;
                piece.Strength.CurrentBarValue = 20;
                break;
            case 3:
                piece.HealthBar.MaxBarValue = 900;        
                piece.EnergyBar.MaxBarValue = 400;
                piece.Strength.CurrentBarValue = 30;
                break;
            default: Debug.Log("Exceção encontrada no nivelamento das peças");
                break;

        }
    }
}
