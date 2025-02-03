using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject boatModel;

    private Boat _boat;

    void Start()
    {
        _boat = boatModel.GetComponent<Boat>();
    }
    
    void Update()
    {
        // Horizontal and vertical inputs
        float translation = Input.GetAxis("Vertical") * _boat.speed;
        float rotation = Input.GetAxis("Horizontal") * _boat.rotationSpeed;

        _boat.Move(translation, rotation);
    }
}
