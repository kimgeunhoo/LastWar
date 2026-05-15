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
        GameObject gate = GatePoolManager.Instance.GetGate();

        float spawnX = Random.value < 0.5f ? leftLineX : rightLineX;

        gate.transform.position = new Vector3(spawnX, spawnY, spawnZ);

        gate.transform.rotation = Quaternion.identity;

        gate.SetActive(true);
    }
}
