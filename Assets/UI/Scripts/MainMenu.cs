using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private Button _playBtn;
    private Button _configBtn;
    private Button _configSaveBtn;
    private Button _creditsBtn;
    private VisualElement _optionsContainer;
    [SerializeField] GameObject _creditsScreen;

    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _playBtn = root.Q<Button>("play-btn");
        _configBtn = root.Q<Button>("option-btn");
        _configSaveBtn = root.Q<Button>("save-option-btn");
        _creditsBtn = root.Q<Button>("credit-btn");
        _optionsContainer = root.Q<VisualElement>("options-container");
        _playBtn.RegisterCallback<ClickEvent>(OnClickPlay);
        _configBtn.RegisterCallback<ClickEvent>(OnClickConfig);
        _configSaveBtn.RegisterCallback<ClickEvent>(OnClickConfigSave);
        _creditsBtn.RegisterCallback<ClickEvent>(OnClickCredits);
    }

    private void OnClickPlay(ClickEvent evt)
    {
        SceneManager.LoadScene("Game");
    }
    private void OnClickConfig(ClickEvent evt)
    {
        _optionsContainer.AddToClassList("option-open");
    }
    private void OnClickExitOption(ClickEvent evt)
    {
        _optionsContainer.RemoveFromClassList("option-open");
    }
    private void OnClickConfigSave(ClickEvent evt)
    {
        _optionsContainer.RemoveFromClassList("option-open");
    }
    private void OnClickCredits(ClickEvent evt)
    {
        _creditsScreen.SetActive(true);
    }
}
