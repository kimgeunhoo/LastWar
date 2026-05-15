using KevinIglesias;
using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    
    private HumanSoldierController controller;


    private int currentHp;
    private bool isDead;

    private void Awake()
    {
        currentHp = playerData.Hp;
        controller = GetComponent<HumanSoldierController>();
    }


    private void Die()
    {
        isDead = true;
        DieAnimation();
    }

    private IEnumerator DieAnimation()
    {
        yield return null;
        controller.animator.SetTrigger("Death01");
        yield return new WaitForSeconds(1f);
    }
}
