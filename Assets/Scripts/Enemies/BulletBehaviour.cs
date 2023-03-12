using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
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
