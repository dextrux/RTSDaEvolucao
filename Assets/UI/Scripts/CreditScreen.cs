using UnityEngine;
using UnityEngine.UIElements;
public class CreditScreen : MonoBehaviour
{
    Button _exitCreditsButton;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _exitCreditsButton = root.Q<Button>("exit-credits-btn");
        _exitCreditsButton.RegisterCallback<ClickEvent>(OnClickExitCredits);
    }
    private void OnClickExitCredits(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }
}
