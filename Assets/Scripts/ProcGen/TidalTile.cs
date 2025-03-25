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

    private Vector3 m_tileStart = Vector3.zero;
    private Vector3 m_tileEnd = Vector3.zero;


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

    private void Start()
    {
        Vector3 scale = transform.lossyScale;
        m_tileStart = transform.position + new Vector3(-scale.x / 2, 0, scale.z / 2);
        m_tileEnd = transform.position - new Vector3(-scale.x / 2, 0, scale.z / 2);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (TidalGenerator.Instance.debug)
        {
            Debug.DrawRay(new Vector3(m_tileStart.x, transform.position.y, m_tileStart.x), Vector3.up, Color.blue, 0.2f);
            Debug.DrawRay(new Vector3(m_tileEnd.x, transform.position.y, m_tileEnd.x), Vector3.up, Color.red, 0.2f);

            Vector2 tileMagnitude = m_force * m_direction;
            m_tidalText.enabled = true;
            m_tidalText.text = "ID : " + m_tidalTileID.ToString() + "\nMag right : " + tileMagnitude.x.ToString() + "\nMag up : " + tileMagnitude.y.ToString();
        }
        else
        {
            m_tidalText.enabled = false;
        }
    }
#endif
}
