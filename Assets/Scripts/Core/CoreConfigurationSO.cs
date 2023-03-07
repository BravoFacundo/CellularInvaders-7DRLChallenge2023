using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Core", fileName = "CoreConfiguration")]
public class CoreConfigurationSO : ScriptableObject
{
    [SerializeField] private int numberOfCoins = 1;
    public int NumberOfCoins => numberOfCoins;
    [SerializeField, Range(0, 100)] private int powerUpProbability = 30;
    public int PowerUpProbability => powerUpProbability;
    [SerializeField] private float explosionDelay = 1.5f;
    public float ExplosionDelay => explosionDelay;
    [SerializeField] private float explosionForce = 10f;
    public float ExplosionForce => explosionForce;
}
