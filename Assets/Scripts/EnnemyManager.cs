using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int timer = 0;
    private int numberVague = 0;
    private int seed = 0;
    private EnnemyBehaviour ennemyGameObject;
    [SerializeField] private List<Ennemy> allEnnemyType = new List<Ennemy>();
    private List<EnnemyBehaviour> allEnnemyInScene = new List<EnnemyBehaviour>();
    void Start()
    {
        ennemyGameObject = gameObject.GetComponentInChildren<EnnemyBehaviour>();
        UnityEngine.Random.InitState(seed);
        //for (int i = 0; i < allEnnemyType.Count; i++)
        //{
            //Vector2 position = ChooseRandomPosition();
            //Generate(allEnnemyType[i],new Vector2(i,i));
        //}
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
        if (timer%24==0)
        {
            if ((timer/24)%30==0)
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
                Vector2 position = new Vector2(1,1);//ChooseRandomPosition();
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
}
