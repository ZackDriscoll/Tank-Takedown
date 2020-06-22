using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
public class PowerupController : MonoBehaviour
{
    private TankData data;

    public List<Powerup> powerups;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<TankData>();
        powerups = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Powerup> expiredPowerups = new List<Powerup>();

        foreach (Powerup power in powerups)
        {
            power.duration -= Time.deltaTime;

            if (power.duration <= 0)
            {
                expiredPowerups.Add(power);
            }
        }

        foreach (Powerup expiredPower in expiredPowerups)
        {
            expiredPower.OnDeactivate(data);
            powerups.Remove(expiredPower);
        }

        expiredPowerups.Clear();
    }

    public void AddPowerup(Powerup power)
    {
        //Activate the powerup
        power.OnActivate(data);

        //Only keep track of temporary powerups.
        if (!power.isPermanent)
        {
            powerups.Add(power);
        }
    }
}
