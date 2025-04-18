using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float lifetime = 45f;
    
    [SerializeField] private GameObject _modelPrefab;
    
    private Transform _ownerTransform;
    
    public void SetModel(GameObject modelPrefab) => _modelPrefab = modelPrefab;
    public void SetOwner(Transform ownerTransform) => _ownerTransform = ownerTransform;

    void Start()
    {
        // Si un modèle est passé en paramètre
        if (_modelPrefab != null)
        {
            // Instancier le modèle
            GameObject model = Instantiate(_modelPrefab, transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.localRotation = Quaternion.identity;

            // Récupérer le MeshRenderer du modèle instancié
            MeshRenderer modelMeshRenderer = model.GetComponent<MeshRenderer>();
            if (modelMeshRenderer != null)
            {
                // Ajuster le Collider en fonction du modèle
                SphereCollider sphereCollider = GetComponent<SphereCollider>();
                if (sphereCollider != null)
                {
                    // Ajuste le rayon du SphereCollider en fonction de la taille du modèle
                    float radius = modelMeshRenderer.bounds.extents.magnitude;
                    sphereCollider.radius = radius;
                }
            }
        }
        
        // Propulser la cannonball dans sa direction actuelle
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = _ownerTransform.forward * 30f;
            transform.forward = _ownerTransform.forward;
        }

        // Détruire le canonball après une certaine durée
        Destroy(gameObject, lifetime);
    }

    // Gestion de la collision avec d'autres objets (commenté pour l'instant)
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(10);
            }
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }*/
}
