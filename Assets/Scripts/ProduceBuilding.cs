using System.Collections;
using UnityEngine;
using TMPro;

public class ProduceBuilding : MonoBehaviour
{
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI amountTextN;
    public TextMeshProUGUI speedTextN;
    public TextMeshProUGUI levelTextN;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //upgrade();
        }
    }
}
