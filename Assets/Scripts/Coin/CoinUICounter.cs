using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinUICounter : MonoBehaviour
{
    private int currentCoins = 0;
    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void AddCoins(int addition)
    {
        currentCoins += addition;
        UpdateText();
    }

    public void SubstractCoins(int substraction)
    {
        currentCoins -= substraction;

        if(currentCoins < 0)
        {
            currentCoins = 0;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        textComponent.text = currentCoins.ToString();
    }

    public int GetCoins()
    {
        return currentCoins;
    }
}
