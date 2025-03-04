using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    private int _health = 100;
    
    private BoatType _type;
    private BoatType _newType;
    
    private List<GameObject> _boatModels = new List<GameObject>();

    private void Start()
    {
	    _type = BoatType.LEVEL_1;
	    _newType = _type;
	    LoadModels();
	    _boatModels.ForEach(model => model.SetActive(false));
	    ChangeModel();
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
    
    public void Upgrade()
	{
	    if (_newType < BoatType.LEVEL_5)
	    {
		    _newType++;
		    ChangeModel();
	    }
	    else
	    {
		    Debug.Log("Boat is already at max level");
	    }
	}
    
	private void ChangeModel()
	{
		_boatModels[(int)_type - 1].SetActive(false);
		_type = _newType;
		_boatModels[(int)_type - 1].SetActive(true);
	}
	
	private void LoadModels()
	{
		Transform parentTransform = GameObject.Find("Boat").transform;
		foreach (Transform child in parentTransform)
		{
			_boatModels.Add(child.gameObject);
		}
	}
	
	public void setBoatType(BoatType type)
	{
		_newType = type;
		ChangeModel();
	}
}
