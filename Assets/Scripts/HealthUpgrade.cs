using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Powerups/HealthUpgrade")]
public class HealthUpgrade : Powerup
{
    public int bonusHealth = 20;

    public override void Consume(Boat boat)
    {
        if (boat.GetHealthUpgradeLevel() < (int)boat.GetBoatLevel())
        {
            boat.ChangeBonusHealth(bonusHealth);
        
            boat.SetHealthUpgradeLevel(boat.GetHealthUpgradeLevel()+1);
        }
    }
}