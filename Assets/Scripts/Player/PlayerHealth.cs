using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    public event Action OnReceiveDamage;
    private readonly PlayerConfigurationSO playerConfiguration;
    private readonly GameObject healthIconPrefab;
    private readonly Transform lifesParentTransform;
    private readonly List<GameObject> activeLifeIcons;
    private readonly List<GameObject> inactiveLifeIcons;

    public PlayerHealth(PlayerConfigurationSO playerConfiguration, GameObject healthIconPrefab, Transform lifesParentTransform)
    {
        this.playerConfiguration = playerConfiguration;
        this.healthIconPrefab = healthIconPrefab;
        this.lifesParentTransform = lifesParentTransform;

        activeLifeIcons = new List<GameObject>((int)playerConfiguration.NumberOfLifes);
        inactiveLifeIcons = new List<GameObject>((int)playerConfiguration.NumberOfLifes);
    }

    public void Initialize()
    {
        for (int i = 0; i < playerConfiguration.NumberOfLifes; i++)
        {
            Vector2 instantiatedPosition = CalculateInstantiatePositionAroundCircle(i);
            InstantiateLifeIcon(instantiatedPosition);
        }
    }

    private Vector2 CalculateInstantiatePositionAroundCircle(int index)
    {
        float angle = (index * Mathf.PI * 2f) / playerConfiguration.NumberOfLifes;
        Vector2 instantiatedPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        instantiatedPosition *= playerConfiguration.LifesCircleRadius;
        return instantiatedPosition;
    }

    private void InstantiateLifeIcon(Vector2 position)
    {
        GameObject lifeIcon = GameObject.Instantiate(healthIconPrefab, position, Quaternion.identity, lifesParentTransform);
        activeLifeIcons.Add(lifeIcon);
    }

    public void Update()
    {
        lifesParentTransform.Rotate(0, 0, playerConfiguration.LifesCircleRotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(int numberOfLifes)
    {
        if(activeLifeIcons.Count > 0)
        {
            int currentLifes = activeLifeIcons.Count - numberOfLifes;

            while(activeLifeIcons.Count > 0 && currentLifes != activeLifeIcons.Count)
            {
                DeactivateOneLifeIcon();
            }

            OnReceiveDamage?.Invoke();
        }
    }

    private void DeactivateOneLifeIcon()
    {
        GameObject lifeIcon = activeLifeIcons[0];
        lifeIcon.SetActive(false);
        activeLifeIcons.RemoveAt(0);
        inactiveLifeIcons.Add(lifeIcon);
    }
}
