using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private bool isShopPowerUp = false;
    [SerializeField] private int coinCost = 10;
    [SerializeField] private string powerUpType = "Movement";

    [SerializeField] private MovementPowerUpConfigurationSO movementConfiguration;
    private CoinUICounter coinsCounterComponent;
    private Transform playerTransform;

    private void Awake()
    {
        coinsCounterComponent = FindObjectOfType<CoinUICounter>(true);
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (isShopPowerUp) return;

        Vector2 directionVector = (Vector2)playerTransform.position - (Vector2)transform.position;
        float sqrDistanceToPlayer = directionVector.sqrMagnitude;
        if (sqrDistanceToPlayer < 4.0f)
            transform.position += (1 / sqrDistanceToPlayer) * Time.deltaTime * (Vector3)directionVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.CompareTo("Player") == 0)
        {
            if (isShopPowerUp)
            {
                if (coinsCounterComponent.GetCoins() < coinCost)
                {
                    return;
                }
                else
                {
                    coinsCounterComponent.SubstractCoins(coinCost);
                }
            }

            if (powerUpType.CompareTo("Movement") == 0)
            {
                collision.gameObject.GetComponent<PlayerController>().AddSpeedPowerUp(movementConfiguration);
                Destroy(gameObject);
            }
        }
    }
}
