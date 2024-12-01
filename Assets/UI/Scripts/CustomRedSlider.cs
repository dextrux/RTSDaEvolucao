using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomRedSlider : MonoBehaviour
{
    private VisualElement _root;
    private SliderInt _slider;
    private VisualElement _dragger;
    private VisualElement _tracker;
    private VisualElement _newDragger;
    public SliderInt Slider { get=> _slider; set => _slider = value; }

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _slider = _root.Q<SliderInt>("RedSlider");
        _dragger = _slider.Q<VisualElement>("unity-dragger");
        _tracker = _slider.Q<VisualElement>("unity-tracker");
        AddElements();

        _slider.RegisterCallback<ChangeEvent<int>>(SliderValueChange);
        _slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
    }

    private void AddElements()
    {
        _newDragger = new VisualElement();
        _slider.Add(_newDragger);
        _newDragger.name = "NewDraggerRed";
        _newDragger.AddToClassList("newDraggerRed");
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
    public void ChangeBackground(Sprite newBg)
    {
        IStyle style = _tracker.style;
        StyleBackground newStyleBG = new StyleBackground(newBg);
        style.backgroundImage = newStyleBG;
    }
}
