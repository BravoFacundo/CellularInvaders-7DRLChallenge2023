using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", fileName = "PlayerConfigurationSO")]
public class PlayerConfigurationSO : ScriptableObject
{
    private const float MAX_SPEED = 100f;
    [Header("Movement")]
    [SerializeField, Range(0, MAX_SPEED)] private float movementSpeed = 16f;
    public float MovementSpeed { get { return MAX_SPEED - movementSpeed; } }

    private const uint MAX_NUMBER_OF_LIFES = 5;
    [Header("Health")]
    [SerializeField, Range(0, MAX_NUMBER_OF_LIFES)] private uint numberOfLifes = 3;
    public uint NumberOfLifes => numberOfLifes;
    [SerializeField] private float lifesCircleRadius = 0.5f;
    public float LifesCircleRadius => lifesCircleRadius;
    [SerializeField, Tooltip("Number of degrees the lifes circle rotates per second")] private float lifesCircleRotationSpeed = 90f;
    public float LifesCircleRotationSpeed => lifesCircleRotationSpeed;
}
