using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    
    private int _health; // Current health
    private int _baseHealth; // Base health
    private int _bonusHealth; // Bonus health
    private int _maxHealth; // Maximum health
    
    private int _damage; // Damage dealt by the boat
    private int _baseDamage; // Base damage 
    private	int _bonusDamage; // Bonus damage
    
    private BoatLevel _level;
    private BoatLevel _newLevel;
    
    private List<GameObject> _boatModels = new List<GameObject>();

    private void Start()
    {
	    _level = BoatLevel.LEVEL_1;
	    _newLevel = _level;
	    LoadModels();
	    _boatModels.ForEach(model => model.SetActive(false));
	    ChangeModel();
	    _health = _maxHealth;
	    _damage = _baseDamage;
    }

    private void Update()
    {
	    // Horizontal and vertical inputs
	    float inputedTranslation = Input.GetAxis("Vertical") * speed;
	    float inputedRotation = Input.GetAxis("Horizontal") * rotationSpeed;

	    Move(inputedTranslation, inputedRotation);
    }

    public void Move(float inputedTranslation, float inputedRotation)
    {
		// Allows moving 10 meters per second instead of 10 meters per frame
		inputedTranslation *= Time.deltaTime;
		inputedRotation *= Time.deltaTime;
        
        // Move the boat
        transform.Translate(0, 0, inputedTranslation);
        // Rotate the boat
        transform.Rotate(0, inputedRotation, 0);
    }
    
    public void Heal(int amount)
    {
		if (_health + amount > _maxHealth)
		{
			_health = _maxHealth;
		}
		else
	    {	
	 		_health += amount;
		}
    }
    
    public void ChangeBonusHealth(int amount)
	{
	    _bonusHealth += amount;
	    _health += amount;
	    
	    UpdateMaxHealth();
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
    
    public void Upgrade()
	{
	    if (_newLevel < BoatLevel.LEVEL_5)
	    {
		    _newLevel++;
		    ChangeModel();
	    }
	    else
	    {
		    Debug.Log("Boat is already at max level");
	    }
	}
    
	private void ChangeModel()
	{
		_boatModels[(int)_level - 1].SetActive(false);
		_level = _newLevel;
		_boatModels[(int)_level - 1].SetActive(true);

		UpdateBoatStats();
	}
	
	private void LoadModels()
	{
		Transform parentTransform = GameObject.Find("Boat").transform;
		foreach (Transform child in parentTransform)
		{
			_boatModels.Add(child.gameObject);
		}
	}
	
	public void SetBoatLevel(BoatLevel type)
	{
		_newLevel = type;
		ChangeModel();
	}

	private void UpdateBoatStats()
	{
		int healthDifference = _maxHealth - _health;
		
		switch(_level)
		{
			case BoatLevel.LEVEL_1:
				_baseDamage = 10;
				_baseHealth = 100;
				break;
			case BoatLevel.LEVEL_2:
				_baseDamage = 15;
				_baseHealth = 150;
				break;
			case BoatLevel.LEVEL_3:
				_baseDamage = 20;
				_baseHealth = 200;
				break;
			case BoatLevel.LEVEL_4:
				_baseDamage = 25;
				_baseHealth = 250;
				break;
			case BoatLevel.LEVEL_5:
				_baseDamage = 30;
				_baseHealth = 300;
				break;
		}
		
		UpdateMaxHealth();
		_health = _maxHealth - healthDifference;
	}

	private void UpdateMaxHealth()
	{
		_maxHealth = _baseHealth + _bonusHealth;
	}
}
