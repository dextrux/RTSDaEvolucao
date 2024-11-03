using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
public class CreatureInfo : MonoBehaviour
{
    [SerializeField] private GameObject _creatureScreen;
    [SerializeField] private GameObject _mutationScreen;
    [SerializeField] private GameObject _buyMutationScreen;
    [SerializeField] private InGameUi _ingameUi;
    private Piece _actualPiece;
    private Button _creatureInfoBtn;
    private Button _mutationInfoBtn;
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
        var root = GetComponent<UIDocument>().rootVisualElement;
        _creatureInfoBtn = root.Q<Button>("floating-info-btn");
        _mutationInfoBtn = root.Q<Button>("floating-mutation-btn");
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
        _creatureInfoBtn.RegisterCallback<ClickEvent>(OnClickCreatureInfo);
        _mutationInfoBtn.RegisterCallback<ClickEvent>(OnClickMutationInfo);
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
    }
    private void OnClickExitInfo(ClickEvent evt)
    {
        _creatureScreen.SetActive(false);
        _mutationScreen.SetActive(false);
        _ingameUi.CreatureInfoNormal();
    }
    public void SetDiscomfortWarning(bool active)
    {
        if (active) _discomfortWarning.RemoveFromClassList("warning-close");
        else _discomfortWarning.AddToClassList("warning-close");
    }
    public void SetTemperatureWarning(bool active)
    {
        if (active) _discomfortWarning.RemoveFromClassList("warning-close");
        else _discomfortWarning.AddToClassList("warning-close");
    }
    public void SetDisasterWarning(bool active)
    {
        if (active)
            _discomfortWarning.RemoveFromClassList("warning-close");
        else
            _discomfortWarning.AddToClassList("warning-close");
    }
    public void SetHumidityWarning(bool active)
    {
        if (active)
            _discomfortWarning.RemoveFromClassList("warning-close");
        else
            _discomfortWarning.AddToClassList("warning-close");
    }
    public void SetillnessWarning(bool active)
    {
        if (active)
            _discomfortWarning.RemoveFromClassList("warning-close");
        else
            _discomfortWarning.AddToClassList("warning-close");
    }
    //Ajusta a dieta,as barras e o texto para a devida porcentagem da criatura
    public void SetCreatureStateUi(Piece piece)
    {
        SetPiece(piece);
        SetDietUi(_actualPiece);
        AnimateBar(_actualPiece.Temperature.IdealTemperature, 40F, _creatureTemperature);
        _creatureTemperatureTxt.text = _actualPiece.Temperature.IdealTemperature.ToString() + " ºC";
        AnimateBar(_actualPiece.FertilityBar.CurrentBarValue, 100, _creatureFertility);
        _creatureFertilityTxt.text = _actualPiece.FertilityBar.CurrentBarValue.ToString() + " %";
        AnimateBar(_actualPiece.Humidity.CurrentHumidity, 100, _creatureHumidity);
        _creatureHumidityTxt.text = _actualPiece.Humidity.CurrentHumidity.ToString() + " %";
        AnimateBar(_actualPiece.EnergyBar.CurrentBarValue, 100, _creatureEnergy);
        _creatureEnergyTxt.text = _actualPiece.EnergyBar.CurrentBarValue.ToString() + " %";
        //Falta informação dos tiles e a fome
    }
    private void SetDietUi(Piece piece)
    {
        if (piece.Diet == PieceDiet.Herbivore)
        {
            _creatureDietImg.RemoveFromClassList("omnivorous-diet-img");
            _creatureDietImg.RemoveFromClassList("carnivore-diet-img");
            _creatureDietImg.AddToClassList("herbivore-diet-img");
            _creatureDietTxt.text = "Herbívoro";
        } else if (piece.Diet == PieceDiet.Carnivore)
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
    public void SetPiece(Piece piece)
    {
        _actualPiece = piece;
    }
    private void AnimateBar(float actualValue, float maxValue, VisualElement targetBar)
    {
        targetBar.RegisterCallback<GeometryChangedEvent>(evt =>
        {
            float endHeight = targetBar.parent.worldBound.height * (actualValue / maxValue);
            DOTween.To(() => targetBar.resolvedStyle.top, x => targetBar.style.top = x, endHeight, 0.5F).SetEase(Ease.Linear);
        });
    }
}
