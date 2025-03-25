using System.Collections.Generic;

using IFilter = NoiseData.IFilter;
using NOISE_NAME = NoiseData.NOISE_NAME;
using INoise = NoiseData.INoise;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Profiling;
using System.Collections;


public class NoiseMap
{
    private NoiseData m_data;
    private float[,] m_colors = new float[0, 0];
    private float m_scale = 0f;
    private int m_seed = 0;

    // Garbage management :
    private float m_color = 0f;
    private Texture2D m_preview = new Texture2D(0, 0);

    public NOISE_NAME name
    {
        get { return m_data.noise_name; }
    }

    public float[,] noiseMap
    {
        get { return m_colors; }
    }

    public NoiseMap (NoiseData datas, float noiseScale)
    {
        m_data = datas;
        m_scale = noiseScale;
    }

    public void Generate(int width, int heigth)
    {
        m_seed = System.DateTime.Now.Millisecond
           ^ System.Guid.NewGuid().GetHashCode()
           ^ UnityEngine.Random.Range(int.MinValue, int.MaxValue);


        m_colors = new float[width, heigth];
        m_preview = new Texture2D(width, heigth);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {

                float2 coordsOffseted = new float2(x, y);
                float2 coords = new float2(coordsOffseted.x / m_scale, coordsOffseted.y / m_scale);

                m_color = m_data.noise_type.ApplyNoise(coords, m_seed % 10000);
                m_color = math.remap(-1f, 1f, 0f, 1f, m_color);

                for (int filterIndex = 0; filterIndex < m_data.noise_filters.Count; filterIndex++)
                {
                    m_color = m_data.noise_filters[filterIndex].ApplyFilter(m_color);
                }

                m_colors[x, y] = m_color;
                m_preview.SetPixel(x, y, Color.white * m_color);
            }
        }
        m_preview.Apply();
        m_data.preview = m_preview;
    }

    public IEnumerator UpdateMap(Vector2 windOffset)
    {
        int iterations = 0;
        int width = m_colors.GetLength(0);
        int heigth = m_colors.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                iterations++;
                float2 coordsOffseted = new float2(x + windOffset.x, y + windOffset.y);
                float2 coords = new float2(coordsOffseted.x / m_scale, coordsOffseted.y / m_scale);

                m_color = m_data.noise_type.ApplyNoise(coords, m_seed % 10000);
                m_color = math.remap(-1f, 1f, 0f, 1f, m_color);

                for (int filterIndex = 0; filterIndex < m_data.noise_filters.Count; filterIndex++)
                {
                    m_color = m_data.noise_filters[filterIndex].ApplyFilter(m_color);
                }

                m_colors[x, y] = m_color;

                if (iterations % ((width * heigth) / 5f) == 0) yield return null;
            }
        }
    }
}
