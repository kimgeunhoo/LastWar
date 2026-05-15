using UnityEngine;

public class GateMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 20f;
    [SerializeField]
    private float resetZ = -20f;

    private void Update()
    {
        transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        
        if (transform.position.z < resetZ)
        {
            gameObject.SetActive(false);
        }
    }


}
