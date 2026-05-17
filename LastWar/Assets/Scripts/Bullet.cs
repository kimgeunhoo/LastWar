using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] 
    private float moveSpeed = 20f;
    [SerializeField] 
    private float lifeTime = 3f;
    [SerializeField] 
    private int damage = 1;

    private Vector3 moveDir;
    private float timer;

    public void Init(int damage, float moveSpeed, float lifeTime)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.lifeTime = lifeTime;
    }

    public void FireDir(Vector3 dir)
    {
        if (dir == Vector3.zero)
            dir = Vector3.forward;

        moveDir = dir.normalized;
        timer = 0f;

        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Update()
    {
        Move();
        LifeCycle();
    }

    private void Move()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void LifeCycle()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(damage);
            gameObject.SetActive(false);
            return;
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();

        if (obstacle != null)
        {
            obstacle.TakeHit(damage);
            gameObject.SetActive(false);
            return;
        }

        if (other.CompareTag("Wall"))
        {
            gameObject.SetActive(false);
        }
    }
}

// ÃÑ¾Ë
//[Header("Bullet")]
//[SerializeField]
//private float moveSpeed = 20f;
//[SerializeField]
//private float lifeTime = 3f;
//[SerializeField]
//private int damage = 1;

//private Vector3 moveDir;
//private float timer;

//public void FireDir(Vector3 dir)
//{
//    moveDir = dir.normalized;
//    timer = 0f;

//    transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);

//    gameObject.SetActive(true);
//}

//private void OnEnable()
//{
//    timer = 0f;
//}

//private void Update()
//{
//    Move();
//    LifeCycle();
//}

//private void Move()
//{
//    transform.position += transform.forward * moveSpeed * Time.deltaTime;
//}

//private void LifeCycle()
//{
//    timer += Time.deltaTime;

//    if (timer >= lifeTime)
//    {
//        gameObject.SetActive(false);
//    }
//}