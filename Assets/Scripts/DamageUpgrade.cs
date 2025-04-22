using UnityEngine;

[CreateAssetMenu(fileName = "DamageUpgrade", menuName = "Powerups/DamageUpgrade")]
public class DamageUpgrade : Powerup
{
    public float bonusDamage = 10f;

    public override void Consume(Boat boat)
    {
        if (boat.GetDamageUpgradeLevel() < (int)boat.GetBoatLevel())
        {
            boat.ChangeBonusDamage(bonusDamage);

            boat.SetDamageUpgradeLevel(boat.GetDamageUpgradeLevel() + 1);
        }
    }
}