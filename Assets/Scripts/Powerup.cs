[System.Serializable]
public class Powerup
{
    public float speedModifier;
    public float healthModifier;
    public float fireRateModifier;

    public float duration; //In seconds
    public bool isPermanent;

    public void OnActivate(TankData target)
    {
        target.moveSpeed += speedModifier;
        target.currentHealth += healthModifier;
        target.fireRate += fireRateModifier;
    }

    public void OnDeactivate(TankData target)
    {
        target.moveSpeed -= speedModifier;
        target.currentHealth -= healthModifier;
        target.fireRate -= fireRateModifier;
    }
}
