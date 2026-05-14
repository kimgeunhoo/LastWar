using System;
using UnityEngine;

public class RoadLoop : MonoBehaviour
{
    [Header("Road Pieces")]
    [SerializeField]
    private Transform[] roadPieces;

    [Header("Move")]
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private float pieceLength = 60f;

    [Header("Loop")]
    [SerializeField]
    private float resetZ = -60f;

    private void Update()
    {
        MoveRoads();
    }

    private void MoveRoads()
    {
        for (int i = 0; i < roadPieces.Length; i++)
        {
            roadPieces[i].localPosition += Vector3.back * moveSpeed * Time.deltaTime;

            if (roadPieces[i].localPosition.z <= resetZ)
            {
                float frontZ = GetFrontMostZ();

                Vector3 pos = roadPieces[i].localPosition;
                pos.z = frontZ + pieceLength;
                roadPieces[i].localPosition = pos;
            }
        }

    }

    private float GetFrontMostZ()
    {
        float frontZ = roadPieces[0].position.z;
        for (int i = 1; i < roadPieces.Length; i++)
        {
            if (roadPieces[i].position.z > frontZ)
            {
                frontZ = roadPieces[i].localPosition.z;
            }
        }
        return frontZ;
    }

    
}
