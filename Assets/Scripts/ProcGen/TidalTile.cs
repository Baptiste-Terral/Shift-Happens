using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;

public class TidalTile : MonoBehaviour
{
    [SerializeField] private int m_tidalTileID = 0;
    [SerializeField] private float m_force = 0f;
    [SerializeField] private Vector2 m_direction = Vector2.zero;

#if UNITY_EDITOR
    [SerializeField] private TMP_Text m_tidalText = default;
#endif


    public int ID
    {
        set { m_tidalTileID = value; }
        get { return m_tidalTileID; }
    }
    public float force
    {
        set { m_force = value; }
    }
    public Vector2 direction
    {
        set { m_direction = value; }
    }
    public Vector2 magnitude
    {
        get { return m_force * m_direction; }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (TidalGenerator.Instance.debug)
        {
            Debug.DrawRay(transform.position, new Vector3(m_direction.x, 0, 1f) * 3, Color.magenta, 0.1f);
        }
        else
        {
            m_tidalText.enabled = false;
        }
    }
#endif
}
