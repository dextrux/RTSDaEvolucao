using UnityEngine;
using UnityEngine.UIElements;

public class TempButton : MonoBehaviour
{
    [SerializeField] private GameObject CreatureInfo;
    [SerializeField] private InGameUi _ingameUi;
    [SerializeField] private CreatureInfo _creatureInfo;
    [SerializeField] private Piece _jubileuson;
    [SerializeField] private RoundManager _roundManager;
    private Button tempButton;
    private Button temp2Button;
    private int tempIndex = 1;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        tempButton = root.Q<Button>("temp-button");
        temp2Button = root.Q<Button>("temp2-button");
        tempButton.RegisterCallback<ClickEvent>(ClickTemp);
    }
    private void ClickTemp(ClickEvent evt)
    {
        _ingameUi.CreatureInfoChange();
        CreatureInfo.SetActive(true);
        _creatureInfo.SetPiece(_jubileuson);
        _creatureInfo.SetCreatureStateUi(_jubileuson);
        _creatureInfo.SetDisasterWarning(true);
        _creatureInfo.SetDiscomfortWarning(false);
        _creatureInfo.SetHumidityWarning(false);
        _creatureInfo.SetTemperatureWarning(true);
        _creatureInfo.SetillnessWarning(true);
    }
}
