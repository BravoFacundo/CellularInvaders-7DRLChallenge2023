using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigurationSO configuration;
    private PlayerMovement movementComponent;

    [SerializeField] private GameObject healtIconPrefab;
    [SerializeField] private Transform lifesParentTransform;
    private PlayerHealth healthComponent;

    private void Awake()
    {
        movementComponent = new PlayerMovement(transform, configuration);
        healthComponent = new PlayerHealth(configuration, healtIconPrefab, lifesParentTransform);
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
}
