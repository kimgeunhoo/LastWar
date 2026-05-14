using UnityEngine;

public class Character : ScriptableObject
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float speed;

    public int Hp => hp;
    public int Damage => damage;
    public float Speed => speed;
}
