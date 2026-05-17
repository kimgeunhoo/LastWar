using Unity.VisualScripting;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 3f;


    [Header("Spawn Position")]
    [SerializeField]
    private float spawnZ = 80f;
    [SerializeField]
    private float spawnY = 0f;
    [SerializeField]
    private float leftLineX = -4f;
    [SerializeField]
    private float rightLineX = 4f;

    private float timer;

    [SerializeField]
    private ObstaclePoolManager obstaclePoolManager;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            timer = 0f;
            SpawnGate();
        }
    }

    private void SpawnGate()
    {
        bool spawnValue = Random.value < 0.5f;
        float gateX = spawnValue ? leftLineX : rightLineX;
        float obstacleX = spawnValue ? rightLineX : leftLineX;

        GameObject gate = GatePoolManager.Instance.GetGate();

        gate.transform.position = new Vector3(gateX, spawnY, spawnZ);
        gate.transform.rotation = Quaternion.identity;
        gate.SetActive(true);

        GameObject obstacle = obstaclePoolManager.Getobstacle();

        obstacle.transform.position = new Vector3(obstacleX, spawnY, spawnZ);
        obstacle.transform.rotation = Quaternion.identity;
        obstacle.SetActive(true);
    }
}
