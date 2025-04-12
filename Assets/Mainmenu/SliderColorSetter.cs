using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColorSetter : MonoBehaviour
{
    public ColorPreset colorPreset;

    public Image background;
    public Image fill;
    public Image handle;

    void Start()
    {
        if (colorPreset != null)
        {
            //if (background != null) background.color = colorPreset.backgroundColor;
            if (background != null)
            {
                background = colorPreset.backgroundImage;
                background.color = Color.white; // fontos, hogy a kép rendesen látszódjon!
            }
            if (fill != null) fill.color = colorPreset.fillColor;
            if (handle != null) handle.color = colorPreset.handleColor;
        }
    }
}
