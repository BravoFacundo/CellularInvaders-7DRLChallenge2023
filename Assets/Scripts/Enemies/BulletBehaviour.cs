using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int bulletLevel;
    [SerializeField] private Sprite[] bulletSprites = new Sprite[3];
    [SerializeField] private float lifetime = 3f;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    public void ChangeBulletSprite()
    {
        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = bulletSprites[bulletLevel-1];
        Color newColor = Color.HSVToRGB(0, 0, 1f - bulletLevel * 0.1f);
        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = newColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.CompareTo("Player") == 0)
        {
            collision.GetComponent<IDamageable>().TakeDamage(1);
        }

        Destroy(gameObject);
        //StartCoroutine(DestroyAfterTime(Time.deltaTime * 3f));
    }

    private IEnumerator DestroyAfterTime(float timeInSeconds)
    {
        yield return new WaitForSecondsRealtime(timeInSeconds);
        Destroy(gameObject);
    }
}
