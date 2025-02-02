using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    
    [SerializeField]
    private GameObject boatModel;
    
    // Update is called once per frame
    void Update()
    {
        // Horizontal and vertical inputs
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        
        // Allows moving 10 meters per second instead of 10 meters per frame
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        // Move the boat
        boatModel.transform.Translate(0, 0, translation);
        // Rotate the boat
        boatModel.transform.Rotate(0, rotation, 0);
    }
}