using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnInterval = 1.5f;

    [Header("Spawn Area")]
    [SerializeField]
    private Collider spawnArea;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        if(spawnArea == null)
        {
            Debug.LogError("[MonsterSpawner] SpawnArea가 비어있음");
            return;
        }

        GameObject monster = MonsterPoolManager.Instance.GetMonster();

        Vector3 spawnPos = GetRandomPointInBounds(spawnArea.bounds);

        monster.transform.position = spawnPos;
        monster.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        Monster monsterScript = monster.GetComponent<Monster>();
        if(monsterScript != null)
        {
            monsterScript.ResetMonster();
        }
        monster.SetActive(true);
    }

    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);   
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, bounds.center.y, randomZ);
    }

    private void OnDrawGizmos()
    {
        if (spawnArea == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
    }
}
