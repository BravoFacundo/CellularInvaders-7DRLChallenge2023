using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PowerUps/Movement PowerUp", fileName = "MovementPowerUpConfiguration")]
public class MovementPowerUpConfigurationSO : PowerUpBaseConfigurationSO
{
    private const float MAX_SPEED_MULTIPLIER = 2f;
    [SerializeField, Range(0, MAX_SPEED_MULTIPLIER)] private float speedMultiplier = 1f;
    public float SpeedMultiplier { get { return MAX_SPEED_MULTIPLIER - speedMultiplier; } }
}