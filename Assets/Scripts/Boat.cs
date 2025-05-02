using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boat : MonoBehaviour, IDamageable
{
    public float speed = 100.0f;
    public float rotationSpeed = 150f;
    
    private float _health; // Current health
    private float _baseHealth; // Base health
    private float _bonusHealth; // Bonus health
    private float _maxHealth; // Maximum health

    private int _healthLevel; // Health level
    private int _damageLevel; // Damage level
    private int _speedLevel; // Speed level
    
    [SerializeField]
    private HealthBar _healthBar; // Health bar
    
    [SerializeField] private GameObject _cannonballPrefab;     // Prefab avec rigidbody et logique
    [SerializeField] private GameObject _cannonballModel;      // Modele 3D
    [SerializeField] private Transform _firePoint;			   // Point de tir du canon
    [SerializeField] private GameObject _cannonballsContainer; // Conteneur pour les cannonballs
    
    private float _damage; // Damage dealt by the boat
    private float _baseDamage; // Base damage 
    private	float _bonusDamage; // Bonus damage
    
    private BOAT_LEVEL _level;
    private BOAT_LEVEL _newLevel;
    
    private int _healthUpgradeLevel = 0; // Niveau d'amélioration de la santé
    private int _damageUpgradeLevel = 0; // Niveau d'amélioration des dégâts
    private int _speedUpgradeLevel = 0;  // Niveau d'amélioration de la vitesse
    
    private List<BoatComponent> _boatModels = new List<BoatComponent>();

    private Rigidbody rb;
    
    private void Start()
    {
	    _level = BOAT_LEVEL.LEVEL_1;
	    _newLevel = _level;
	    LoadModels();
	    _boatModels.ForEach(model => model.gameObject.SetActive(false));
	    ChangeModel();
	    _health = _maxHealth;
	    UpdateHealthBar();
	    _damage = _baseDamage;
	    
	    rb = GetComponent<Rigidbody>();
	    rb.useGravity = false; // Gravité
		rb.linearDamping = 2.5f; // Résistance pour simuler la friction de l'eau
		rb.angularDamping = 40f; // Résistance à la rotation
    }

    private void Update()
    {
	    // Horizontal and vertical inputs
	    float inputedTranslation = Input.GetAxis("Vertical") * speed;
	    float inputedRotation = Input.GetAxis("Horizontal") * rotationSpeed;

	    Move(inputedTranslation, inputedRotation);
	    
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
		    Shoot();
	    }

	    // Si les upgrades sont suffisantes, on peut passer au niveau suivant
	    if ((int)_level <= 5)
	    {
		    if ((int)_level == _healthUpgradeLevel && (int)_level == _damageUpgradeLevel && (int)_level == _speedUpgradeLevel)
            {
                Upgrade();
            }
	    }
    }

    public void Move(float inputedTranslation, float inputedRotation)
    {
        Vector3 currentAppliedOnBoat = TidalGenerator.Instance.GetMagnitudeOnShip(transform.position);
        
        // Applique une force pour simuler le déplacement
        Vector3 force = transform.forward * inputedTranslation + currentAppliedOnBoat*100;
        rb.AddForce(force, ForceMode.Force);

        // Applique un torque pour la rotation
        Vector3 torque = Vector3.up * inputedRotation;
        rb.AddTorque(torque, ForceMode.Force);
    }
    
    public void Heal(float amount)
    {
		if (_health + amount > _maxHealth)
		{
			_health = _maxHealth;
		}
		else
	    {	
	 		_health += amount;
		}
		
		UpdateHealthBar();
    }
    
    public void ChangeBonusHealth(float amount)
	{
	    _bonusHealth += amount;
	    _health += amount;
	    
	    _healthLevel++;
	    
	    UpdateMaxHealth();
	    
	    UpdateHealthBar();
	}

	public void ChangeBonusDamage(float amount)
	{
		_bonusDamage += amount;
		_damage += amount;
		
		_damageLevel++;
	}

    public void TakeDamage(float amount)
    {
		if (_health - amount <= 0f)
	    {
	        _health = 0f;
			gameObject.SetActive(false);
		}
		else
		{
        	_health -= amount;
		}
		
		UpdateHealthBar();
    }
    
    private void UpdateHealthBar()
    {
	    _healthBar.SetHealth(_health / _maxHealth);
	}

    public float GetHealth()
    {
        return _health;
    }
    
    public void Upgrade()
	{
	    if (_newLevel < BOAT_LEVEL.LEVEL_5)
	    {
		    _newLevel++;
		    ChangeModel();
		    SetHealthUpgradeLevel((int)_level - 1);
		    SetDamageUpgradeLevel((int)_level - 1);
		    SetSpeedUpgradeLevel((int)_level - 1);
	    }
	    else
	    {
		    //Debug.Log("Boat is already at max level");
	    }
	}
    
	private void ChangeModel()
	{
		_boatModels[(int)_level - 1].gameObject.SetActive(false);
		_level = _newLevel;
		_boatModels[(int)_level - 1].gameObject.SetActive(true);

		UpdateBoatStats();
	}
	
	private void LoadModels()
	{
		_boatModels = new List<BoatComponent>();
		_boatModels.AddRange(GetComponentsInChildren<BoatComponent>(true));
	}
	
	public void SetBoatLevel(BOAT_LEVEL type)
	{
		_newLevel = type;
		ChangeModel();
	}

	private void UpdateBoatStats()
	{
		float healthDifference = _maxHealth - _health;
		
		switch(_level)
		{
			case BOAT_LEVEL.LEVEL_1:
				_baseDamage = 10;
				_baseHealth = 100f;
				SetSpeed(30);
				break;
			case BOAT_LEVEL.LEVEL_2:
				_baseDamage = 15;
				_baseHealth = 150f;
				break;
			case BOAT_LEVEL.LEVEL_3:
				_baseDamage = 20;
				_baseHealth = 200f;
				break;
			case BOAT_LEVEL.LEVEL_4:
				_baseDamage = 25;
				_baseHealth = 250f;
				break;
			case BOAT_LEVEL.LEVEL_5:
				_baseDamage = 30;
				_baseHealth = 300f;
				break;
		}
		
		UpdateMaxHealth();
		_health = _maxHealth - healthDifference;
		UpdateHealthBar();
	}

	private void UpdateMaxHealth()
	{
		_maxHealth = _baseHealth + _bonusHealth;
	}

	public BOAT_LEVEL GetBoatLevel()
	{
		return _level;
	}

	public void Shoot()
	{
		GameObject cannonball = Instantiate(_cannonballPrefab, _firePoint.position, Quaternion.identity);
		var script = cannonball.GetComponent<Cannonball>();
		script.SetModel(_cannonballModel); 
		script.SetOwner(transform);
		script.SetDamage(_damage);
		script.GetComponentInChildren<Cannonball>().transform.SetParent(_cannonballsContainer.transform);
	}
	
	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}
	
	public void SetRotationSpeed(float rotationSpeed)
	{
		this.rotationSpeed = rotationSpeed;
	}
	
	public void SetHealthUpgradeLevel(int level)
	{
		_healthUpgradeLevel = level;
	}
	
	public void SetDamageUpgradeLevel(int level)
	{
		_damageUpgradeLevel = level;
	}
	
	public void SetSpeedUpgradeLevel(int level)
	{
		_speedUpgradeLevel = level;
	}
	
	public int GetHealthUpgradeLevel()
	{
		return _healthUpgradeLevel;
	}
	
	public int GetDamageUpgradeLevel()
	{
		return _damageUpgradeLevel;
	}
	
	public int GetSpeedUpgradeLevel()
	{
		return _speedUpgradeLevel;
	}
}
