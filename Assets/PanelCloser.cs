using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloser : MonoBehaviour
{
    [SerializeField] private GameObject panelToClose;

    public void ClosePanel()
    {
        panelToClose.SetActive(false);
    }
}
