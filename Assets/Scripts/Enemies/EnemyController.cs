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

    [Header("Sniper")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform bulletParent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySpeedBackup = enemySpeed;

        pointA = transform.GetChild(1).GetChild(0).position;
        pointB = transform.GetChild(1).GetChild(1).position;
        bulletParent = transform.GetChild(2);

        // Not the best option, but it works at this moment
        if (playerPos == null)
            playerPos = FindObjectOfType<PlayerController>().transform;
    }

    void Start()
    {
        switch (enemyType)
        {
            case EnemyType.Watchman:
                StartCoroutine(nameof(WatchmanBehaviour));
                break;
            case EnemyType.Sniper:
                StartCoroutine(nameof(SniperBehaviour));
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

            waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
