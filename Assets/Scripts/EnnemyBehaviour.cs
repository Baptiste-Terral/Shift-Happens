using UnityEngine;

public class EnnemyBehaviour : MonoBehaviour
{
    private Ennemy ennemyData;
    private int _health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Ennemy ennemyData)
    {
        this.ennemyData= ennemyData;
        _health = ennemyData.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


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
