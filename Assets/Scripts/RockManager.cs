using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allRockType;
    private List<GameObject> allRockInScene = new List<GameObject>();
    [SerializeField] private GameObject rockGameObject;
    [SerializeField] private GameObject OceanTerrain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 size = OceanTerrain.GetComponent<MeshRenderer>().bounds.size;
        Vector3 center = OceanTerrain.GetComponent<MeshRenderer>().bounds.center;
        Setup((int)center.x, (int)center.z, (int)size.x, (int)size.z);
    }

    void Setup(int centerX, int centerY, int width, int height)
    {
        int mapX = centerX - width / 2;
        int mapY = centerY - height / 2;
        for (int i = 0; i<4; i++)
        {
            if (i/2==0)
            {
                for (int j = mapX; j <= width / 2; j = j + 5)
                {
                    generateRock(new Vector2(j, mapY+height*(i%2)));
                }
            }
            else
            {
                for (int j = mapY; j <= height / 2; j = j + 5)
                {
                    generateRock(new Vector2(mapX + width * (i % 2),j));
                }
            }
        }
    }

    void generateRock(Vector2 position)
    {
        int number = Random.Range(0, allRockType.Count);
        GameObject rock = Instantiate(allRockType[number], new Vector3(position.x, 0, position.y), Quaternion.identity);
        rock.transform.SetParent(rockGameObject.transform);
        allRockInScene.Add(rock);
    }
}
