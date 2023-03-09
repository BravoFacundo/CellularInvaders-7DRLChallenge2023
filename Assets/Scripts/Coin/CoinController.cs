using UnityEngine;

public class CoinController : MonoBehaviour
{
    private bool hasBeenCollected = false;
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
        Destroy(this.gameObject);
    }
}
