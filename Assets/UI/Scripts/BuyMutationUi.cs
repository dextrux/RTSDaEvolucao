using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuyMutationUi : MonoBehaviour
{
    private Button exitBuyMutation;

    private int _selectedMutation;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        exitBuyMutation = root.Q<Button>("exit-mutation-btn");
        exitBuyMutation.RegisterCallback<ClickEvent>(ExitBuyMutation);
    }
    private void ExitBuyMutation(ClickEvent evt)
    {
        gameObject.SetActive(false);
    }
}
