using System;
using Unity.VisualScripting;
using UnityEngine;
using KevinIglesias;

public class PlayerMove : MonoBehaviour
{
    [Header("MoveSpeed")]
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private float minX = -6f;
    [SerializeField]
    private float maxX = 6f;
    [SerializeField] 
    private float minZ = -2f;
    [SerializeField]
    private float maxZ = 2f;

    [Header("Mouse")]
    [SerializeField]
    private Camera mainCamara;
    [SerializeField]
    private LayerMask groundLayer;

    private HumanSoldierController controller;

    private Vector3 targetPosition;

    private void Awake()
    {
        if(mainCamara == null)
            mainCamara = Camera.main;

        targetPosition = transform.position;
        controller = GetComponentInChildren<HumanSoldierController>();
        controller.animator.SetTrigger("Run");
        controller.animator.SetTrigger("Aim");
    }

    private void Update()
    {
        MouseTargetPos();
        Move();
    }
    private void MouseTargetPos()
    {
        Ray ray = mainCamara.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            targetPosition = hit.point;
        }
    }

    private void Move()
    {
        Vector3 currentPos = transform.position;

        float targetX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float targetZ = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        float nextX = Mathf.Lerp(currentPos.x, targetX, playerData.Speed * Time.deltaTime);
        float nextZ = Mathf.Lerp(currentPos.z, targetZ, playerData.Speed * Time.deltaTime);

        transform.position = new Vector3(nextX, currentPos.y, nextZ);
    }

}
