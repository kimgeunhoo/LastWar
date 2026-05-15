using System.Collections.Generic;
using UnityEngine;

public class GatePoolManager : MonoBehaviour
{
    public static GatePoolManager Instance;

    [SerializeField] 
    private GameObject[] gatePrefabs;
    [SerializeField] 
    private int poolSize = 20;

    private readonly List<GameObject> gatePool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        gatePool.Clear();

        int half = poolSize / 2;

        for (int i = 0; i < half; i++)
        {
            CreateGate(0);
        }

        for (int i = 0; i < half; i++)
        {
            CreateGate(1);
        }

        ShufflePool();
    }

    private void CreateGate(int index)
    {
        GameObject gate = Instantiate(gatePrefabs[index], transform);
        gate.SetActive(false);
        gatePool.Add(gate);
    }

    private void ShufflePool()
    {
        for (int i = 0; i < gatePool.Count; i++)
        {
            int randomIndex = Random.Range(i, gatePool.Count);

            GameObject temp = gatePool[i];
            gatePool[i] = gatePool[randomIndex];
            gatePool[randomIndex] = temp;
        }
    }

    public GameObject GetGate()
    {
        for (int i = 0; i < gatePool.Count; ++i)
        {
            if (gatePool[i].activeSelf == false)
                return gatePool[i];
        }

        return GetGate();
    }

    
}
