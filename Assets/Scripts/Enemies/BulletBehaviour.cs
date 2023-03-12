using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public int bulletLevel;
    [SerializeField] private Sprite[] bulletSprites = new Sprite[3];

    public void ChangeBulletSprite()
    {
        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = bulletSprites[bulletLevel-1];
        Color newColor = Color.HSVToRGB(0, 0, 1f - bulletLevel * 0.1f);
        transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().color = newColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DestroyAfterTime(Time.deltaTime * 3f));
    }

    private IEnumerator DestroyAfterTime(float timeInSeconds)
    {
        yield return new WaitForSecondsRealtime(timeInSeconds);
        Destroy(gameObject);
    }
}
