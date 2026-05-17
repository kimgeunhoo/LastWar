using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolManager : MonoBehaviour
{
    public static ObstaclePoolManager Instance;

    [SerializeField]
    private GameObject[] obstaclePrefabs;
    [SerializeField]
    private int poolSize = 10;

    private readonly List<GameObject> obstaclePool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        for(int i = 0; i< poolSize; i++)
        {
            CreateRandomObstacle();
        }
    }

    private GameObject CreateRandomObstacle()
    {
        int index = Random.Range(0, obstaclePrefabs.Length);

        GameObject obstacle = Instantiate(
            obstaclePrefabs[index],
            transform
        );

        obstacle.SetActive(false);
        obstaclePool.Add(obstacle);

        return obstacle;
    }

    public GameObject Getobstacle()
    {
        List<GameObject> inactiveObstacles = new List<GameObject>();

        for (int i = 0; i < obstaclePool.Count; i++)
        {
            if(obstaclePool[i].activeSelf == false)
                inactiveObstacles.Add(obstaclePool[i]);
        }

        if (inactiveObstacles.Count > 0)
        {
            return inactiveObstacles[
                Random.Range(0, inactiveObstacles.Count)
            ];
        }

        return CreateRandomObstacle();
    }

}
