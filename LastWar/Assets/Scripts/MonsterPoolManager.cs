using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolManager : MonoBehaviour
{
    public static MonsterPoolManager Instance;

    [SerializeField]
    private GameObject monsterPF;
    [SerializeField]
    private int poolSize = 20;

    private readonly List<GameObject> monsterPool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject monster = Instantiate(monsterPF, transform);
            monster.SetActive(false);
            monsterPool.Add(monster);
        }

    }

    public GameObject GetMonster()
    {
        for (int i = 0; i < monsterPool.Count; i++)
        {
            if (monsterPool[i].activeSelf == false)
                return monsterPool[i];
        }

        GameObject newMonster = Instantiate(monsterPF, transform);
        newMonster.SetActive(false);
        monsterPool.Add(newMonster);

        return newMonster;
    }

}
