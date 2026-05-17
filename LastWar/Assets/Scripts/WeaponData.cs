using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public GameObject weaponPrefab;
    public GameObject bulletPrefab;

    public int damage = 1;
    public float fireInterval = 0.3f;
    public float bulletSpeed = 20f;
}
