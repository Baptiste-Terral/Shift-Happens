using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "Powerups/SpeedUpgrade")]
public class SpeedUpgrade : Powerup
{
    public float speedMultiplier = 1.2f;

    public override void Consume(Boat boat)
    {
        
    }
}