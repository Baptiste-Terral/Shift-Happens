using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private int _health = 100;

	public void Move(float translation, float rotation)
    {
		// Allows moving 10 meters per second instead of 10 meters per frame
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        // Move the boat
        transform.Translate(0, 0, translation);
        // Rotate the boat
        transform.Rotate(0, rotation, 0);
    }
    
    public void Heal(int amount)
    {
		if (_health + amount > 100)
		{
			_health = 100;
		}
		else
	    {	
	 		_health += amount;
		}
    }

    public void TakeDamage(int amount)
    {
		if (_health - amount <= 0)
	    {
	        _health = 0;
			gameObject.SetActive(false);
		}
		else
		{
        	_health -= amount;
		}
    }

    public int GetHealth()
    {
        return _health;
    }
}
