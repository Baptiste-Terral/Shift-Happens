using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = Unity.Mathematics.Random;

public class MagnitudeGen : MonoBehaviour 
{
    [SerializeField]
    private int m_seed;


    [SerializeField, Range(1f, 20f)]
    private float m_noiseScale = 1f;

    [SerializeField]
    private List<NoiseData> m_noises = new List<NoiseData>();


    private List<NoiseMap> m_noiseMaps = new List<NoiseMap>();

    private int m_width = 0;
    private int m_heigth = 0;

    private void Generate()
    {
        StopAllCoroutines();
        StartCoroutine(CrtGenerate(m_width, m_heigth));
    }

    private IEnumerator CrtGenerate(int width, int heigth)
    {

        int interationSteps = Mathf.RoundToInt((width * heigth) / 100f);

        foreach (NoiseMap noise in m_noiseMaps)
        {
            noise.Generate(width, heigth);
            yield return false;
        }

        TidalGenerator.Instance.onMagnitudeGen.Invoke(m_noiseMaps);
    }

    public void Initialize(int width, int heigth)
    {
        foreach (NoiseData noise in m_noises)
        {
            m_noiseMaps.Add(new NoiseMap(noise, m_noiseScale));
        }

        m_width = width;
        m_heigth = heigth;

        Generate();
    }
}
