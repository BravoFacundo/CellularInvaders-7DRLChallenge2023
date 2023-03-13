using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    [Header("Sounds")]
    public AK.Wwise.Event coreCollision;
    public AK.Wwise.Event coreExplosion;

    [Header("Config")]
    [SerializeField] private CoreConfigurationSO configuration;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private SpriteRenderer coreSpriteRenderer;

    [SerializeField] private Rigidbody2D coinPrefab;
    [SerializeField] private Rigidbody2D powerUpPrefab;
    [SerializeField, Range(0f, 1f)] private float powerUpSpawnProbability = 0.3f;
    private bool isAboutToExplode = false;
    private CoreUICounter coreUICounter;

    private void Awake()
    {
        coreUICounter = FindObjectOfType<CoreUICounter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isAboutToExplode && other.tag.CompareTo("Player") == 0)
        {
            coreCollision.Post(gameObject);
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
        coreExplosion.Post(gameObject);
        coreUICounter.AddCore();
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
                colliders[i].gameObject.GetComponent<IDamageable>()?.TakeDamage(configuration.ExplosionDamage);
            }
        }
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < configuration.NumberOfCoins; i++)
        {
            SpawnCoin();
        }

        float randomPowerUp = Random.Range(0, 1f);
        if(randomPowerUp <= powerUpSpawnProbability)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        Rigidbody2D powerUpRigidBody = GameObject.Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        Vector2 force = new Vector2(configuration.ExplosionForce * x, configuration.ExplosionForce * y);
        powerUpRigidBody.AddForce(force, ForceMode2D.Impulse);
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
