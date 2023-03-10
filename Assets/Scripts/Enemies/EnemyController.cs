using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Configuration")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private enum EnemyType { Watchman , Sniper , Stalker }
    [SerializeField] private int enemyLevel;
    [SerializeField] private float enemySpeed; private float enemySpeedBackup;
    private Vector2 direction;
    private float distance;
    private Rigidbody2D rb;

    [Header("Player")]
    [SerializeField] private Transform playerPos;

    [Header("General")]
    [SerializeField] private bool followTarget;
    private Transform target;

    [Header("Watchman")]
    private Transform pointA;
    private Transform pointB;

    [Header("Sniper")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform bulletParent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySpeedBackup = enemySpeed;

        //pointA = transform.GetChild(1).GetChild(0);
        //pointB = transform.GetChild(1).GetChild(1);
        bulletParent = transform.GetChild(2);
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
                target = playerPos;
                enemySpeed = 0;
                break;

            case EnemyType.Stalker:
                target = playerPos;
                enemySpeed = enemySpeedBackup;
                break;
        }

        distance = Vector2.Distance(transform.position, target.position);
        direction = target.transform.position - transform.position;
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
    }

    private IEnumerator WatchmanBehaviour()
    {
        target = pointB;
        yield return new WaitForSeconds(1f);
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
