

using UnityEngine;

public class CanvasMovement : MonoBehaviour
{
    private Camera mainCamera;
    
    private float _heightOffset = 0f;
    
    void Start()
    {
        mainCamera = Camera.main; // Get the main camera
    }
    
    void Update()
    {
        if (!mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);   
        }
    }
    
    public void SetHeightOffset(float heightOffset)
    {
        _heightOffset = heightOffset;
        transform.position = new Vector3(transform.position.x, _heightOffset, transform.position.z);
    }
}