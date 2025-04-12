using UnityEngine;
using TMPro;
using System.Xml;

public class TextMeshProColorModifier : MonoBehaviour
{
    [SerializeField] private static Color unifiedColor = new Color(1f, 0f, 0f); // Piros alapértelmezett szín

    // Gradiens színek
    [SerializeField] private Color topLeftColor = unifiedColor;
    [SerializeField] private Color topRightColor = unifiedColor;
    [SerializeField] private Color bottomLeftColor = unifiedColor;
    [SerializeField] private Color bottomRightColor = unifiedColor;

    // Color Gradient beállítás engedélyezése/letiltása
    [SerializeField] private bool enableColorGradient = false;

    // Color Mode beállítása
    [SerializeField] private ColorMode colorMode = ColorMode.FourCornersGradient;

    // Az összes TextMeshPro objektum színének frissítése
    public void UpdateAllTextColors()
    {
        TextMeshProUGUI[] allTexts = GetComponentsInChildren<TextMeshProUGUI>(true);

        foreach (TextMeshProUGUI text in allTexts)
        {
            if (text == null) continue;

            // Color Gradient engedélyezés/letiltás beállítása
            text.enableVertexGradient = enableColorGradient;

            // Color Mode beállítása
            //text.colorGradient;

            if (enableColorGradient)
            {
                // Színátmenet (gradient) módosítása
                VertexGradient gradient = new VertexGradient();
                gradient.topLeft = topLeftColor;
                gradient.topRight = topRightColor;
                gradient.bottomLeft = bottomLeftColor;
                gradient.bottomRight = bottomRightColor;

                text.colorGradient = gradient;
            }
            else
            {
                // Egyszerű szín módosítása
                text.color = unifiedColor;
            }
        }

        Debug.Log($"TextMeshPro színek módosítva: {allTexts.Length} komponens frissítve.");
    }

    // Color Gradient engedélyezés/letiltás
    public void EnableColorGradient(bool enable)
    {
        enableColorGradient = enable;
        UpdateAllTextColors();
    }

    // Color Mode beállítása
    public void SetColorMode(ColorMode mode)
    {
        colorMode = mode;
        UpdateAllTextColors();
    }

    // Egységes szín beállítása
    public void SetUnifiedColor(Color newColor)
    {
        unifiedColor = newColor;
        UpdateAllTextColors();
    }

    // Gradiens színek beállítása
    public void SetGradientColors(Color topLeft, Color topRight, Color bottomLeft, Color bottomRight)
    {
        topLeftColor = topLeft;
        topRightColor = topRight;
        bottomLeftColor = bottomLeft;
        bottomRightColor = bottomRight;
        UpdateAllTextColors();
    }

    // Egységes gradiens beállítása
    public void SetUniformGradient(Color newColor)
    {
        topLeftColor = topRightColor = bottomLeftColor = bottomRightColor = newColor;
        UpdateAllTextColors();
    }

    void Start()
    {
        UpdateAllTextColors();
    }
}