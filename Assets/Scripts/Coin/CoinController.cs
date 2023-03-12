using UnityEngine;

public class CoinController : MonoBehaviour
{
    private bool hasBeenCollected = false;
    private CoinUICounter coinUICounter;

    private void Awake()
    {
        coinUICounter = FindObjectOfType<CoinUICounter>();
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
        Destroy(this.gameObject);
    }
}
