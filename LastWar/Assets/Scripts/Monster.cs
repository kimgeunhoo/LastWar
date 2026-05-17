using System;
using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private MonsterData data;
    [SerializeField]
    private Animator animator;

    private Transform playerTrs;
    private Player player;
    private SquadController squadController;

    private int currentHp;
    private float attackTimer;
    private bool isDead;
    private bool isAttacking;

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
        if (playerObj == null)
        {
            enabled = false;
            return;
        }

        squadController = playerObj.GetComponent<SquadController>();

        if (squadController == null)
            squadController = playerObj.GetComponentInParent<SquadController>();

        if (squadController == null)
            squadController = FindFirstObjectByType<SquadController>();
        //Debug.Log($"[Monster] Player 찾음: {name} -> {player.name}");
    }

    private void Update()
    {
        if (isDead || player == null)
            return;

        float distance = Vector3.Distance(transform.position, playerTrs.position);

        if (distance > data.attackRange)
        {
            TracePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void TracePlayer()
    {

        if (playerTrs == null || player == null)
            return;

        Vector3 dir = (playerTrs.position - transform.position).normalized;

        dir.y = 0;

        transform.position += dir * data.Speed * Time.deltaTime;

        attackTimer += Time.deltaTime;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);

            animator.SetBool("isTracking", true);
        }
        else
        {
            animator.SetBool("isTracking", false);
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
        if (isAttacking)
            return;

        if (playerTrs == null)
            return;

        if (squadController == null)
        {
            Debug.LogWarning("[Monster] squadController가 null이라 공격 불가");
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTrs.position);

        if (distance > data.attackRange)
            return;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(data.attackDelay);

        if (playerTrs == null || squadController == null)
        {
            isAttacking = false;
            yield break;
        }

        float distance = Vector3.Distance(transform.position, playerTrs.position);

        if (distance <= data.attackRange)
        {
            squadController.TakeDamage(data.Damage);
        }

        // 공격 쿨타임
        yield return new WaitForSeconds(data.attackCooldown);

        isAttacking = false;
    }

    public void ResetMonster()
    {
        currentHp = data.Hp;
        isDead = false;
        attackTimer = 0f;
    }

    // 범위 참고용
    //private void OnDrawGizmos()
    //{
    //    if (data != null)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    //    }

    //    if (player != null)
    //    {
    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(transform.position, playerTrs.position);
    //    }
    //}
}
