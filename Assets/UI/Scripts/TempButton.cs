using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class TempButton : MonoBehaviour
{
    [SerializeField] private GameObject CreatureInfo;
    [SerializeField] private InGameUi _ingameUi;
    private Button tempButton;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        tempButton = root.Q<Button>("temp-button");
        tempButton.RegisterCallback<ClickEvent>(ClickTemp);
    }
    private void ClickTemp(ClickEvent evt)
    {
        TryFixing();
        CreatureInfo.SetActive(true);
    }
    private void TryFixing()
    {
        _ingameUi.CreatureInfoChange();
    }
}
