using UnityEngine;

[CreateAssetMenu(fileName = "Ennemy", menuName = "Scriptable Objects/Ennemy")]
public class Ennemy : ScriptableObject
{
    private float _speed;
    public float Speed { get { return _speed; } }
    private float _rotationSpeed;
    public float RotationSpeed { get; set; }
    private float _health;
    public float Health;
    private float _maxHealth;
    public float MaxHealth;
    private float _damage;
    public float Damage;
    private float _bodyDamage;
    public float BodyDamage;
    private GameObject _player;
    public GameObject Player;
    private GameObject _model;
    public GameObject Model;
    private int _level;
    public int Level;

    /*public void Move(float translation, float rotation)
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
    }*/
}
