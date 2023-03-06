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

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementComponent = new PlayerMovement(ghostTransform, configuration);
        healthComponent = new PlayerHealth(configuration, healtIconPrefab, lifesParentTransform);
    }

    private void OnEnable()
    {
        healthComponent.OnReceiveDamage += TakeDamageEvents;
    }

    private void OnDisable()
    {
        healthComponent.OnReceiveDamage -= TakeDamageEvents;
    }

    private void TakeDamageEvents()
    {
        AkSoundEngine.SetRTPCValue("RTPC_PlayerLife", healthComponent.GetLifesLeft());
    }

    private void Start()
    {
        healthComponent.Initialize();
    }

    private void Update()
    {
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
