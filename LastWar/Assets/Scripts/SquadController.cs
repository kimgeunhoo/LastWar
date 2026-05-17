using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquadController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    private GameObject clonePrefab;

    [SerializeField]
    private Transform playerParent;

    [Header("Formation")]
    [SerializeField]
    private float unitRadius = 0.35f;
    [SerializeField]
    private float formationMoveSpeed = 0.1f;
    [SerializeField]
    private float spacingPadding = 0.1f;

    [Header("Road Limit")]
    [SerializeField]
    private float minLocalX = -3f;
    [SerializeField]
    private float maxLocalX = 3f;

    [Header("Weapon Data")]
    [SerializeField]
    private WeaponData currentWeapon;

    [Header("GameOverUI")]
    [SerializeField]
    private GameOverUI gameOverUI;

    private List<Transform> soldiers = new List<Transform>();
    private List<Vector3> targetLocalPositions = new List<Vector3>();

    public int Count => soldiers.Count;



    private void Start()
    {
        soldiers.Clear();

        foreach (Transform child in transform)
        {
            soldiers.Add(child);
        }

        RefreshFormationTargets();
    }

    private void Update()
    {
        MoveSoldierToFormation();
    }

    public void AddPlayer(int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            Transform inactiveSolider = GetInActiveSoludier();

            if (inactiveSolider != null)
            {
                inactiveSolider.gameObject.SetActive(true);
                soldiers.Add(inactiveSolider);

                continue;
            }
            else
            {
                GameObject clone = Instantiate(clonePrefab, playerParent);
                clone.name = $"Soldier_(soldiers.Count)";
                clone.tag = "Player";

                WeaponController weapon = clone.GetComponentInChildren<WeaponController>();
                if (weapon != null && currentWeapon != null)
                {
                    weapon.EquipWeapon(currentWeapon);
                }

                Rigidbody rb = clone.GetComponent<Rigidbody>();
                if (rb != null)
                    Destroy(rb);

                Collider col = clone.GetComponent<Collider>();
                if (col != null)
                    col.isTrigger = true;

                soldiers.Add(clone.transform);
            }

            //Debug.Log($"[Squad] Clone 생성 / total={soldiers.Count}");
        }

        RefreshFormationTargets();
        //Debug.Log($"[Squad] AddPlayers 완료 / after={soldiers.Count}");
    }

    private Transform GetInActiveSoludier()
    {
        foreach (Transform child in playerParent)
        {
            if (child.gameObject.activeSelf == false)
            {
                return child;
            }
        }
        return null;
    }

    private void RefreshFormationTargets()
    {
        targetLocalPositions.Clear();

        float spacing = unitRadius * 2f + spacingPadding;

        for (int i = 0; i < soldiers.Count; i++)
        {
            Vector3 target = GetCircleFormationLocalPosition(i, spacing);

            target.x = Mathf.Clamp(target.x, minLocalX, maxLocalX);

            targetLocalPositions.Add(target);
        }
    }

    private Vector3 GetCircleFormationLocalPosition(int index, float spacing)
    {
        if (index == 0)
            return Vector3.zero;

        float angle = index * 137.5f * Mathf.Deg2Rad;
        float radius = spacing * Mathf.Sqrt(index);

        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        return new Vector3(x, 0f, z);
    }

    private void MoveSoldierToFormation()
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            soldiers[i].localPosition = Vector3.Lerp(
                soldiers[i].localPosition, targetLocalPositions[i], formationMoveSpeed * Time.deltaTime);
            soldiers[i].localRotation = Quaternion.identity;
        }

    }

    public void TakeDamage(int damage)
    {
        int removeCount = Mathf.Min(damage, soldiers.Count);

        for (int i = 0; i < removeCount; i++)
        {
            Transform target = soldiers[soldiers.Count - 1];

            soldiers.RemoveAt(soldiers.Count - 1);

            target.gameObject.SetActive(false);
        }

        RefreshFormationTargets();

        if (soldiers.Count <= 0)
        {
            gameObject.SetActive(false);
            gameOverUI.ShowGameOver();
        }
    }

    public void EquipWeaponToAll(WeaponData weaponData)
    {
        currentWeapon = weaponData;

        for(int i = 0; i < soldiers.Count;i++)
        {
            WeaponController weapon = soldiers[i].GetComponentInChildren<WeaponController>();

            if (weapon != null)
            {
                weapon.EquipWeapon(weaponData);
            }
        }
    }

    public List<Transform> GetSoldiers()
    {
        return soldiers;
    }

}
