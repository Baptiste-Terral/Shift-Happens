using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "Powerups/SpeedUpgrade")]
public class SpeedUpgrade : Powerup
{
    public float speedMultiplier = 1.4f;

    public override void Consume(Boat boat)
    {
        if (boat.GetSpeedUpgradeLevel() < (int)boat.GetBoatLevel())
        {
            //boat.ChangeSpeedMultiplier(speedMultiplier);

            boat.SetSpeedUpgradeLevel(boat.GetSpeedUpgradeLevel() + 1);
        }
    }
}