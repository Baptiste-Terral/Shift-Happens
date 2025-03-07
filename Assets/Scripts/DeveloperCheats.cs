using UnityEngine;

public class DeveloperCheats : MonoBehaviour
{
    [SerializeField]
	private Boat _boat;

    void Start()
    {
        
    }
    
    void Update()
    {
		// Soigner le bateau
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            _boat.Heal(10);
            Debug.Log("Boat healed, current health: " + _boat.GetHealth());
        }
		// Endommager le bateau
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            _boat.TakeDamage(10); 
            Debug.Log("Boat damaged, current health: " + _boat.GetHealth());
        }
        
        // Upgrade le bateau
        if (Input.GetKeyDown(KeyCode.U))
        {
            _boat.Upgrade();  
        }
        
        // Reset le bateau
        if (Input.GetKeyDown(KeyCode.R))
        {
            _boat.SetBoatLevel(BoatLevel.LEVEL_1);
        }
    }
}