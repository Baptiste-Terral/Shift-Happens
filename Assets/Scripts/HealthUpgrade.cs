using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "Powerups/HealthUpgrade")]
public class HealthUpgrade : Powerup
{
    public int bonusHealth = 20;

    public override void Consume(Boat boat)
    {
        boat.ChangeBonusHealth(bonusHealth);
    }
}