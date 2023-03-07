using UnityEngine;

public class PlayerMovement
{
    private readonly PlayerConfigurationSO playerConfiguration;
    private readonly Transform playerTransform;
    private float movementSpeedMultiplier = 1f;

    public PlayerMovement(Transform playerTransform, PlayerConfigurationSO playerConfiguration)
    {
        this.playerTransform = playerTransform;
        this.playerConfiguration = playerConfiguration;
    }

    public void Update()
    {
        Vector2 mouseWorldPosition = CalculateMousePositionInWorldSpace();
        Vector2 interpolatedPosition = CalculateInterpolatedPosition(mouseWorldPosition);
        playerTransform.position = interpolatedPosition;
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
        Vector2 velocity = Vector2.zero;
        return Vector2.SmoothDamp(playerTransform.position, mouseWorldPosition, ref velocity, Time.deltaTime * playerConfiguration.MovementSpeed * movementSpeedMultiplier);
    }
}
