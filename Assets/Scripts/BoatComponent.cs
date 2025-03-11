using UnityEngine;

public class BoatComponent : MonoBehaviour
{
    [SerializeField]
    private CanvasMovement _canvas;
    
    private BOAT_LEVEL _boatLevel;
    
    private float _canvasHeightOffset = 0f;
    
    // Place canvas higher depending on boat level
    void Start()
    {
        _boatLevel = BOAT_LEVEL.LEVEL_1;
        _canvas.transform.position = new Vector3(_canvas.transform.position.x, 1.5f, _canvas.transform.position.z);
    }
    
    void Update()
    {
        // Get boat level from parent
        _boatLevel = transform.parent.GetComponentInParent<Boat>().GetBoatLevel();
        
        switch (_boatLevel)
        {
            case BOAT_LEVEL.LEVEL_1:
                _canvasHeightOffset = 1.5f;
                break;
            case BOAT_LEVEL.LEVEL_2:
                _canvasHeightOffset = 1.5f;
                break;
            case BOAT_LEVEL.LEVEL_3:
                _canvasHeightOffset = 10.5f;
                break;
            case BOAT_LEVEL.LEVEL_4:
                _canvasHeightOffset = 10.5f;
                break;
            case BOAT_LEVEL.LEVEL_5:
                _canvasHeightOffset = 10.5f;
                break;
        }
        
        // Update canvas position
        _canvas.SetHeightOffset(_canvasHeightOffset);
    }
}