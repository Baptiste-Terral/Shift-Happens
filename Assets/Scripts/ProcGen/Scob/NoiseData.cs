using UnityEngine;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "Noise", menuName = "Scriptable Objects/Noise")]
public class NoiseData : SerializedScriptableObject
{
    #region filters
    //  #################################################################################
    //  Paramétrage des filtres du bruit    
    //  #################################################################################

    public interface IFilter
    {
        float ApplyFilter(float color);
    }

    public class Power : IFilter
    {
        [Range(0.5f, 10f)]
        public float m_power = 80f;

        public float ApplyFilter(float color)
        {
            return math.pow(color, m_power);
        }
    }

    public class Test2 : IFilter
    {
        public float fixedValue;

        public float ApplyFilter(float color)
        {
            return fixedValue;
        }
    }

    #endregion

    #region noises

    //  #################################################################################
    //  Paramétrage des du bruit  
    //  #################################################################################

    public interface INoise
    {
        float ApplyNoise(float2 coordinates, float seed);
    }

    public class Simplex : INoise
    {
        public float ApplyNoise(float2 coordinates, float seed)
        {
            return math.remap(-1f, 1f, 0f, 1f, noise.snoise(coordinates + seed));
        }
    }

    public class Perlin : INoise
    {
        public float ApplyNoise(float2 coordinates, float seed)
        {
            return math.remap(-1f, 1f, 0f, 1f, noise.cnoise(coordinates + seed));
        }
    }

    public class Cellular : INoise
    {
        public float ApplyNoise(float2 coordinates, float seed)
        {
            return noise.cellular(coordinates + seed).x;
        }
    }

    #endregion

    public enum NOISE_NAME
    {
        FORCE = 0,
        UP = 1,
        RIGHT = 2
    }

    public NOISE_NAME noise_name;

    public INoise noise_type;

    public List<IFilter> noise_filters;

    [HideLabel]
    [PreviewField(400, ObjectFieldAlignment.Center)]
    public Texture2D preview;
}