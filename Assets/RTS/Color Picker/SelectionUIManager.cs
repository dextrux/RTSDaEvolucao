using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class SelectionUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject colorPickerPanel;
    public List<Slider> channelSliders;
    public List<RawImage> channelDisplay;
    public TMP_InputField hexInputField;

    [Header("Material Settings")]
    public GameObject playerObject;
    public List<Material> materials;
    public int currentTarget;

    private bool _isUpdating;

    private void Start()
    {
        UpdateSlidersFromObjectColor();
        AddListeners();
        InitializeChannelDisplayColors();
        UpdateColor();
    }

    private void AddListeners()
    {
        for (int i = 0; i < channelSliders.Count; i++)
        {
            int index = i; // To avoid closure issue
            channelSliders[i].onValueChanged.AddListener(value => { if (index == 3) UpdateAlpha(value); else UpdateColor(); });
        }
        hexInputField.onEndEdit.AddListener(OnHexInputChanged);
    }

    private void InitializeChannelDisplayColors()
    {
        for (int i = 0; i < channelDisplay.Count; i++)
        {
            channelDisplay[i].color = materials[i].color;
        }
    }

    private void UpdateColor()
    {
        if (_isUpdating) return;
        _isUpdating = true;

        Color newColor = GetCurrentColorFromSliders();
        ApplyColorToDisplay(newColor);
        UpdateHexInputField(newColor);
        UpdateSlidersGradients(newColor);

        ApplyNewColor(newColor);

        _isUpdating = false;
    }

    private Color GetCurrentColorFromSliders()
    {
        float red = channelSliders[0].value / 255f;
        float green = channelSliders[1].value / 255f;
        float blue = channelSliders[2].value / 255f;
        float alpha = Mathf.Clamp(channelSliders[3].value / 255f, 0.5f, 1f);
        return new Color(red, green, blue, alpha);
    }

    private void ApplyColorToDisplay(Color color)
    {
        channelDisplay[currentTarget].color = color;
    }

    private void UpdateHexInputField(Color color)
    {
        hexInputField.text = "#" + ColorUtility.ToHtmlStringRGB(color);
    }

    private void UpdateSlidersGradients(Color color)
    {
        channelSliders[0].GetComponentInChildren<Image>().sprite = GenerateGradientTexture(new Color(0, color.g, color.b, 1f), new Color(1, color.g, color.b, 1f));
        channelSliders[1].GetComponentInChildren<Image>().sprite = GenerateGradientTexture(new Color(color.r, 0, color.b, 1f), new Color(color.r, 1, color.b, 1f));
        channelSliders[2].GetComponentInChildren<Image>().sprite = GenerateGradientTexture(new Color(color.r, color.g, 0, 1f), new Color(color.r, color.g, 1, 1f));
        channelSliders[3].GetComponentInChildren<Image>().sprite = GenerateGradientTexture(new Color(color.r, color.g, color.b, 0.5f), new Color(color.r, color.g, color.b, 1f));
    }

    private void ApplyNewColor(Color newColor)
    {
        if (currentTarget < materials.Count)
        {
            materials[currentTarget].color = newColor;
            EditorUtility.SetDirty(materials[currentTarget]);
            AssetDatabase.SaveAssets();
        }
    }

    public void PassarSelectionRight()
    {
        currentTarget = (currentTarget + 1) % materials.Count;
        playerObject.GetComponent<Renderer>().material = materials[currentTarget];
        UpdateSlidersFromObjectColor();
    }

    public void PassarSelectionLeft()
    {
        currentTarget = (currentTarget - 1 + materials.Count) % materials.Count;
        playerObject.GetComponent<Renderer>().material = materials[currentTarget];
        UpdateSlidersFromObjectColor();
    }

    private void UpdateSlidersFromObjectColor()
    {
        Color currentColor = materials[currentTarget].color;
        channelSliders[0].value = currentColor.r * 255f;
        channelSliders[1].value = currentColor.g * 255f;
        channelSliders[2].value = currentColor.b * 255f;
        channelSliders[3].value = Mathf.Clamp(currentColor.a * 255f, 127.5f, 255);
    }

    private void UpdateAlpha(float value)
    {
        Color currentColor = materials[currentTarget].color;
        currentColor.a = Mathf.Clamp(value / 255f, 0.5f, 1f);
        ApplyNewColor(currentColor);
    }

    private void OnHexInputChanged(string hex)
    {
        if (ColorUtility.TryParseHtmlString(hex, out Color newColor))
        {
            channelSliders[0].value = newColor.r * 255f;
            channelSliders[1].value = newColor.g * 255f;
            channelSliders[2].value = newColor.b * 255f;
            UpdateColor();
        }
        else
        {
            Debug.LogWarning("Cor hexadecimal inválida.");
            hexInputField.text = "#";
        }
    }

    private Sprite GenerateGradientTexture(Color startColor, Color endColor)
    {
        int width = 256;
        Texture2D texture = new Texture2D(width, 1);
        for (int x = 0; x < width; x++)
        {
            float t = x / (float)(width - 1);
            texture.SetPixel(x, 0, Color.Lerp(startColor, endColor, t));
        }
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, width, 1), new Vector2(0.5f, 0.5f));
    }

    public void ReadyButtonRoutine()
    {
        colorPickerPanel.SetActive(false);
        ApplyColorToMaterial();
        //Trocar cena
    }

    private void ApplyColorToMaterial()
    {
        foreach (Material material in materials)
        {
            EditorUtility.SetDirty(material);
        }
        AssetDatabase.SaveAssets();
    }
}
