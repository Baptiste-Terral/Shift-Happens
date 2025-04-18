using System.Collections.Generic;
using UnityEngine;

public class EnnemyManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int timer = 0;
    [SerializeField] private GameObject ennemyGameObject;
    [SerializeField] private List<Ennemy> allEnnemyType = new List<Ennemy>();
    [SerializeField] private List<EnnemyBehaviour> allEnnemyInScene = new List<EnnemyBehaviour>();
    void Start()
    {

        for(int i = 0; i < allEnnemyType.Count; i++)
        {
            //Vector2 position = ChooseRandomPosition();
            Generate(allEnnemyType[i],new Vector2(i,i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
        if (timer%24==0)
        {
            if ((timer/24)%60==0)
            {
                print("test");
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
