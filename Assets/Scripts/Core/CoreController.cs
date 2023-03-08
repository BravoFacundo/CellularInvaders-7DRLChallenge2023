using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    [SerializeField] private CoreConfigurationSO configuration;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private SpriteRenderer coreSpriteRenderer;

    [SerializeField] private Rigidbody2D coinPrefab;
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
        coreSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(configuration.ExplosionDelay);
        Explode();
    }

    private void Explode()
    {
        Debug.Log("Boom");
        explosionParticleSystem.Play();
        coreSpriteRenderer.enabled = false;
        SpawnObjects();
        ApplyExplosionDamage();
    }

    private void ApplyExplosionDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, configuration.ExplosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag.CompareTo("Player") == 0)
            {
                colliders[i].gameObject.GetComponent<IDamageable>().TakeDamage(configuration.ExplosionDamage);
            }
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < configuration.NumberOfCoins; i++)
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        Rigidbody2D coinRigidBody = GameObject.Instantiate(coinPrefab, transform.position, Quaternion.identity);
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        Vector2 force = new Vector2(configuration.ExplosionForce * x, configuration.ExplosionForce * y);
        coinRigidBody.AddForce(force, ForceMode2D.Impulse);
    }
}
