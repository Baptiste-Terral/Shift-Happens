using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float damageAmount = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si l'objet qui entre en collision a un composant qui peut prendre des dégâts
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount);
        }
    }
}