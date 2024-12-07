using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Linq;

public class SelectionUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CustomRedSlider redSlider;
    [SerializeField] private CustomGreenSlider greenSlider;
    [SerializeField] private CustomBlueSlider blueSlider;
    private SliderInt[] channelSliders;

    [Header("Material Settings")]
    public Material[] materials;
    public Material[] glowingMaterials;
    public int currentTarget;
    public GameObject observer;
    private Observer observerScript;
    private bool _isUpdating;

    public List<Owner> players;

    private void Start()
    {
        observerScript = observer.GetComponent<Observer>();
        players.Clear();
        channelSliders = new SliderInt[] {redSlider.Slider, greenSlider.Slider, blueSlider.Slider};
        UpdateSlidersFromObjectColor();
        AddListeners();
        UpdateColor();
    }

    private void AddListeners()
    {
        for (int i = 0; i < channelSliders.Length; i++)
        {
            int index = i; // To avoid closure issue
            channelSliders[i].RegisterCallback<ChangeEvent<int>>(value => { UpdateColor(); });
        }
    }

    private void UpdateColor()
    {
        if (_isUpdating) return;
        _isUpdating = true;

        Color newColor = GetCurrentColorFromSliders();
        UpdateSlidersGradients(newColor);
        ApplyNewColor(newColor);

        _isUpdating = false;
    }

    private Color GetCurrentColorFromSliders()
    {
        float red = Mathf.Clamp(channelSliders[0].value / 255f * 3f,0,1);
        float green = Mathf.Clamp(channelSliders[1].value / 255f * 3f, 0, 1);
        float blue = Mathf.Clamp(channelSliders[2].value / 255f * 3f, 0, 1);
        return new Color(red, green, blue, 1f);
    }

    private void UpdateSlidersGradients(Color color)
    {
        redSlider.ChangeBackground(GenerateGradientTexture(new Color(0, color.g, color.b, 1f), new Color(1, color.g, color.b, 1f)));
        greenSlider.ChangeBackground(GenerateGradientTexture(new Color(color.r, 0, color.b, 1f), new Color(color.r, 1, color.b, 1f)));
        blueSlider.ChangeBackground(GenerateGradientTexture(new Color(color.r, color.g, 0, 1f), new Color(color.r, color.g, 1, 1f)));
    }

    private void ApplyNewColor(Color newColor)
    {
        if (currentTarget < materials.Length)
        {
            materials[currentTarget].color = newColor;
        }
    }

    public void ChangeSelection(int newTarget)
    {
        currentTarget = newTarget % materials.Length;
        UpdateSlidersFromObjectColor();
    }

    private void UpdateSlidersFromObjectColor()
    {
        Debug.Log(channelSliders);
        Color currentColor = materials[currentTarget].color;
        channelSliders[0].value = (int)(currentColor.r * 255f);
        channelSliders[1].value = (int)(currentColor.g * 255f);
        channelSliders[2].value = (int)(currentColor.b * 255f);
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
        ApplyColorToMaterialGlowingMaterial();
        AddOwnerOnList(Owner.P1);
        AddOwnerOnList(Owner.P2);
        DontDestroyOnLoad(observer);
        players.OrderBy(p => (int)p);
        observerScript.Owners = players;
        SceneManager.LoadScene("Game");
    }

    private void ApplyColorToMaterialGlowingMaterial()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            glowingMaterials[i].SetColor("_EmissionColor", materials[i].color);
        }

    }

    public void AddOwnerOnList(Owner owner)
    {
        if (!players.Contains(owner))
        {
            players.Add(owner);
        }
    }
    public void RemoveOwnerOnList(Owner owner)
    {
        if (players.Contains(owner))
        {
            players.Remove(owner);

        }
    }

}
