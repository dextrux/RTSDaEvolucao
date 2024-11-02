using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class TempButton : MonoBehaviour
{
    [SerializeField] private GameObject CreatureInfo;
    [SerializeField] private InGameUi _ingameUi;
    [SerializeField] private CreatureInfo _creatureInfo;
    [SerializeField] private Jubileuson _jubileuson;
    private Button tempButton;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        tempButton = root.Q<Button>("temp-button");
        tempButton.RegisterCallback<ClickEvent>(ClickTemp);
    }
    private void ClickTemp(ClickEvent evt)
    {
        _ingameUi.CreatureInfoChange();
        CreatureInfo.SetActive(true);
        _creatureInfo.SetCreatureStateUi(_jubileuson);
        _creatureInfo.SetDisasterWarning(true);
        _creatureInfo.SetDiscomfortWarning(false);
        _creatureInfo.SetHumidityWarning(false);
        _creatureInfo.SetTemperatureWarning(true);
        _creatureInfo.SetillnessWarning(true);
    }
}
