using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Powerup powerup = GetComponent<Powerup>();
            if (powerup != null)
            {
                powerup.Consume(other.gameObject.GetComponentInParent<Boat>());
                Debug.Log("Powerup consumed: " + powerup.name);
                Destroy(gameObject);
            }
        }
    }
}