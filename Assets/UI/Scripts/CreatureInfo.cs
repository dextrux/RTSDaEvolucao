using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
public class CreatureInfo : MonoBehaviour
{
    [SerializeField] private GameObject _creatureScreen;
    [SerializeField] private GameObject _mutationScreen;
    [SerializeField] private GameObject _buyMutationScreen;
    [SerializeField] private InGameUi _ingameUi;
    [SerializeField] private PlayerRaycast _playerRaycast;
    private Piece _actualPiece;
    private Tile _actualTile;
    private Button _creatureInfoBtn;
    private Button _mutationInfoBtn;
    private Button _actionBtn;
    private Button _exitInfoBtn;
    #region Avisos Sobre a peca
    private VisualElement _discomfortWarning;
    private VisualElement _temperatureWarning;
    private VisualElement _disasterWarning;
    private VisualElement _humidityWarning;
    private VisualElement _illnessWarning;
    #endregion
    #region Dados Sobre a peça
    private VisualElement _creatureDietImg;
    private Label _creatureDietTxt;
    private VisualElement _creatureTemperature;
    private Label _creatureTemperatureTxt;
    private VisualElement _creatureFertility;
    private Label _creatureFertilityTxt;
    private VisualElement _creatureHumidity;
    private Label _creatureHumidityTxt;
    private VisualElement _creatureEnergy;
    private Label _creatureEnergyTxt;
    private VisualElement _creatureHunger;
    private Label _creatureHungerTxt;
    private VisualElement _tileHumidity;
    private Label _tileHumidityTxt;
    private VisualElement _tileBiome;
    private Label _tileBiomeTxt;
    private VisualElement _tileTemperature;
    private Label _tileTemperatureTxt;
    #endregion
    private void OnEnable()
    {
        SetReferences();
        _ingameUi.CreatureInfoChange();
        _creatureInfoBtn.RegisterCallback<ClickEvent>(OnClickCreatureInfo);
        _mutationInfoBtn.RegisterCallback<ClickEvent>(OnClickMutationInfo);
        _actionBtn.RegisterCallback<ClickEvent>(OnClickAction);
        _exitInfoBtn.RegisterCallback<ClickEvent>(OnClickExitInfo);
    }
    private void OnClickCreatureInfo(ClickEvent evt)
    {
        _mutationScreen.SetActive(false);
        _creatureScreen.SetActive(true);
    }
    private void OnClickMutationInfo(ClickEvent evt)
    {
        _creatureScreen.SetActive(false);
        _mutationScreen.SetActive(true);
        _mutationScreen.GetComponent<MutationList>().SetPieceMutationScreen(_actualPiece);
    }
    private void OnClickExitInfo(ClickEvent evt)
    {
        _playerRaycast.DeselectPiece();
        _ingameUi.CreatureInfoNormal();
        _ingameUi.UpdateLifeBarOwnerBase(GameObject.Find("Manager").GetComponent<RoundManager>());
        _creatureScreen.SetActive(false);
        _mutationScreen.SetActive(false);
    }
    private void OnClickAction(ClickEvent evt)
    {
        _ingameUi.CreatureInfoNormal();
        FindAnyObjectByType<PlayerRaycast>().isBlinking = true;
        _ingameUi.UpdateLifeBarOwnerBase(GameObject.Find("Manager").GetComponent<RoundManager>());
        gameObject.SetActive(false);
    }
    public void SetPiece(Piece piece)
    {
        _actualPiece = piece;
        _actualTile = _actualPiece.PieceRaycastForTile();
        SetCreatureStateUi(piece);
    }
    private void SetCreatureStateUi(Piece piece)
    {
        SetDietUi();
        _ingameUi.AnimateLifeBar(Mathf.Round(_actualPiece.Health.CurrentBarValue), Mathf.Round(_actualPiece.Health.MaxBarValue));
        _creatureTemperatureTxt.text = Mathf.Round(_actualPiece.Temperature.IdealValue).ToString() + " ºC";
        _creatureFertilityTxt.text = Mathf.Round(_actualPiece.Fertility.CurrentBarValue).ToString() + "%";
        _creatureHumidityTxt.text = Mathf.Round(_actualPiece.Humidity.CurrentValue).ToString() + "%";
        _creatureEnergyTxt.text = Mathf.Round(_actualPiece.Energy.CurrentBarValue).ToString() + "%";
        _creatureHungerTxt.text = Mathf.Round(_actualPiece.Hunger.CurrentBarValue).ToString() + "%";
        SetTileRefs();
        SetWarningUi();
    }
    private void SetDietUi()
    {
        if (_actualPiece.Diet == PieceDiet.Herbivore)
        {
            _creatureDietImg.RemoveFromClassList("omnivorous-diet-img");
            _creatureDietImg.RemoveFromClassList("carnivore-diet-img");
            _creatureDietImg.AddToClassList("herbivore-diet-img");
            _creatureDietTxt.text = "Herbívoro";
        }
        else if (_actualPiece.Diet == PieceDiet.Carnivore)
        {
            _creatureDietImg.RemoveFromClassList("herbivore-diet-img");
            _creatureDietImg.RemoveFromClassList("omnivorous-diet-img");
            _creatureDietImg.AddToClassList("carnivore-diet-img");
            _creatureDietTxt.text = "Carnívoro";
        }
        else
        {
            _creatureDietImg.RemoveFromClassList("herbivore-diet-img");
            _creatureDietImg.RemoveFromClassList("carnivore-diet-img");
            _creatureDietImg.AddToClassList("omnivorous-diet-img");
            _creatureDietTxt.text = "Onívoro";
        }
    }
    private void SetTileRefs()
    {
        _tileHumidityTxt.text = _actualTile.Humidity.CurrentValue + "%";
        _tileTemperatureTxt.text = _actualTile.Temperature.CurrentValue + "ºC";
        if (_actualTile.Biome == Biome.Caatinga)
        {
            _tileBiome.AddToClassList("caatinga-visual");
            _tileBiome.RemoveFromClassList("mata-atlantica-visual");
            _tileBiome.RemoveFromClassList("mata-das-araucarias-visual");
            _tileBiome.RemoveFromClassList("pampa-visual");
            _tileBiome.RemoveFromClassList("pantanal-visual");
            _tileBiomeTxt.text = "Caatinga" ;
        }
        else if (_actualTile.biome == Biome.Mata_Atlantica)
        {
            _tileBiome.RemoveFromClassList("caatinga-visual");
            _tileBiome.AddToClassList("mata-atlantica-visual");
            _tileBiome.RemoveFromClassList("mata-das-araucarias-visual");
            _tileBiome.RemoveFromClassList("pampa-visual");
            _tileBiome.RemoveFromClassList("pantanal-visual");
            _tileBiomeTxt.text = "Mata Atlântica" ;
        }
        else if (_actualTile.biome == Biome.Pantanal)
        {
            _tileBiome.RemoveFromClassList("caatinga-visual");
            _tileBiome.RemoveFromClassList("mata-atlantica-visual");
            _tileBiome.RemoveFromClassList("mata-das-araucarias-visual");
            _tileBiome.RemoveFromClassList("pampa-visual");
            _tileBiome.AddToClassList("pantanal-visual");
            _tileBiomeTxt.text = "Pantanal" ;
        }
        else if (_actualTile.biome == Biome.Pampa)
        {
            _tileBiome.RemoveFromClassList("caatinga-visual");
            _tileBiome.RemoveFromClassList("mata-atlantica-visual");
            _tileBiome.RemoveFromClassList("mata-das-araucarias-visual");
            _tileBiome.AddToClassList("pampa-visual");
            _tileBiome.RemoveFromClassList("pantanal-visual");
            _tileBiomeTxt.text = "Pampa";
        }
        else
        {
            _tileBiome.RemoveFromClassList("caatinga-visual");
            _tileBiome.RemoveFromClassList("mata-atlantica-visual");
            _tileBiome.AddToClassList("mata-das-araucarias-visual");
            _tileBiome.RemoveFromClassList("pampa-visual");
            _tileBiome.RemoveFromClassList("pantanal-visual");
            _tileBiomeTxt.text = "Mata das Araucarias";
        }
    }
    private void SetWarningUi()
    {
        SetDiscomfortWarning(false);
        SetTemperatureWarning(false);
        SetHumidityWarning(false);
        if (_actualPiece.IsUnderDesastre) SetDisasterWarning(true);
        else SetDisasterWarning(false);
        if (_actualPiece.IsDoente) SetillnessWarning(true);
        else SetillnessWarning(false);
        foreach (Alerta activeWarning in _actualPiece.Alerta)
        {
            if (activeWarning == Alerta.Fome || activeWarning == Alerta.Cansaço) SetDiscomfortWarning(true);
            if (activeWarning == Alerta.Temperatura || activeWarning == Alerta.Calor || activeWarning == Alerta.Frio) SetTemperatureWarning(true);
            if (activeWarning == Alerta.Umidade || activeWarning == Alerta.Ressecaçao || activeWarning == Alerta.Desconforto) SetHumidityWarning(true);
        }
    }
    private void SetDiscomfortWarning(bool active)
    {
        if (active) _discomfortWarning.RemoveFromClassList("warning-close");
        else _discomfortWarning.AddToClassList("warning-close");
    }
    private void SetTemperatureWarning(bool active)
    {
        if (active) _temperatureWarning.RemoveFromClassList("warning-close");
        else _temperatureWarning.AddToClassList("warning-close");
    }
    private void SetDisasterWarning(bool active)
    {
        if (active)
            _disasterWarning.RemoveFromClassList("warning-close");
        else
            _disasterWarning.AddToClassList("warning-close");
    }
    private void SetHumidityWarning(bool active)
    {
        if (active)
            _humidityWarning.RemoveFromClassList("warning-close");
        else
            _humidityWarning.AddToClassList("warning-close");
    }
    private void SetillnessWarning(bool active)
    {
        if (active)
            _illnessWarning.RemoveFromClassList("warning-close");
        else
            _illnessWarning.AddToClassList("warning-close");
    }
    private void SetReferences()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _creatureInfoBtn = root.Q<Button>("floating-info-btn");
        _mutationInfoBtn = root.Q<Button>("floating-mutation-btn");
        _actionBtn = root.Q<Button>("floating-action-btn");
        _exitInfoBtn = root.Q<Button>("exit-info-btn");
        _discomfortWarning = root.Q<VisualElement>("discomfort-container");
        _temperatureWarning = root.Q<VisualElement>("temperature-container");
        _disasterWarning = root.Q<VisualElement>("disaster-container");
        _humidityWarning = root.Q<VisualElement>("humidity-container");
        _illnessWarning = root.Q<VisualElement>("illness-container");
        _creatureDietImg = root.Q<VisualElement>("creature-diet-img");
        _creatureDietTxt = root.Q<Label>("creature-diet-txt");
        _creatureTemperature = root.Q<VisualElement>("creature-temperature-bar");
        _creatureTemperatureTxt = root.Q<Label>("creature-temperature-txt");
        _creatureFertility = root.Q<VisualElement>("reproduction-bar");
        _creatureFertilityTxt = root.Q<Label>("creature-reproduction-txt");
        _creatureHumidity = root.Q<VisualElement>("creature-humidity-bar");
        _creatureHumidityTxt = root.Q<Label>("creature-humidity-txt");
        _creatureEnergy = root.Q<VisualElement>("creature-energy-bar");
        _creatureEnergyTxt = root.Q<Label>("creature-energy-txt");
        _creatureHunger = root.Q<VisualElement>("creature-hunger-bar");
        _creatureHungerTxt = root.Q<Label>("creature-hunger-txt");
        _tileBiome = root.Q<VisualElement>("biome-visual");
        _tileBiomeTxt = root.Q<Label>("biome-txt");
        _tileHumidity = root.Q<VisualElement>("tile-humidity-bar");
        _tileHumidityTxt = root.Q<Label>("tile-humidity-txt");
        _tileTemperature = root.Q<VisualElement>("tile-temperature-bar");
        _tileTemperatureTxt = root.Q<Label>("tile-temperature-txt");
    }
}
