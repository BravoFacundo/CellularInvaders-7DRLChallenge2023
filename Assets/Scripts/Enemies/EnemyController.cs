using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private enum EnemyType { Watchman, Sniper, Stalker }
    [SerializeField] private int enemyLevel;
    [SerializeField] private float enemySpeed; private float enemySpeedBackup;
    [SerializeField] private float distanceThreshold = .25f;
    private Vector2 direction;
    private float distance;
    private Rigidbody2D rb;

    [Header("Player")]
    [SerializeField] private Transform playerPos;

    [Header("General")]
    [SerializeField] private bool followTarget;
    private Vector3 targetPos;

    [Header("Watchman")]
    [SerializeField] private Vector3 pointA;
    [SerializeField] private Vector3 pointB;
    [SerializeField] private Sprite[] watchmanSprites = new Sprite[4];

    [Header("Sniper")]
    [SerializeField] private Transform bulletPrefab;
    private Transform bulletParent;
    [SerializeField] private Sprite[] sniperSprites = new Sprite[4];

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySpeedBackup = enemySpeed;

        pointA = transform.GetChild(1).GetChild(0).position;
        pointB = transform.GetChild(1).GetChild(1).position;
        bulletParent = transform.GetChild(2);

        Color newColor = Color.HSVToRGB(0,0, 1f - enemyLevel * 0.1f );

        switch (enemyType)
        {
            case EnemyType.Watchman:
                StartCoroutine(nameof(WatchmanBehaviour));
                transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().sprite = watchmanSprites[0];
                transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color = newColor;
                transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = watchmanSprites[enemyLevel];
                transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color = newColor;
                break;
            case EnemyType.Sniper:
                if (playerPos == null) playerPos = FindObjectOfType<PlayerController>().transform;
                StartCoroutine(nameof(SniperBehaviour));
                transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().sprite = sniperSprites[0];
                transform.GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().color = newColor;
                transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = sniperSprites[enemyLevel];
                transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().color = newColor;
                break;
        }
    }

    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.Watchman:
                enemySpeed = enemySpeedBackup;
                break;

            case EnemyType.Sniper:
                targetPos = playerPos.position;
                enemySpeed = 0;
                break;

            case EnemyType.Stalker:
                targetPos = playerPos.position;
                enemySpeed = enemySpeedBackup;
                break;
        }

        distance = Vector2.Distance(transform.position, targetPos);
        direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        rb.rotation = angle;
    }

    private void FixedUpdate()
    {
        if (followTarget) MoveEnemy(direction);
    }
    void MoveEnemy(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (enemySpeed * Time.deltaTime * direction));
        //rb.AddForce((Vector2)transform.position + (enemySpeed * Time.deltaTime * direction));
    }

    private IEnumerator WatchmanBehaviour()
    {
        targetPos = pointB;
        bool isTargetingB = true;

        while (true)
        {
            if (distance < distanceThreshold)
            {
                targetPos = isTargetingB ? pointA : pointB;
                isTargetingB = !isTargetingB;
            }
            yield return null;
        }
    }

    private IEnumerator SniperBehaviour()
    {
        float waitTime = Random.Range(2f, 4f);
        while (true)
        {
            Transform newBullet = Instantiate(bulletPrefab, bulletParent.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(50f * enemySpeedBackup * direction);
            newBullet.GetComponent<BulletBehaviour>().bulletLevel = enemyLevel;
            newBullet.GetComponent<BulletBehaviour>().ChangeBulletSprite();

            waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
