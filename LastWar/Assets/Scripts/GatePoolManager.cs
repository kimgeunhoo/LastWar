using System.Collections.Generic;
using UnityEngine;

public class GatePoolManager : MonoBehaviour
{
    public static GatePoolManager Instance;

    [SerializeField] 
    private GameObject[] gatePrefabs;
    [SerializeField] 
    private int poolSize = 4;

    private readonly List<GameObject> gatePool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        gatePool.Clear();

        for (int i = 0; i < poolSize; i++)
        {
            CreateGate(i);
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
        List<GameObject> inactiveGates = new List<GameObject>();

        for (int i = 0; i < gatePool.Count; ++i)
        {
            if (gatePool[i].activeSelf == false)
            {
                inactiveGates.Add(gatePool[i]);
            }
        }

        if (inactiveGates.Count > 0)
            return inactiveGates[Random.Range(0, inactiveGates.Count)];

        // 부족할때 쓰는 임시코드
        //int randomIndex = Random.Range(0, gatePrefabs.Length);

        //GameObject newGate = Instantiate(gatePrefabs[randomIndex], transform);

        //newGate.SetActive(false);

        //gatePool.Add(newGate);

        return GetGate();
    }

    
}
