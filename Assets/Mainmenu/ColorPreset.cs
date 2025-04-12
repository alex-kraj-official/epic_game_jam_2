using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewColorPreset", menuName = "UI/Color Preset")]
public class ColorPreset : ScriptableObject
{
    //public Color backgroundColor;
    public Image backgroundImage;
    public Color fillColor;
    public Color handleColor;
}
