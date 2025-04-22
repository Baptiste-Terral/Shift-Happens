using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnnemyBehaviour : MonoBehaviour
{
    private Ennemy ennemyData;
    private int _health;
    private Rigidbody rb;
    [SerializeField] private Boat player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Ennemy ennemyData)
    {
        this.ennemyData= ennemyData;
        _health = ennemyData.MaxHealth;
        player = GameObject.FindObjectsOfType<Boat>()[0];

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Gravité
        rb.linearDamping = 2.5f; // Résistance pour simuler la friction de l'eau
        rb.angularDamping = 40f; // Résistance à la rotation
    }

    // Update is called once per frame
    void Update()
    {
        if (ennemyData!=null) {
            Move();
        }
    }

    public static float calculateAngle(Vector3 pointA, Vector3 pointB, Vector3 pointC, Vector3 normal)
    {
        Vector3 vectorBA = (pointA - pointB).normalized;
        Vector3 vectorBC = (pointC - pointB).normalized;

        float dotProduct = Vector3.Dot(vectorBA, vectorBC);
        float angleRadians = Mathf.Acos(dotProduct);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        Vector3 crossProduct = Vector3.Cross(vectorBA, vectorBC);
        float sign = Vector3.Dot(crossProduct, normal) < 0 ? -1f : 1f;

        return angleDegrees * sign;
    }

    /*public static float calculateAngle(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
        Vector3 vectorAB = (pointB - pointA).normalized;
        Vector3 vectorBC = (pointC - pointB).normalized;
        float dotProduct = Vector3.Dot(vectorAB, vectorBC);
        float angleRadians = Mathf.Acos(dotProduct);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        return angleDegrees;
    }*/

    public static Vector3 calculatePoint(Vector3 startPoint, Quaternion rotation, float distance)
    {
        Vector3 forward = rotation * Vector3.forward;
        Vector3 newPoint = startPoint + forward * distance;
        return newPoint;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<Cannonball>())
        {
            Destroy(collision.gameObject, 0);
            Destroy(this.gameObject, 0);
        }
    }



    public void Move()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 newPoint = calculatePoint(transform.position, transform.rotation, distance);
        float rotation = calculateAngle(newPoint, transform.position, player.transform.position, Vector3.up);

        // Horizontal and vertical inputs
        float inputedTranslation = 1 * ennemyData.Speed;
        float inputedRotation = rotation * ennemyData.RotationSpeed;
        /*if (rotationLeft<= rotationRight)
        {
            inputedRotation = rotationLeft * ennemyData.RotationSpeed;
        }
        else
        {
            inputedRotation = rotationRight * ennemyData.RotationSpeed;
        }*/

        Vector3 currentAppliedOnEnnemy = TidalGenerator.Instance.GetMagnitudeOnShip(transform.position);

        // Applique une force pour simuler le déplacement
        Vector3 force = transform.forward * inputedTranslation + currentAppliedOnEnnemy * 100;
        rb.AddForce(force, ForceMode.Force);

        // Applique un torque pour la rotation
        Vector3 torque = Vector3.up * inputedRotation;
        rb.AddTorque(torque, ForceMode.Force);
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
