using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using NOISE_NAME = NoiseData.NOISE_NAME;

public class TidalGrid : MonoBehaviour
{

    [SerializeField] private GameObject TidalTileReference = default;

    [SerializeField] private GameObject OceanTerrain = default;

    private Vector2 m_oceanSize = Vector2.zero;
    private Vector2 m_tileSize = Vector2.zero;
    private Vector2 m_oceanStartPoint = Vector2.zero;

    private Dictionary<int, TidalTile> m_grid = new Dictionary<int, TidalTile>();

    private Texture2D m_oceanBuffer;

    // Garbage management
    private int m_id = 0;
    private float m_force = 0f;
    private float m_right = 0f;
    private float m_up = 0f;

    // Debug
    [SerializeField] private GameObject m_debugTidalTileReference = default;


    private void AddTile(Vector2 coordinates, int newId)
    {
        float x = m_oceanStartPoint.x + ((coordinates.x * m_tileSize.x) + (m_tileSize.x / 2));
        float y = OceanTerrain.transform.position.y;
        float z = m_oceanStartPoint.y - ((coordinates.y * m_tileSize.y) + (m_tileSize.y / 2));
#if UNITY_EDITOR
        GameObject tile = Instantiate(m_debugTidalTileReference, new Vector3(x, y, z), Quaternion.identity, transform);
#else
        GameObject tile = Instantiate(TidalTileReference, new Vector3(x, y, z), Quaternion.identity, transform);
#endif
        tile.transform.localScale = new Vector3(m_tileSize.x, 0.1f, m_tileSize.y);
        tile.GetComponent<TidalTile>().ID = newId;


        m_grid.Add(newId, tile.GetComponent<TidalTile>());
    }

    private void RemoveTile(int id)
    {
        m_grid.Remove(id);
    }

    private void UpdateTile(int id, float noiseForce, float noiseRight, float noiseUp)
    {
        TidalTile tile = m_grid[id];

        tile.force = noiseForce;
        tile.direction = new Vector2(noiseRight, noiseUp);
    }

    private void CreateTileSet(int width, int heigth)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                AddTile(new Vector2(x, y), x * width + y);
            }
        }
    }

    public void UpdateGrid(List<NoiseMap> noises)
    {
        Debug.Log("Update Grid");
        int width = noises[0].noiseMap.GetLength(0);
        int heigth = noises[0].noiseMap.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                Color bufferColor = new Color();

                foreach(NoiseMap map in noises)
                {
                    switch (map.name)
                    {
                        case NOISE_NAME.FORCE:
                            m_force = map.noiseMap[x, y];
                            bufferColor.r = m_force;
                            break;

                        case NOISE_NAME.RIGHT:
                            m_right = map.noiseMap[x, y];
                            bufferColor.g = m_right;
                            break;

                        case NOISE_NAME.UP:
                            m_up = map.noiseMap[x, y];
                            bufferColor.b = m_up;
                            break;
                    }
                }

                m_oceanBuffer.SetPixel(x, y, bufferColor);
                UpdateTile(x * noises[0].noiseMap.GetLength(0) + y, m_force, m_right, m_up);
            }
        }

        m_oceanBuffer.Apply();
    }

    public void Initialize(int width, int heigth)
    {
        m_oceanSize.x = OceanTerrain.transform.lossyScale.x;
        m_oceanSize.y = OceanTerrain.transform.lossyScale.z;

        m_tileSize = m_oceanSize / TidalGenerator.Instance.gridSize;

        m_oceanStartPoint = new Vector2(OceanTerrain.transform.position.x - m_oceanSize.x / 2,
                                        OceanTerrain.transform.position.z + m_oceanSize.y / 2);

        CreateTileSet(width, heigth);

        TidalGenerator.Instance.onMagnitudeGen.AddListener(UpdateGrid);

        m_oceanBuffer = new Texture2D(width, heigth);
        Shader.SetGlobalTexture("g_oceanBuffer", m_oceanBuffer);
    }



    public Vector3 GetMagnitude(Rect shipPosition)
    {
        Vector2 sumMagnitudes = Vector2.zero;
        int denominator = 0;

        for (int x = (int)((shipPosition.xMin - m_oceanStartPoint.x)/m_tileSize.x); x < (int)((shipPosition.xMax - m_oceanStartPoint.x) / m_tileSize.x); x++)
        {
            for (int y = (int)((shipPosition.yMin - m_oceanStartPoint.y) / m_tileSize.y); y < (int)((shipPosition.yMax - m_oceanStartPoint.y) / m_tileSize.y); y++)
            {
                sumMagnitudes += m_grid[x * (int)TidalGenerator.Instance.gridSize.y + y].GetComponent<TidalTile>().magnitude;
                denominator++;
            }
        }

        return new Vector3(sumMagnitudes.x, 0, sumMagnitudes.y) / denominator;
    }
}
