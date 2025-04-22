using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int timer = 0;
    private int numberVague = 0;
    private EnnemyBehaviour ennemyGameObject;
    [SerializeField] private GameObject OceanTerrain;
    [SerializeField] private List<Ennemy> allEnnemyType = new List<Ennemy>();
    private List<EnnemyBehaviour> allEnnemyInScene = new List<EnnemyBehaviour>();
    void Start()
    {
        ennemyGameObject = gameObject.GetComponentInChildren<EnnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
        if (timer%24==0)
        {
            if ((timer/24)%10==0)
            {
                UdapteVague();
            }
        }
    }

    public void UdapteVague()
    {
        numberVague += 1;
        int vagueCost = numberVague;
        while (vagueCost > 0)
        {
            int numEnnemyType = Random.Range(0, allEnnemyType.Count);
            if (vagueCost-allEnnemyType[numEnnemyType].Cost>=0)
            {
                Vector3 size = OceanTerrain.GetComponent<MeshRenderer>().bounds.size;
                Vector3 center = OceanTerrain.GetComponent<MeshRenderer>().bounds.center;
                Vector2 position = ChooseMapRandomPosition((int)center.x,(int)center.z,(int)size.x-10,(int)size.z-10);
                Generate(allEnnemyType[numEnnemyType], position);
                vagueCost = vagueCost - allEnnemyType[numEnnemyType].Cost;
            }
        }

    }
    public void Generate(Ennemy ennemyType, Vector2 position)
    {
        EnnemyBehaviour model = Instantiate(ennemyType.Model, new Vector3(position.x, 0, position.y), Quaternion.identity);
        model.Setup(ennemyType);
        model.transform.SetParent(ennemyGameObject.transform);
        allEnnemyInScene.Add(model);
    }

    public void MoveAllEnnemy()
    {

    }

    public Vector2 ChooseMapRandomPosition(int centerX, int centerY, int width, int height)
    {
        int mapX = centerX - width/2;
        int mapY = centerY - height/2;
        int number = Random.Range(0, width * 2+ height *2-4);
        int x, y;
        if (number< width * 2)
        {
            x = mapX + number % width;
            y = mapY + number / width * height;
        }
        else
        {
            number = number - width*2;
            x = mapX + number / height * width;
            y = mapY + number % height;
        }
        return new Vector2(x, y);
    }
}
