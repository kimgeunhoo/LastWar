using UnityEngine;


public class MultiplyGate : MonoBehaviour
{
    [SerializeField]
    private int multiplyValue = 2;

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

        isUsed = true;

        int currentCount = squad.Count;

        // 현재 인원을 기준으로 증가량 계산
        int addAmount = currentCount * (multiplyValue - 1);

        squad.AddPlayer(addAmount);

        gameObject.SetActive(false);
    }
}