using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverUi : MonoBehaviour
{
    [SerializeField] GameObject _inGameUi;
    private Button _menuButton;
    private void OnEnable()
    {
        _inGameUi.SetActive(false);
        var root = gameObject.GetComponent<UIDocument>().rootVisualElement;
        _menuButton = root.Q<Button>("menu-btn");
        _menuButton.RegisterCallback<ClickEvent>(OnClickMenuButton);
    }
    private void OnClickMenuButton(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }
}
