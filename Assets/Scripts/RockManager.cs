using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allRockType;
    private List<GameObject> allRockInScene;
    [SerializeField] private GameObject rockGameObject;
    [SerializeField] private GameObject OceanTerrain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 size = OceanTerrain.GetComponent<MeshRenderer>().bounds.size;
        Vector3 center = OceanTerrain.GetComponent<MeshRenderer>().bounds.center;
        Setup((int)center.x, (int)center.z, (int)size.x - 5, (int)size.z - 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Setup(int centerX, int centerY, int width, int height)
    {
        int newWidth = width / 5;
        int newHeight = height / 5;
        for (int i=0;i<width/5;i++)
        {
            for (int j = 0;j<height/5;i++)
            {
                if (i==0 || i== newWidth || j==0 || j== newHeight)
                {
                    generateRock(new Vector2(i*5,j*5));
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
