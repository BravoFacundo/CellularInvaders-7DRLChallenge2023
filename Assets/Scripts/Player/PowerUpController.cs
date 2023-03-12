using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController
{
    private class ActivePowerUp
    {
        private PowerUpBaseConfigurationSO powerUp;
        public float lifetimeLeft;
        public string powerUpType;

        public ActivePowerUp(PowerUpBaseConfigurationSO powerUp, string powerUpType)
        {
            this.powerUp = powerUp;
            lifetimeLeft = powerUp.Duration;
            this.powerUpType = powerUpType;
        }
    }

    public event Action<MovementPowerUpConfigurationSO> OnMovementPowerUpActivated;
    public event Action OnMovementPowerUpDeactivated;
    private HashSet<ActivePowerUp> activePowerUps;

    public PowerUpController()
    {
        activePowerUps = new HashSet<ActivePowerUp>();
    }

    public void AddSpeedPowerUp(MovementPowerUpConfigurationSO configuration)
    {
        activePowerUps.RemoveWhere((ActivePowerUp p) => p.powerUpType == "Movement");
        activePowerUps.Add(new ActivePowerUp(configuration, "Movement"));
        OnMovementPowerUpActivated?.Invoke(configuration);
    }

    public void Update(float time)
    {
        foreach(ActivePowerUp powerUp in activePowerUps)
        {
            powerUp.lifetimeLeft -= time;

            if(powerUp.lifetimeLeft <= 0)
            {
                RemovePowerUp(powerUp.powerUpType);
            }
        }

        activePowerUps.RemoveWhere((ActivePowerUp p) => p.lifetimeLeft <= 0);
    }

    private void RemovePowerUp(string powerUpType)
    {
        switch(powerUpType)
        {
            case "Movement":
                OnMovementPowerUpDeactivated?.Invoke();
                break;
        }
    }
}
