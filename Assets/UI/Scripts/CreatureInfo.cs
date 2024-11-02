using DG.Tweening;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
public class CreatureInfo : MonoBehaviour
{
    [SerializeField] private GameObject _creatureScreen;
    [SerializeField] private GameObject _mutationScreen;
    [SerializeField] private GameObject _buyMutationScreen;
    [SerializeField] private InGameUi _ingameUi;
    private Jubileuson _actualPiece;
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
    public void SetCreatureStateUi(Jubileuson piece)
    {
        SetPiece(piece);
        SetDietUi(_actualPiece);        
        AnimateBar(_actualPiece.GetTemperature().GetIdealTemperatureValue(), 40F, _creatureTemperature);
        _creatureTemperatureTxt.text = _actualPiece.GetTemperature().GetIdealTemperatureAsString();
        AnimateBar(_actualPiece.GetFertilityBar().GetCurrentBarValue(), 100, _creatureFertility);
        _creatureFertilityTxt.text = _actualPiece.GetFertilityBar().GetCurrentBarValueAsString();
        AnimateBar(_actualPiece.GetHumidity().GetIdealHumidityValue(), 100, _creatureHumidity);
        _creatureHumidityTxt.text = _actualPiece.GetHumidity().GetIdealHumidityAsString();
        AnimateBar(_actualPiece.GetEnergyBar().GetCurrentBarValue(), 100, _creatureEnergy);
        _creatureEnergyTxt.text = _actualPiece.GetEnergyBar().GetCurrentBarValueAsString();
        //Falta Dieta e informação dos tiles
    }
    private void SetDietUi(Jubileuson piece)
    {
        if (piece.GetDiet() == PieceDiet.Herbivore)
        {
            _creatureDietImg.RemoveFromClassList("omnivorous-diet-img");
            _creatureDietImg.RemoveFromClassList("carnivore-diet-img");
            _creatureDietImg.AddToClassList("herbivore-diet-img");
            _creatureDietTxt.text = "Herbívoro";
        } else if (piece.GetDiet() == PieceDiet.Carnivore)
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
    private void SetPiece(Jubileuson piece)
    {
        _actualPiece = piece;
    }
    private void AnimateBar(float actualValue, float maxValue, VisualElement targetBar)
    {
        //Pega o valor da barra para animar diminuindo a margem
        float endHeight = targetBar.parent.worldBound.height;
        Debug.Log("Valor atual: " + actualValue);
        Debug.Log("Altura considerada: " + endHeight);
        DOTween.To(() => targetBar.worldBound.height, x => targetBar.style.height = x, (endHeight * (actualValue / maxValue)), 0.5F).SetEase(Ease.Linear);
    }
}
