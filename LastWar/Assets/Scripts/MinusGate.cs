using UnityEngine;

public class MinusGate : MonoBehaviour
{
    [SerializeField]
    private int MinusCount = 1;
    //[SerializeField]
    //private float validDistance = 2.5f;

    private bool isUsed;

    private void OnEnable()
    {
        isUsed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isUsed)
            return;

        if (other.CompareTag("Player") == false)
        {
            //Debug.Log($"[CloneGate] Ignore: {other.name}, tag={other.tag}");
            return;
        }

        SquadController player = other.GetComponent<SquadController>();

        if (player == null)
        {
            //squad = FindFirstObjectByType<SquadController>();
            //Debug.LogWarning($"[CloneGate] SquadController ¾øÀ½: other={other.name}, root={other.transform.root.name}");
            return;
        }
        isUsed = true;

        //Debug.Log($"[CloneGate] Add={addCount}, Before={squad.Count}");

        isUsed = true;

        player.TakeDamage(MinusCount);

        gameObject.SetActive(false);
    }
}
