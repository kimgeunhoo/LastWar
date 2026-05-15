using System;
using UnityEngine;

public class SquadFireController : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.15f;

    private SquadController squad;
    private float fireTimer;

    private void Awake()
    {
        squad = GetComponent<SquadController>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == false)
        {
            fireTimer = 0f;
            return;
        }

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            FireAllSoldiers();
        }

    }

    private void FireAllSoldiers()
    {
        foreach (Transform soldier in squad.GetSoldiers())
        {
            WeaponFire weapon = soldier.GetComponentInChildren<WeaponFire>();

            if (weapon != null)
            {
                weapon.Fire();
            }
            else
            {
                Debug.LogWarning($"[SquadFire] WeaponFire ¾øÀ½: {soldier.name}");
            }
        }
    }
}
