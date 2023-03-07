using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigurationSO configuration;
    private PlayerMovement movementComponent;
    [SerializeField] private Transform ghostTransform;
    private Rigidbody2D rigidbody2D;

    [SerializeField] private GameObject healtIconPrefab;
    [SerializeField] private Transform lifesParentTransform;
    private PlayerHealth healthComponent;

    [SerializeField] private MovementPowerUpConfigurationSO a;
    [SerializeField] private TrailRenderer[] speedPowerUpTrailRenderers;
    private PowerUpController powerUpsComponent; 

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementComponent = new PlayerMovement(ghostTransform, configuration);
        healthComponent = new PlayerHealth(configuration, healtIconPrefab, lifesParentTransform);
        powerUpsComponent = new PowerUpController();
    }

    private void OnEnable()
    {
        healthComponent.OnReceiveDamage += TakeDamageEvents;
        powerUpsComponent.OnMovementPowerUpActivated += MovementPowerUpActivationEvents;
        powerUpsComponent.OnMovementPowerUpDeactivated += MovementPowerUpDeactivationEvents;
    }

    private void OnDisable()
    {
        healthComponent.OnReceiveDamage -= TakeDamageEvents;
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
        if (Input.GetKeyDown(KeyCode.M))
        {
            powerUpsComponent.AddSpeedPowerUp(a);
        }

        powerUpsComponent.Update(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.P))
        {
            healthComponent.TakeDamage(1);
        }

        movementComponent.Update();
        healthComponent.Update();
    }

    private void FixedUpdate()
    {
        rigidbody2D.MovePosition(ghostTransform.position);
        ghostTransform.position = transform.position;
    }
}
