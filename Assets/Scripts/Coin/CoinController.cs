using UnityEngine;

public class CoinController : MonoBehaviour
{
    [Header("Sound stuff")]
    public AK.Wwise.Event playerGetCoin;
    [Header("Conf stuff")]
    private bool hasBeenCollected = false;
    private CoinUICounter coinUICounter;
    private Transform playerTransform;

    private void Awake()
    {
        coinUICounter = FindObjectOfType<CoinUICounter>();
        playerTransform = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        Vector2 directionVector = (Vector2)playerTransform.position - (Vector2)transform.position;
        float sqrDistanceToPlayer = directionVector.sqrMagnitude;
        if (sqrDistanceToPlayer < 4.0f)
            transform.position += (1 / sqrDistanceToPlayer) * Time.deltaTime * (Vector3)directionVector;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenCollected && other.tag.CompareTo("Player") == 0)
        {
            Collect();
        }
    }

    private void Collect()
    {
        hasBeenCollected = true;
        coinUICounter.AddCoins(1);
        playerGetCoin.Post(gameObject);
        Debug.Log("Wwise has played " + playerGetCoin + " from " + gameObject);
        Destroy(this.gameObject);
    }
}
