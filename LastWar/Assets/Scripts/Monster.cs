using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData data;

    private Transform playerTrs;
    private Player player;
    private SquadController squadController;

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
            player = playerObj.GetComponentInChildren<Player>();
        }

        //Debug.Log($"[Monster] Player Ã£À½: {name} -> {player.name}");
    }

    private void Update()
    {
        if (isDead || player == null)
            return;

        float distance = Vector3.Distance(transform.position, playerTrs.position);

        if (distance > data.attackRange)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector3 dir = (playerTrs.position - transform.position).normalized;

        dir.y = 0;

        transform.position += dir * data.Speed * Time.deltaTime;

        attackTimer += Time.deltaTime;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
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
        float distance = Vector3.Distance(transform.position, playerTrs.position);

        if (distance > data.attackRange)
            return;

        attackTimer += Time.deltaTime;

        if(attackTimer >= data.attackCooldown)
        {
            attackTimer = 0f;

            if(player != null)
                squadController.TakeDamage(data.Damage);
        }

    }

    public void ResetMonster()
    {
        currentHp = data.Hp;
        isDead = false;
        attackTimer = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnDrawGizmos()
    {
        if (data != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, data.attackRange);
        }

        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, playerTrs.position);
        }
    }
}
