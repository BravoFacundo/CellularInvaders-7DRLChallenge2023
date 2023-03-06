using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", fileName = "PlayerConfigurationSO")]
public class PlayerConfigurationSO : ScriptableObject
{
    [Header("Movement")]
    private const float MAX_SPEED = 100f;
    [SerializeField, Range(0, MAX_SPEED)] private float movementSpeed = 16f;
    public float MovementSpeed { get { return MAX_SPEED - movementSpeed; } }
}
