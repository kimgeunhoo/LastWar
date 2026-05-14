using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField]
    private Bullet bulletPF;
    [SerializeField]
    private int poolSize = 50;

    private List<Bullet> bulletPool = new List<Bullet>();

    private void Awake()
    {
        Instance = this;

        CreatePool();
    }

    private void CreatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPF, transform);

            bullet.gameObject.SetActive(false);

            bulletPool.Add(bullet);
        }
    }

    public Bullet GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].gameObject.activeSelf == false)
                return bulletPool[i];
        }

        Bullet newBullet = Instantiate(bulletPF, transform);

        newBullet.gameObject.SetActive(false);

        bulletPool.Add(newBullet);

        return newBullet;
    }
}
