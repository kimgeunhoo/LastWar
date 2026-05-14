using UnityEngine;

[CreateAssetMenu(menuName = "CharacterData/Player")]
public class PlayerData : Character
{
    [SerializeField]
    private float runnerCount;

    public float RunnerCount => runnerCount;


}
