using UnityEngine;
using UnityEngine.UIElements;
public class CreatureInfo : MonoBehaviour
{
    [SerializeField] private GameObject _creatureScreen;
    [SerializeField] private GameObject _mutationScreen;
    [SerializeField] private GameObject _buyMutationScreen;
    [SerializeField] private InGameUi _ingameUi;
    [SerializeField] private bool _mutation;
    private Button _creatureInfoBtn;
    private Button _mutationInfoBtn;
    private Button _exitInfoBtn;
    private Button _buyMutationBtn;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _creatureInfoBtn = root.Q<Button>("floating-info-btn");
        _mutationInfoBtn = root.Q<Button>("floating-mutation-btn");
        _exitInfoBtn = root.Q<Button>("exit-info-btn");
        _creatureInfoBtn.RegisterCallback<ClickEvent>(OnClickCreatureInfo);
        _mutationInfoBtn.RegisterCallback<ClickEvent>(OnClickMutationInfo);
        _exitInfoBtn.RegisterCallback<ClickEvent>(OnClickExitInfo);
        if (_mutation)
        {
            _buyMutationBtn = root.Q<Button>("floating-buy-btn");
            _buyMutationBtn.RegisterCallback<ClickEvent>(OnClickBuyMutation);
        }
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
    private void OnClickBuyMutation(ClickEvent evt)
    {
        _mutationScreen.SetActive(false);
        _buyMutationScreen.SetActive(true);
    }
}
