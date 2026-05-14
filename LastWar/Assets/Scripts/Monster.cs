using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData data;

    private Transform playerTrs;
    private Player player;

    private int currentHp;
    private float attackTimer;
    private bool isDead;

    private void Awake()
    {
        currentHp = data.Hp;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            playerTrs = playerObj.transform;
            player = playerObj.GetComponent<Player>();
        }
    }

    private void Update()
    {
        if (isDead || player == null)
            return;

        ChasePlayer();
        AttackPlayer();
    }

    private void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, playerTrs.position);

        if (distance < data.attackRange)
            return;

        attackTimer += Time.deltaTime;

        if (attackTimer >= data.attackCooldown)
        {
            attackTimer = 0f;

            if(player != null)
                player.TakeDamage(data.Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHp -= damage;

        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;

        gameObject.SetActive(false);
    }

    private void AttackPlayer()
    {
        throw new NotImplementedException();
    }
}
