using UnityEngine;

[CreateAssetMenu(menuName = ("CharacterData/Monster"))]
public class MonsterData : Character
{
    public float attackRange = 1.5f;
    public float attackDelay = 1f;
    public float attackCooldown = 1f;
}
