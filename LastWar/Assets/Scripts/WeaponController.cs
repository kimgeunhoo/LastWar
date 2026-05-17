using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Transform weaponHolder;

    private WeaponData currentWeapon;
    private GameObject currentWeaponObj;

    public WeaponData CurrentWeapon => currentWeapon;

    public void EquipWeapon(WeaponData weaponData)
    {
        if (weaponData == null)
            return;

        currentWeapon = weaponData;

        if (currentWeapon != null)
            Destroy(currentWeaponObj);

        if (weaponData.weaponPrefab != null)
        {
            currentWeaponObj = Instantiate(weaponData.weaponPrefab, weaponHolder);

            currentWeaponObj.transform.localPosition = Vector3.zero;
            currentWeaponObj.transform.localRotation = Quaternion.identity;
        }
    }

}
