using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    [SerializeField] private CoreConfigurationSO configuration;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private SpriteRenderer coreSpriteRenderer;
    private bool isAboutToExplode = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isAboutToExplode && other.tag.CompareTo("Player") == 0)
        {
            StartCoroutine(ExplosionCountDown());
        }
    }

    private IEnumerator ExplosionCountDown()
    {
        Debug.Log("It is going to explode...");
        isAboutToExplode = true;
        coreSpriteRenderer.color = Color.black;
        yield return new WaitForSeconds(configuration.ExplosionDelay);
        Explode();
    }

    private void Explode()
    {
        Debug.Log("Boom");
        explosionParticleSystem.Play();
        coreSpriteRenderer.enabled = false;
    }
}
