using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] private SelectionUIManager selectionManager;
    private VisualElement _root;
    private SliderInt _slider;
    private VisualElement _dragger;
    private VisualElement _newDragger;

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _slider = _root.Q<SliderInt>("MySlider");
        _dragger = _root.Q<VisualElement>("unity-dragger");
        AddElements();

        _slider.RegisterCallback<ChangeEvent<int>>(SliderValueChange);
        _slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
    }

    private void AddElements()
    {
        _newDragger = new VisualElement();
        _slider.Add(_newDragger);
        _newDragger.name = "NewDragger";
        _newDragger.AddToClassList("newdragger");
        _newDragger.pickingMode = PickingMode.Ignore;
    }
    private void SliderValueChange(ChangeEvent<int> value)
    {
        Vector2 dist = new Vector2((_newDragger.layout.width - _dragger.layout.width) / 2, (_newDragger.layout.height - _dragger.layout.height) / 2 - 12f);
        Vector2 pos = _dragger.parent.LocalToWorld(_dragger.transform.position);
        _newDragger.transform.position = _newDragger.parent.WorldToLocal(pos - dist);
    }
    private void SliderInit(GeometryChangedEvent evt)
    {
        Vector2 dist = new Vector2((_newDragger.layout.width - _dragger.layout.width) / 2, (_newDragger.layout.height - _dragger.layout.height) / 2 - 12f);
        Vector2 pos = _dragger.parent.LocalToWorld(_dragger.transform.position);
        _newDragger.transform.position = _newDragger.parent.WorldToLocal(pos - dist);
    }
}
