using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] private GameObject[] powerupPrefabs;  // Les différents types de powerups
    [SerializeField] private float spawnInterval = 5f;      // Intervalle entre chaque spawn
    [SerializeField] private float spawnRange = 30f;        // Distance maximale du joueur pour le spawn

    private void Start()
    {
        // Lancement de la génération de powerups en boucle
        InvokeRepeating("SpawnPowerup", 0f, spawnInterval);
    }

    void SpawnPowerup()
    {
        // Génération d'une position aléatoire autour du joueur
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0f,
            Random.Range(-spawnRange, spawnRange)
        );
        
        // Sélection d'un powerup aléatoire parmi les prefabs
        int randomIndex = Random.Range(0, powerupPrefabs.Length);
        GameObject powerupPrefab = powerupPrefabs[randomIndex];

        // Instanciation du powerup à la position générée
        Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
    }
}