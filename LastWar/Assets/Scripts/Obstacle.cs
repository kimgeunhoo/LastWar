using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int maxHitCount = 5;
    [SerializeField]
    private TextMeshProUGUI countText;
    [SerializeField] 
    private int collisionDamage = 3;

    [SerializeField]
    private WeaponData rewardWeapon;

    private int currentHitCount;

    private bool canDamage = true;

    private void OnEnable()
    {
        currentHitCount = maxHitCount;
        UpdateText();
    }

    public void TakeHit(int damage = 1)
    {
        currentHitCount -= damage;
        currentHitCount = Mathf.Max(0, currentHitCount);

        UpdateText();

        if (currentHitCount <= 0)
        {
            BreakObstacle();
        }

    }

    private void UpdateText()
    {
        if (countText != null) 
            countText.text = currentHitCount.ToString();
    }

    private void BreakObstacle()
    {
        if (rewardWeapon != null)
        {
            SquadController squad = FindFirstObjectByType<SquadController>();

            if (squad != null)
            {
                squad.EquipWeaponToAll(rewardWeapon);
            }
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeHit(1);

            other.gameObject.SetActive(false);
            return;
        }

        SquadController squad =
            other.GetComponent<SquadController>();

        if (squad == null)
            squad = other.GetComponentInParent<SquadController>();

        if (squad == null)
            return;

        squad.TakeDamage(collisionDamage);
    }

}
