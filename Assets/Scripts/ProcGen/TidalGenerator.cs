using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class TidalGenerator : MonoBehaviour
{
    [SerializeField] private bool m_tidalDebug = false;

    [SerializeField] private TidalGrid m_tidalGrid = default;
    [SerializeField] private MagnitudeGen m_magnitudeGen = default;

    [SerializeField] private int m_gridWidth = 64;
    [SerializeField] private int m_gridHeigth = 64;

    public UnityEvent<List<NoiseMap>> onMagnitudeGen;

    public static TidalGenerator Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public bool debug
    {
        get { return m_tidalDebug; }
    }

    public Vector2 gridSize
    {
        get { return new Vector2(m_gridWidth, m_gridHeigth); }
    }

    void Start()
    {
        m_tidalGrid.Initialize(m_gridWidth, m_gridHeigth);
        m_magnitudeGen.Initialize(m_gridWidth, m_gridHeigth);
    }

    /// <summary>
    /// Get average magnitude applied on the ship !!! BE CAREFUL NOT TO HAVE yMax > yMin OR xMax > xMin !!!
    /// </summary>
    /// <param name="shipPosition">The projection of the ship position on the ocean. BE CAREFUL not to have yMax > yMin or xMax > xMin</param>
    /// <returns></returns>
    public Vector3 GetMagnitudeOnShip(Rect shipPosition)
    {
        return m_tidalGrid.GetMagnitude(shipPosition);
    }
}
