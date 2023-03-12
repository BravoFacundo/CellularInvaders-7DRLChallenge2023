using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private bool isShopPowerUp = false;
    [SerializeField] private int coinCost = 10;
    [SerializeField] private string powerUpType = "Movement";

    [SerializeField] private MovementPowerUpConfigurationSO movementConfiguration;
    private CoinUICounter coinsCounterComponent;

    private void Awake()
    {
        coinsCounterComponent = FindObjectOfType<CoinUICounter>(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.CompareTo("Player") == 0)
        {
            if(isShopPowerUp)
            {
                if(coinsCounterComponent.GetCoins() < coinCost)
                {
                    return;
                }
                else
                {
                    coinsCounterComponent.SubstractCoins(coinCost);
                }
            }

            if(powerUpType.CompareTo("Movement") == 0)
            {
                collision.gameObject.GetComponent<PlayerController>().AddSpeedPowerUp(movementConfiguration);
                Destroy(gameObject);
            }
        }
    }
}
