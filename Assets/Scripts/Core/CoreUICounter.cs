using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoreUICounter : MonoBehaviour
{
    private int currentCores = 0;
    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = "0";
    }

    public void AddCore()
    {
        currentCores++;
        UpdateText();
    }

    public void SubstractCoins(int substraction)
    {
        currentCores -= substraction;

        if (currentCores < 0)
        {
            currentCores = 0;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        textComponent.text = currentCores.ToString();
    }
}
