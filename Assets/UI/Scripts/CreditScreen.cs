using UnityEngine;
using UnityEngine.UIElements;
public class CreditScreen : MonoBehaviour
{
    Button _exitCreditsButton;
    [SerializeField] private AudioClip _buttonConfirmation;
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        _exitCreditsButton = root.Q<Button>("exit-credits-btn");
        _exitCreditsButton.RegisterCallback<ClickEvent>(OnClickExitCredits);
    }
    private void OnClickExitCredits(ClickEvent evt)
    {
        SoundManagerSO.PlaySoundFXClip(_buttonConfirmation, transform.position, 1);
        gameObject.SetActive(false);
    }
}
