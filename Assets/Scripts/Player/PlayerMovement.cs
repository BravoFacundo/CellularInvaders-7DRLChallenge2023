using UnityEngine;

public class PlayerMovement
{
    private readonly PlayerConfigurationSO playerConfiguration;
    private readonly Transform playerTransform;
    private float movementSpeedMultiplier = 1f;

    private Vector2 playerPosition;

    public PlayerMovement(Transform playerTransform, PlayerConfigurationSO playerConfiguration)
    {
        this.playerTransform = playerTransform;
        this.playerConfiguration = playerConfiguration;
    }

    public void Update()
    {
        Vector2 mouseWorldPosition = CalculateMousePositionInWorldSpace();
        Vector2 interpolatedPosition = CalculateInterpolatedPosition(mouseWorldPosition);
        playerPosition = interpolatedPosition;
    }

    public void FixedUpdate()
    {
        playerTransform.position = playerPosition;
    }

    public void SetMovementSpeedMultiplier(float newValue)
    {
        movementSpeedMultiplier = newValue;
    }

    private Vector2 CalculateMousePositionInWorldSpace()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;

        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private Vector2 CalculateInterpolatedPosition(Vector2 mouseWorldPosition)
    {
#if UNITY_EDITOR
        float speedTweak = 1.0f;
#else
        float speedTweak = 0.25f;
#endif
        Vector2 velocity = Vector2.zero;
        return Vector2.SmoothDamp(playerTransform.position, mouseWorldPosition, ref velocity, Time.deltaTime * speedTweak * playerConfiguration.MovementSpeed * movementSpeedMultiplier);
    }
}
