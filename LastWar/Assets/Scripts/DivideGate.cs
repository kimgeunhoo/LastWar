using UnityEngine;

public class DivideGate : MonoBehaviour
{
    [SerializeField]
    private int divideValue = 2;

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
            return;

        SquadController squad = other.GetComponent<SquadController>();

        if (squad == null)
            return;

        if (divideValue <= 0)
            return;

        isUsed = true;

        int currentCount = squad.Count;

        int targetCount = Mathf.Max(1, currentCount / divideValue);

        int removeAmount = currentCount - targetCount;

        squad.TakeDamage(removeAmount);

        gameObject.SetActive(false);
    }
}
