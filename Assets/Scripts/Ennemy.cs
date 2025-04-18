using UnityEngine;

[CreateAssetMenu(fileName = "Ennemy", menuName = "Scriptable Objects/Ennemy")]
public class Ennemy : ScriptableObject
{
    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } }
    [SerializeField]
    private float _rotationSpeed;
    public float RotationSpeed { get { return _rotationSpeed; } }
    [SerializeField]
    private int _maxHealth;
    public int MaxHealth { get { return _maxHealth; } }
    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }
    [SerializeField]
    private int _bodyDamage;
    public int BodyDamage { get { return _bodyDamage; } }
    [SerializeField]
    private EnnemyBehaviour _model;
    public EnnemyBehaviour Model { get { return _model; } }
    [SerializeField]
    private int _cost;
    public int Cost { get { return _cost; } }
    [SerializeField]
    private int _level;
    public int Level { get { return _level; } }
}
