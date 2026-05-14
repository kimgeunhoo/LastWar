using System;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float fireRate = 0.15f;

    [SerializeField]
    private Vector3 bulletRotationOffset;

    private float fireTimer;
    private bool isFiring;

    private void Update()
    {
        if (isFiring == false)
            return;

        fireTimer += Time.deltaTime;

        if(fireTimer >= fireRate)
        {
            fireTimer = 0f;

            Fire();
        }
    }

    private void Fire()
    {
        Bullet bullet = BulletPoolManager.Instance.GetBullet();

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation =
           firePoint.rotation * Quaternion.Euler(bulletRotationOffset);

        bullet.FireDir(Vector3.forward);
    }
}
