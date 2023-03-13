using UnityEngine;
using System;

public class PlayerController : MonoBehaviour, IDamageable
{
    public event Action OnPlayerDead;
    [SerializeField] private PlayerConfigurationSO configuration;
    private PlayerMovement movementComponent;
    [SerializeField] private Transform ghostTransform;
    private new Rigidbody2D rigidbody2D;

    [SerializeField] private GameObject healtIconPrefab;
    [SerializeField] private Transform lifesParentTransform;
    private PlayerHealth healthComponent;

    [SerializeField] private TrailRenderer[] speedPowerUpTrailRenderers;
    private PowerUpController powerUpsComponent;

    [SerializeField] private SpriteRenderer cellularRenderer;
    private Color cellularColor;
    private float invulnerabilityCounter = 0f, invulnerabilityTime = 1.25f;
    private bool isInvunerable = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementComponent = new PlayerMovement(ghostTransform, configuration);
        healthComponent = new PlayerHealth(configuration, healtIconPrefab, lifesParentTransform);
        powerUpsComponent = new PowerUpController();
        cellularColor = cellularRenderer.color;

        AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", healthComponent.GetLifesLeft());
    }

    private void OnEnable()
    {
        OnPlayerDead += PlayerController_OnPlayerDead;
        healthComponent.OnReceiveDamage += TakeDamageEvents;
        healthComponent.OnPlayerDead += PlayerDeadEvents;
        powerUpsComponent.OnMovementPowerUpActivated += MovementPowerUpActivationEvents;
        powerUpsComponent.OnMovementPowerUpDeactivated += MovementPowerUpDeactivationEvents;
    }

    private void OnDisable()
    {
        OnPlayerDead -= PlayerController_OnPlayerDead;
        healthComponent.OnReceiveDamage -= TakeDamageEvents;
        healthComponent.OnPlayerDead -= PlayerDeadEvents;
        powerUpsComponent.OnMovementPowerUpActivated -= MovementPowerUpActivationEvents;
        powerUpsComponent.OnMovementPowerUpDeactivated -= MovementPowerUpDeactivationEvents;
    }

    private void TakeDamageEvents()
    {
        AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", healthComponent.GetLifesLeft());
    }

    private void MovementPowerUpActivationEvents(MovementPowerUpConfigurationSO configuration)
    {
        movementComponent.SetMovementSpeedMultiplier(configuration.SpeedMultiplier);
        SetTrailsEmission(true);
    }

    private void MovementPowerUpDeactivationEvents()
    {
        movementComponent.SetMovementSpeedMultiplier(1f);
        SetTrailsEmission(false);
    }

    private void SetTrailsEmission(bool shouldEmit)
    {
        for (int i = 0; i < speedPowerUpTrailRenderers.Length; i++)
        {
            speedPowerUpTrailRenderers[i].emitting = shouldEmit;
        }
    }

    private void Start()
    {
        healthComponent.Initialize();
        SetTrailsEmission(false);
    }

    private void Update()
    {
        powerUpsComponent.Update(Time.deltaTime);
        movementComponent.Update();
        healthComponent.Update();

        // This should be another component, but I'm sleepy as hell, it works
        if (isInvunerable)
        {
            invulnerabilityCounter -= Time.deltaTime;
            if (invulnerabilityCounter <= 0f)
                isInvunerable = false;

            Color transparent = cellularColor;
            transparent.a = 0.45f;
            cellularRenderer.color = transparent;
        }
        else
        {
            cellularRenderer.color = cellularColor;
        }
    }

    public void AddSpeedPowerUp(MovementPowerUpConfigurationSO configuration)
    {
        powerUpsComponent.AddSpeedPowerUp(configuration);
    }

    private void FixedUpdate()
    {
        movementComponent.FixedUpdate();

        rigidbody2D.MovePosition(ghostTransform.position);
        ghostTransform.position = transform.position;
    }

    public void TakeDamage(int damage)
    {
        if (isInvunerable) return;

        healthComponent.TakeDamage(damage);
        isInvunerable = true;
        invulnerabilityCounter = invulnerabilityTime;
    }

    private void PlayerDeadEvents()
    {
        OnPlayerDead?.Invoke();
        Debug.Log("Muerotooo");
    }

    private void PlayerController_OnPlayerDead()
    {
        FindObjectOfType<PauseMenuManager>(includeInactive: true).LoseGame();
    }
}
