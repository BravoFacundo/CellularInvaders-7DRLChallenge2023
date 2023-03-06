using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigurationSO configuration;
    private PlayerMovement movementComponent;

    private void Awake()
    {
        movementComponent = new PlayerMovement(transform, configuration);
    }

    private void Update()
    {
        movementComponent.Update();
    }
}
