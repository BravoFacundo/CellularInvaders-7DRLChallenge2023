using UnityEngine;

public class PowerUpBaseConfigurationSO : ScriptableObject
{
    [SerializeField] private float duration = 5f;
    public float Duration => duration;
}
